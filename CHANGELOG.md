# Changelog

모든 주요 변경사항이 이 파일에 기록됩니다.

이 프로젝트는 [Semantic Versioning](https://semver.org/spec/v2.0.0.html)을 따릅니다.

---

## [Unreleased]

### Changed
- UI 레이아웃 개선 (PowerRename 스타일)
  - 2-열 레이아웃으로 변경 (왼쪽 컨트롤 패널, 오른쪽 데이터 그리드)
  - 상단 메뉴바 제거
  - 왼쪽 패널에 모든 컨트롤 통합
    - 프리셋 선택/저장/삭제
    - 검색 및 치환 입력
    - 옵션 (정규식, 대소문자 구분, 대소문자 변환)
  - 정규식 도움말 Expander 추가
  - 하단 좌측에 아이콘 버튼 추가 (설정, 정보)
  - 오른쪽 패널에 파일 목록 및 상태바 배치

---

## [0.7.0] - Phase 7 완료 (2026-01-18)

### Added
- 다국어 지원 (한국어, 영어)
  - `Resources/Strings.resx` - 영어 (기본)
  - `Resources/Strings.ko-KR.resx` - 한국어
  - `ILocalizationService`, `LocalizationService` - 언어 설정 서비스
- About 다이얼로그
  - 버전 정보 표시
  - 기술 스택 및 라이선스 정보
  - GitHub 링크
  - Help 메뉴 추가

### Changed
- 모든 UI 문자열을 리소스 파일로 이동
- MainWindow에 메뉴 바 추가

---

## [0.6.0] - Phase 6 완료 (2026-01-18)

### Added
- 변수 지원 시스템
  - 열거형 변수: `${count}` (옵션: start, step, digits)
  - 날짜/시간 변수: `${now}`, `${created}`, `${modified}`
  - `IVariableService`, `VariableService` 구현
- 변수 사용 가이드 툴팁 추가

### Changed
- `MainViewModel.UpdatePreview()` 메서드에 변수 처리 통합
- 치환 패턴 TextBox에 변수 도움말 툴팁 추가

---

## [0.5.0] - Phase 5 완료 (2026-01-18)

### Added
- 프리셋 저장/불러오기 기능
  - `Preset` 모델 추가
  - `IPresetService`, `PresetService` 구현
  - JSON 기반 로컬 저장 (%LocalAppData%\WpfRename\presets.json)
- 프리셋 UI
  - ComboBox로 프리셋 선택
  - 저장/삭제 버튼
  - ContentDialog를 사용한 프리셋 이름 입력

---

## [0.4.0] - Phase 4 완료 (2026-01-18)

### Added
- 파일/폴더 이름 변경 실행 기능
  - `IFileSystemService`, `FileSystemService` 구현
  - `RenameOperation` 모델 (Undo 추적용)
- Undo(실행 취소) 기능
  - 마지막 이름 변경 작업 되돌리기
- 진행률 표시
  - ProgressBar와 상태 메시지

### Changed
- `MainViewModel`에 `RenameCommand`, `UndoCommand` 추가
- 비동기 파일 작업 처리

---

## [0.3.0] - Phase 3 완료 (2026-01-18)

### Added
- 정규식 및 일반 텍스트 검색/치환 엔진
  - `IRenameService`, `RenameService` 구현
  - 정규식 지원 (Generated Regex 사용)
  - 대소문자 구분 옵션
  - 대소문자 변환 옵션 (소문자, 대문자, Title Case)
- 실시간 미리보기 업데이트
- 오류 처리
  - 잘못된 정규식 패턴 감지
  - InfoBar를 통한 사용자 피드백

### Changed
- `MainViewModel`에 검색/치환 로직 통합
- `RenameItem`에 `NewName`, `HasConflict` 속성 추가

---

## [0.2.0] - Phase 2 완료 (2026-01-18)

### Added
- MVVM 패턴 구현
  - `MainViewModel` with CommunityToolkit.Mvvm
  - `RenameItem` 모델
  - ObservableProperty 및 RelayCommand 사용
- 파일/폴더 선택 기능
  - 드래그 앤 드롭 지원
  - 재귀적 폴더 탐색
- 데이터 바인딩
  - DataGrid ItemsSource 바인딩
  - 검색/치환 패턴 양방향 바인딩
  - 옵션 체크박스 바인딩

### Changed
- 정적 UI에서 MVVM 기반 동적 UI로 전환

---

## [0.1.0] - Phase 1 완료 (2026-01-18)

### Added
- WPF 기본 UI 구현
  - lepoco/wpfui (Fluent Design) 적용
  - MainWindow 레이아웃
    - 검색/치환 입력 영역
    - 옵션 체크박스 (정규식, 대소문자 구분)
    - DataGrid 파일 목록
    - 상태바 및 버튼
  - Dark/Light 테마 지원

### Technical
- .NET 10, WPF
- WPF-UI 4.2.0
- CommunityToolkit.Mvvm 8.4.0

---

## [0.0.0] - Phase 0 완료 (2026-01-18)

### Added
- 프로젝트 초기 설정
  - .NET 10 WPF 프로젝트 생성
  - Git 저장소 초기화
  - 프로젝트 문서
    - README.md
    - TODO.md
    - REQUIREMENTS.md
    - CLAUDE.md
    - PROJECT_LOG.md
  - 폴더 구조 설정

---

## 버전 관리 규칙

- **[Major]**: 주요 기능 변경, API 변경
- **[Minor]**: 새 기능 추가, Phase 완료
- **[Patch]**: 버그 수정, 문서 업데이트

---

[Unreleased]: https://github.com/YOUR_USERNAME/WpfRename/compare/v0.7.0...HEAD
[0.7.0]: https://github.com/YOUR_USERNAME/WpfRename/compare/v0.6.0...v0.7.0
[0.6.0]: https://github.com/YOUR_USERNAME/WpfRename/compare/v0.5.0...v0.6.0
[0.5.0]: https://github.com/YOUR_USERNAME/WpfRename/compare/v0.4.0...v0.5.0
[0.4.0]: https://github.com/YOUR_USERNAME/WpfRename/compare/v0.3.0...v0.4.0
[0.3.0]: https://github.com/YOUR_USERNAME/WpfRename/compare/v0.2.0...v0.3.0
[0.2.0]: https://github.com/YOUR_USERNAME/WpfRename/compare/v0.1.0...v0.2.0
[0.1.0]: https://github.com/YOUR_USERNAME/WpfRename/compare/v0.0.0...v0.1.0
[0.0.0]: https://github.com/YOUR_USERNAME/WpfRename/releases/tag/v0.0.0
