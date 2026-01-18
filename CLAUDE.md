# CLAUDE.md - AI 협업 가이드

> 이 파일은 AI(Claude)가 WpfRename 프로젝트 작업 시 참조하는 규칙입니다.

---

## 프로젝트 개요

WpfRename은 PowerToys PowerRename의 WPF 재구현 프로젝트입니다.

| 항목 | 값 |
|------|-----|
| 목적 | WPF/MVVM 학습 + 파일 이름 변경 도구 |
| Framework | .NET 10, WPF |
| UI Library | lepoco/wpfui (Fluent Design) |
| MVVM | CommunityToolkit.Mvvm |
| 언어 | C# 13, XAML |

---

## 핵심 문서

작업 전 반드시 참조:

1. **docs/REQUIREMENTS.md** - 요구사항 정의서
2. **TODO.md** - 현재 작업 목록 및 진행 상황
3. **CHANGELOG.md** - 변경 이력

---

## 코딩 규칙

### C# 스타일
```csharp
// ✅ 좋음: 파일 범위 네임스페이스
namespace WpfRename.ViewModels;

// ✅ 좋음: Primary Constructor (C# 12+)
public class MainViewModel(IRenameService renameService) : ObservableObject
{
    private readonly IRenameService _renameService = renameService;
}

// ✅ 좋음: 표현식 본문
public string FullPath => Path.Combine(Directory, FileName);

// ✅ 좋음: null 조건 연산자
var name = item?.Name ?? "Unknown";
```

### XAML 스타일
```xml
<!-- ✅ 좋음: 속성 줄바꿈 (3개 이상일 때) -->
<Button
    Content="이름 변경"
    Command="{Binding RenameCommand}"
    IsEnabled="{Binding CanRename}"
    Style="{StaticResource AccentButtonStyle}" />

<!-- ✅ 좋음: wpfui 컨트롤 사용 -->
<ui:TextBox
    PlaceholderText="검색할 텍스트"
    Text="{Binding SearchPattern, UpdateSourceTrigger=PropertyChanged}" />
```

### MVVM 패턴
```csharp
// ViewModel은 CommunityToolkit.Mvvm 사용
public partial class MainViewModel : ObservableObject
{
    [ObservableProperty]
    private string _searchPattern = string.Empty;

    [RelayCommand]
    private async Task RenameAsync()
    {
        // 구현
    }
}
```

---

## 프로젝트 구조

```
src/WpfRename/
├── App.xaml                 # 앱 진입점
├── MainWindow.xaml          # 메인 창
├── ViewModels/              # MVVM ViewModels
│   └── MainViewModel.cs
├── Views/                   # UserControl, Page
├── Models/                  # 데이터 모델
│   ├── RenameItem.cs
│   └── Preset.cs
├── Services/                # 비즈니스 로직
│   ├── IRenameService.cs
│   └── RenameService.cs
├── Converters/              # XAML 값 변환기
├── Resources/               # 리소스 (문자열, 이미지)
│   ├── Strings.ko-KR.resx
│   └── Strings.en-US.resx
└── Assets/                  # 아이콘, 이미지
```

---

## 작업 규칙

### 작업 시작 전
1. `TODO.md`에서 현재 작업 확인
2. `docs/REQUIREMENTS.md`에서 요구사항 확인
3. 관련 기존 코드 파악

### 코드 작성 시
1. **한 번에 한 기능씩** 구현
2. 기존 패턴과 일관성 유지
3. 주석은 "왜"를 설명 (무엇이 아니라)
4. 한국어 주석 허용 (사용자 요청)

### 작업 완료 후
1. `TODO.md` 체크박스 업데이트
2. 중요 변경 시 `CHANGELOG.md` 업데이트

---

## 의존성 (NuGet)

```xml
<PackageReference Include="WPF-UI" Version="4.*" />
<PackageReference Include="CommunityToolkit.Mvvm" Version="8.*" />
<PackageReference Include="Microsoft.Extensions.DependencyInjection" Version="10.*" />
```

---

## 자주 사용하는 패턴

### DI 등록 (App.xaml.cs)
```csharp
services.AddSingleton<MainViewModel>();
services.AddSingleton<IRenameService, RenameService>();
services.AddSingleton<IPresetService, PresetService>();
```

### 비동기 명령
```csharp
[RelayCommand(CanExecute = nameof(CanRename))]
private async Task RenameAsync(CancellationToken token)
{
    try
    {
        IsProcessing = true;
        await _renameService.RenameAsync(Items, token);
    }
    finally
    {
        IsProcessing = false;
    }
}
```

### 파일 작업
```csharp
// 파일 이름 변경
File.Move(oldPath, newPath);

// 폴더 이름 변경
Directory.Move(oldPath, newPath);

// 안전한 경로 결합
var fullPath = Path.Combine(directory, fileName);
```

---

## 금지 사항

- ❌ Code-behind에 비즈니스 로직 금지 (MVVM 위반)
- ❌ 하드코딩된 문자열 (리소스 파일 사용)
- ❌ 동기 파일 I/O (async/await 사용)
- ❌ try-catch 없는 파일 작업

---

## 질문 시 제공할 정보

AI에게 질문할 때 다음 정보 포함:
1. 현재 작업 중인 Phase (TODO.md 참조)
2. 관련 파일 경로
3. 에러 메시지 (있다면)
4. 기대 동작 vs 실제 동작

---

## 변경 이력

| 날짜 | 변경 내용 |
|------|----------|
| 2026-01-18 | 초안 작성 |
