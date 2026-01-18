# WpfRename 작업 목록 (TODO)

> 최종 수정: 2026-01-18
> 현재 단계: Phase 0-7 완료 + UI 개선 완료

---

## 🎯 현재 진행 중

- [x] **Phase 0-7 모든 기능 구현 완료** (2026-01-18)
- [x] **UI 레이아웃 개선 (PowerRename 스타일)** (2026-01-18)

---

## ✅ Phase 0: 프로젝트 설정 (완료)

### 환경 설정
- [x] GitHub Repository 생성
- [x] 로컬에 clone (`C:\Project\create_by_ai\WpfRename`)
- [x] VS 2026에서 WPF 프로젝트 생성 (`src/WpfRename/`)
- [x] lepoco/wpfui NuGet 패키지 설치
- [x] CommunityToolkit.Mvvm NuGet 패키지 설치
- [x] 프로젝트 빌드 확인

### 문서
- [x] REQUIREMENTS.md 작성
- [x] TODO.md 작성
- [x] CLAUDE.md 작성
- [x] README.md 초안 작성
- [x] GitHub에 push

---

## ✅ Phase 1: MVP 기본 UI (완료)

- [x] MainWindow 기본 레이아웃 (XAML)
- [x] Fluent Design 테마 적용
- [x] 검색/치환 입력 영역
- [x] 파일 목록 DataGrid
- [x] 상태바 (선택 개수, 변경 개수)
- [x] 기본 버튼 (취소, 이름 변경)

---

## ✅ Phase 2: 파일 선택 기능 (완료)

- [x] ViewModel 기본 구조 (MainViewModel)
- [x] 드래그 앤 드롭 구현
- [x] 폴더 선택 시 하위 파일 재귀 탐색
- [x] 파일 목록 ObservableCollection 바인딩
- [x] 개별 항목 제거 기능
- [x] 전체 선택/해제

---

## ✅ Phase 3: 검색/치환 엔진 (완료)

- [x] RenameService 클래스 생성
- [x] 일반 텍스트 검색/치환
- [x] 정규식 검색/치환
- [x] 대소문자 구분 옵션
- [x] 실시간 미리보기 업데이트
- [x] 충돌(중복 파일명) 감지

---

## ✅ Phase 4: 이름 변경 실행 (완료)

- [x] FileSystemService 구현
- [x] 파일 이름 변경 (File.Move)
- [x] 폴더 이름 변경 (Directory.Move)
- [x] 진행률 표시 (ProgressBar)
- [x] 오류 처리 (권한, 잠금 등)
- [x] 실행 취소(Undo) 기능

---

## ✅ Phase 5: 프리셋 기능 (완료)

- [x] Preset 모델 클래스
- [x] PresetService (JSON 저장/로드)
- [x] 프리셋 저장 다이얼로그
- [x] 프리셋 목록 ComboBox
- [x] 프리셋 삭제 기능

---

## ✅ Phase 6: 확장 기능 (완료)

- [x] 변수 파서 구현 (VariableService)
- [x] `${count}` 열거형 변수 (start, step, digits 옵션)
- [x] `${now}`, `${created}`, `${modified}` 날짜 변수
- [x] 대소문자 변환 (Lower/Upper/Title)
- [x] 변수 도움말 툴팁

---

## ✅ Phase 7: 다국어 및 마무리 (완료)

- [x] 한국어/영어 리소스 파일 (Strings.resx, Strings.ko-KR.resx)
- [x] LocalizationService 구현
- [x] About 다이얼로그
- [x] 모든 UI 문자열 리소스화

---

## ✅ UI 개선 (2026-01-18 완료)

### PowerRename 스타일 레이아웃 적용
- [x] 2-열 레이아웃으로 변경 (왼쪽 컨트롤 패널 380px + 오른쪽 데이터 그리드)
- [x] 상단 메뉴바 제거
- [x] 왼쪽 패널에 모든 컨트롤 통합
  - [x] 프리셋 선택/저장/삭제 (아이콘 버튼)
  - [x] 검색 입력 (세로 레이아웃)
  - [x] 치환 입력 (세로 레이아웃)
  - [x] 옵션 섹션 (정규식, 대소문자 구분, 대소문자 변환)
- [x] 정규식 도움말 Expander 추가
  - [x] 기본 패턴 (., *, +, ?, ^, $)
  - [x] 문자 클래스 ([abc], \d, \w, \s)
  - [x] 그룹 및 치환 예시
- [x] 하단 좌측에 아이콘 버튼 추가
  - [x] 설정 버튼 (⚙️, 현재 비활성)
  - [x] 정보 버튼 (ℹ️, About 다이얼로그 연결)
- [x] 오른쪽 패널에 파일 목록 및 상태바 배치
- [x] 빌드 테스트 성공

---

## 🐛 버그 / 이슈

(발견된 버그를 여기에 기록)

---

## 💡 아이디어 (Backlog)

- [ ] 파일 필터링 (확장자별)
- [ ] EXIF 메타데이터 지원
- [ ] 정규식 테스터 내장
- [ ] 히스토리 기능 (최근 작업)
- [ ] 키보드 단축키
- [ ] 명령줄 인터페이스 (CLI)

---

## 📊 진행률

| Phase | 상태 | 완료율 |
|-------|------|--------|
| Phase 0 | ✅ 완료 | 100% |
| Phase 1 | ✅ 완료 | 100% |
| Phase 2 | ✅ 완료 | 100% |
| Phase 3 | ✅ 완료 | 100% |
| Phase 4 | ✅ 완료 | 100% |
| Phase 5 | ✅ 완료 | 100% |
| Phase 6 | ✅ 완료 | 100% |
| Phase 7 | ✅ 완료 | 100% |
| UI 개선 | ✅ 완료 | 100% |

**전체 진행률: 100% (모든 계획된 기능 완료)**

---

## 🎉 다음 단계 (선택사항)

- [ ] 통합 테스트 및 버그 수정
- [ ] 설정 페이지 구현 (현재 버튼 비활성화됨)
- [ ] 추가 변수 지원 (파일 크기, 확장자 등)
- [ ] 설치 프로그램 생성
- [ ] GitHub Release 배포

---

## 변경 이력

| 날짜 | 변경 내용 |
|------|----------|
| 2026-01-18 | 초안 작성, Phase 0-7 정의 |
| 2026-01-18 | Phase 0-7 모두 완료, UI 개선 완료 (PowerRename 스타일) |
