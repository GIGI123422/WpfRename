using CommunityToolkit.Mvvm.ComponentModel;

namespace WpfRename.Models;

/// <summary>
/// 이름 변경 대상 항목 (파일 또는 폴더)
/// </summary>
public partial class RenameItem : ObservableObject
{
    /// <summary>
    /// 원본 전체 경로
    /// </summary>
    [ObservableProperty]
    private string _originalPath = string.Empty;

    /// <summary>
    /// 원본 파일/폴더명 (확장자 포함)
    /// </summary>
    [ObservableProperty]
    private string _originalName = string.Empty;

    /// <summary>
    /// 변경될 파일/폴더명
    /// </summary>
    [ObservableProperty]
    private string _newName = string.Empty;

    /// <summary>
    /// 폴더 여부 (true: 폴더, false: 파일)
    /// </summary>
    [ObservableProperty]
    private bool _isDirectory;

    /// <summary>
    /// 이름 충돌 여부 (중복된 파일명)
    /// </summary>
    [ObservableProperty]
    private bool _hasConflict;

    /// <summary>
    /// 선택 여부
    /// </summary>
    [ObservableProperty]
    private bool _isSelected = true;

    /// <summary>
    /// 이름이 변경되었는지 여부
    /// </summary>
    public bool IsRenamed => !string.IsNullOrEmpty(NewName) && OriginalName != NewName;
}
