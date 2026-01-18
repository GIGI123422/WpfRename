using System.Collections.ObjectModel;
using System.IO;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WpfRename.Models;
using WpfRename.Services;

namespace WpfRename.ViewModels;

/// <summary>
/// 메인 창 ViewModel
/// </summary>
public partial class MainViewModel : ObservableObject
{
    private readonly IRenameService _renameService;
    private readonly IFileSystemService _fileSystemService;
    private readonly IPresetService _presetService;
    private readonly List<RenameOperation> _lastOperations = [];

    public MainViewModel() : this(new RenameService(), new FileSystemService(), new PresetService())
    {
    }

    public MainViewModel(IRenameService renameService, IFileSystemService fileSystemService, IPresetService presetService)
    {
        _renameService = renameService;
        _fileSystemService = fileSystemService;
        _presetService = presetService;

        // 프리셋 로드
        _ = LoadPresetsAsync();
    }
    /// <summary>
    /// 검색할 텍스트
    /// </summary>
    [ObservableProperty]
    private string _searchPattern = string.Empty;

    /// <summary>
    /// 치환할 텍스트
    /// </summary>
    [ObservableProperty]
    private string _replacePattern = string.Empty;

    /// <summary>
    /// 정규식 사용 여부
    /// </summary>
    [ObservableProperty]
    private bool _useRegex;

    /// <summary>
    /// 대소문자 구분 여부
    /// </summary>
    [ObservableProperty]
    private bool _caseSensitive;

    /// <summary>
    /// 대소문자 변환 인덱스 (0: 변환 안 함, 1: 소문자, 2: 대문자, 3: Title Case)
    /// </summary>
    [ObservableProperty]
    private int _caseTransformIndex;

    /// <summary>
    /// 정규식 오류 메시지
    /// </summary>
    [ObservableProperty]
    private string? _regexError;

    /// <summary>
    /// 처리 중 여부
    /// </summary>
    [ObservableProperty]
    private bool _isProcessing;

    /// <summary>
    /// 진행률 (0-100)
    /// </summary>
    [ObservableProperty]
    private int _progress;

    /// <summary>
    /// 상태 메시지
    /// </summary>
    [ObservableProperty]
    private string _statusMessage = string.Empty;

    /// <summary>
    /// 프리셋 목록
    /// </summary>
    public ObservableCollection<Preset> Presets { get; } = [];

    /// <summary>
    /// 선택된 프리셋
    /// </summary>
    [ObservableProperty]
    private Preset? _selectedPreset;

    /// <summary>
    /// 이름 변경 대상 항목 목록
    /// </summary>
    public ObservableCollection<RenameItem> Items { get; } = [];

    /// <summary>
    /// 선택된 항목 개수
    /// </summary>
    public int SelectedCount => Items.Count(x => x.IsSelected);

    /// <summary>
    /// 이름이 변경될 항목 개수
    /// </summary>
    public int RenamedCount => Items.Count(x => x.IsSelected && x.IsRenamed);

    /// <summary>
    /// 충돌(중복 파일명) 개수
    /// </summary>
    public int ConflictCount => Items.Count(x => x.HasConflict);

    /// <summary>
    /// SearchPattern 변경 시 미리보기 업데이트
    /// </summary>
    partial void OnSearchPatternChanged(string value)
    {
        ValidateRegex();
        UpdatePreview();
    }

    /// <summary>
    /// ReplacePattern 변경 시 미리보기 업데이트
    /// </summary>
    partial void OnReplacePatternChanged(string value)
    {
        UpdatePreview();
    }

    /// <summary>
    /// 정규식 옵션 변경 시 미리보기 업데이트
    /// </summary>
    partial void OnUseRegexChanged(bool value)
    {
        ValidateRegex();
        UpdatePreview();
    }

    /// <summary>
    /// 대소문자 구분 옵션 변경 시 미리보기 업데이트
    /// </summary>
    partial void OnCaseSensitiveChanged(bool value)
    {
        UpdatePreview();
    }

    /// <summary>
    /// 대소문자 변환 옵션 변경 시 미리보기 업데이트
    /// </summary>
    partial void OnCaseTransformIndexChanged(int value)
    {
        UpdatePreview();
    }

    /// <summary>
    /// 파일/폴더 추가
    /// </summary>
    [RelayCommand]
    private void AddItems(string[] paths)
    {
        foreach (var path in paths)
        {
            if (File.Exists(path))
            {
                AddFile(path);
            }
            else if (Directory.Exists(path))
            {
                AddDirectory(path);
            }
        }

        UpdatePreview();
        UpdateCounts();
    }

    /// <summary>
    /// 파일 추가
    /// </summary>
    private void AddFile(string filePath)
    {
        var item = new RenameItem
        {
            OriginalPath = Path.GetDirectoryName(filePath) ?? string.Empty,
            OriginalName = Path.GetFileName(filePath),
            IsDirectory = false
        };

        Items.Add(item);
    }

    /// <summary>
    /// 폴더 및 하위 파일 재귀 추가
    /// </summary>
    private void AddDirectory(string directoryPath)
    {
        try
        {
            // 폴더 자체 추가
            var dirItem = new RenameItem
            {
                OriginalPath = Path.GetDirectoryName(directoryPath) ?? string.Empty,
                OriginalName = Path.GetFileName(directoryPath),
                IsDirectory = true
            };
            Items.Add(dirItem);

            // 하위 파일 추가
            foreach (var file in Directory.GetFiles(directoryPath, "*", SearchOption.AllDirectories))
            {
                AddFile(file);
            }

            // 하위 폴더 추가
            foreach (var subDir in Directory.GetDirectories(directoryPath, "*", SearchOption.AllDirectories))
            {
                var subDirItem = new RenameItem
                {
                    OriginalPath = Path.GetDirectoryName(subDir) ?? string.Empty,
                    OriginalName = Path.GetFileName(subDir),
                    IsDirectory = true
                };
                Items.Add(subDirItem);
            }
        }
        catch (UnauthorizedAccessException)
        {
            // 권한 없는 폴더는 무시
        }
    }

    /// <summary>
    /// 전체 목록 지우기
    /// </summary>
    [RelayCommand]
    private void ClearItems()
    {
        Items.Clear();
        UpdateCounts();
    }

    /// <summary>
    /// 정규식 유효성 검사
    /// </summary>
    private void ValidateRegex()
    {
        if (UseRegex && !string.IsNullOrEmpty(SearchPattern))
        {
            RegexError = _renameService.ValidateRegexPattern(SearchPattern);
        }
        else
        {
            RegexError = null;
        }
    }

    /// <summary>
    /// 미리보기 업데이트
    /// </summary>
    private void UpdatePreview()
    {
        if (string.IsNullOrEmpty(SearchPattern))
        {
            // 검색어가 없으면 원본 이름 유지
            foreach (var item in Items)
            {
                item.NewName = item.OriginalName;
            }
        }
        else if (!string.IsNullOrEmpty(RegexError))
        {
            // 정규식 오류가 있으면 원본 이름 유지
            foreach (var item in Items)
            {
                item.NewName = item.OriginalName;
            }
        }
        else
        {
            // RenameService를 사용한 변환
            foreach (var item in Items)
            {
                var transformed = _renameService.Transform(
                    item.OriginalName,
                    SearchPattern,
                    ReplacePattern,
                    UseRegex,
                    CaseSensitive);

                // 대소문자 변환 적용
                var newName = CaseTransformIndex switch
                {
                    1 => transformed.ToLower(),
                    2 => transformed.ToUpper(),
                    3 => System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(transformed.ToLower()),
                    _ => transformed
                };

                item.NewName = newName;
            }
        }

        CheckConflicts();
        UpdateCounts();
    }

    /// <summary>
    /// 이름 충돌 검사
    /// </summary>
    private void CheckConflicts()
    {
        // 같은 경로 내에서 중복된 NewName이 있는지 검사
        var groups = Items.GroupBy(x => Path.Combine(x.OriginalPath, x.NewName));

        foreach (var item in Items)
        {
            item.HasConflict = false;
        }

        foreach (var group in groups.Where(g => g.Count() > 1))
        {
            foreach (var item in group)
            {
                item.HasConflict = true;
            }
        }
    }

    /// <summary>
    /// 카운트 업데이트 (UI 갱신)
    /// </summary>
    private void UpdateCounts()
    {
        OnPropertyChanged(nameof(SelectedCount));
        OnPropertyChanged(nameof(RenamedCount));
        OnPropertyChanged(nameof(ConflictCount));
    }

    /// <summary>
    /// 이름 변경 실행
    /// </summary>
    [RelayCommand(CanExecute = nameof(CanRename))]
    private async Task RenameAsync()
    {
        try
        {
            IsProcessing = true;
            Progress = 0;
            _lastOperations.Clear();

            var selectedItems = Items.Where(x => x.IsSelected && x.IsRenamed).ToList();
            var totalCount = selectedItems.Count;

            if (totalCount == 0)
            {
                return;
            }

            StatusMessage = $"이름 변경 중... (0/{totalCount})";

            for (int i = 0; i < selectedItems.Count; i++)
            {
                var item = selectedItems[i];
                var oldPath = Path.Combine(item.OriginalPath, item.OriginalName);
                var newPath = Path.Combine(item.OriginalPath, item.NewName);

                var operation = new RenameOperation
                {
                    OldPath = oldPath,
                    NewPath = newPath,
                    IsDirectory = item.IsDirectory
                };

                try
                {
                    // UI 응답성을 위한 비동기 처리
                    await Task.Run(() =>
                    {
                        if (item.IsDirectory)
                        {
                            _fileSystemService.RenameDirectory(oldPath, newPath);
                        }
                        else
                        {
                            _fileSystemService.RenameFile(oldPath, newPath);
                        }
                    });

                    operation.Success = true;
                    _lastOperations.Add(operation);

                    // 원본 정보 업데이트
                    item.OriginalName = item.NewName;
                }
                catch (Exception ex)
                {
                    operation.Success = false;
                    operation.ErrorMessage = ex.Message;
                    _lastOperations.Add(operation);

                    StatusMessage = $"오류 발생: {item.OriginalName} - {ex.Message}";
                    await Task.Delay(2000); // 오류 메시지 표시
                }

                Progress = (int)((i + 1) / (double)totalCount * 100);
                StatusMessage = $"이름 변경 중... ({i + 1}/{totalCount})";
            }

            var successCount = _lastOperations.Count(x => x.Success);
            var failCount = _lastOperations.Count(x => !x.Success);

            StatusMessage = failCount > 0
                ? $"완료: {successCount}개 성공, {failCount}개 실패"
                : $"완료: {successCount}개 파일 이름 변경됨";

            // 미리보기 업데이트
            UpdatePreview();
        }
        finally
        {
            IsProcessing = false;
            Progress = 0;
        }
    }

    private bool CanRename() => !IsProcessing && RenamedCount > 0 && ConflictCount == 0;

    /// <summary>
    /// 실행 취소 (Undo)
    /// </summary>
    [RelayCommand(CanExecute = nameof(CanUndo))]
    private async Task UndoAsync()
    {
        try
        {
            IsProcessing = true;
            Progress = 0;

            var operations = _lastOperations.Where(x => x.Success).Reverse().ToList();
            var totalCount = operations.Count;

            if (totalCount == 0)
            {
                StatusMessage = "실행 취소할 작업이 없습니다.";
                return;
            }

            StatusMessage = $"실행 취소 중... (0/{totalCount})";

            for (int i = 0; i < operations.Count; i++)
            {
                var operation = operations[i];

                try
                {
                    await Task.Run(() =>
                    {
                        if (operation.IsDirectory)
                        {
                            _fileSystemService.RenameDirectory(operation.NewPath, operation.OldPath);
                        }
                        else
                        {
                            _fileSystemService.RenameFile(operation.NewPath, operation.OldPath);
                        }
                    });

                    // 원본 정보 복원
                    var item = Items.FirstOrDefault(x =>
                        Path.Combine(x.OriginalPath, x.OriginalName) == operation.NewPath);
                    if (item != null)
                    {
                        item.OriginalName = Path.GetFileName(operation.OldPath);
                    }
                }
                catch (Exception ex)
                {
                    StatusMessage = $"실행 취소 오류: {ex.Message}";
                    await Task.Delay(2000);
                }

                Progress = (int)((i + 1) / (double)totalCount * 100);
                StatusMessage = $"실행 취소 중... ({i + 1}/{totalCount})";
            }

            _lastOperations.Clear();
            StatusMessage = $"실행 취소 완료: {totalCount}개 항목 복원됨";

            // 미리보기 업데이트
            UpdatePreview();
        }
        finally
        {
            IsProcessing = false;
            Progress = 0;
        }
    }

    private bool CanUndo() => !IsProcessing && _lastOperations.Any(x => x.Success);

    /// <summary>
    /// 프리셋 로드
    /// </summary>
    private async Task LoadPresetsAsync()
    {
        var presets = await _presetService.LoadPresetsAsync();
        Presets.Clear();
        foreach (var preset in presets)
        {
            Presets.Add(preset);
        }
    }

    /// <summary>
    /// 프리셋 저장
    /// </summary>
    [RelayCommand]
    private async Task SavePresetAsync(string? presetName)
    {
        if (string.IsNullOrWhiteSpace(presetName))
        {
            StatusMessage = "프리셋 이름을 입력해주세요.";
            return;
        }

        var preset = new Preset
        {
            Name = presetName,
            SearchPattern = SearchPattern,
            ReplacePattern = ReplacePattern,
            UseRegex = UseRegex,
            CaseSensitive = CaseSensitive,
            CaseTransformIndex = CaseTransformIndex,
            CreatedAt = DateTime.Now
        };

        try
        {
            await _presetService.SavePresetAsync(preset);
            await LoadPresetsAsync();
            StatusMessage = $"프리셋 '{presetName}' 저장됨";
        }
        catch (Exception ex)
        {
            StatusMessage = $"프리셋 저장 오류: {ex.Message}";
        }
    }

    /// <summary>
    /// 프리셋 불러오기
    /// </summary>
    partial void OnSelectedPresetChanged(Preset? value)
    {
        if (value != null)
        {
            SearchPattern = value.SearchPattern;
            ReplacePattern = value.ReplacePattern;
            UseRegex = value.UseRegex;
            CaseSensitive = value.CaseSensitive;
            CaseTransformIndex = value.CaseTransformIndex;

            StatusMessage = $"프리셋 '{value.Name}' 적용됨";
        }
    }

    /// <summary>
    /// 프리셋 삭제
    /// </summary>
    [RelayCommand]
    private async Task DeletePresetAsync(Preset? preset)
    {
        if (preset == null)
        {
            return;
        }

        try
        {
            await _presetService.DeletePresetAsync(preset.Name);
            await LoadPresetsAsync();
            SelectedPreset = null;
            StatusMessage = $"프리셋 '{preset.Name}' 삭제됨";
        }
        catch (Exception ex)
        {
            StatusMessage = $"프리셋 삭제 오류: {ex.Message}";
        }
    }
}
