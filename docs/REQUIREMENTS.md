# WpfRename 요구사항 정의서

> 버전: 0.8.0
> 최종 수정: 2026-01-18
> 상태: Phase 0-7 완료 + UI 개선 완료

## 1. 프로젝트 개요

### 1.1 목적
PowerToys PowerRename의 WPF 재구현을 통해:
- WPF/XAML/MVVM 패턴 학습
- 파일 일괄 이름 변경 도구 개발
- PowerRename의 성능 문제 개선

### 1.2 배경
- PowerRename (WinUI 3 기반)의 대용량 파일 처리 시 성능 저하 문제
- WinForms → WPF 전환 학습 필요
- 프리셋 저장 기능 부재 (PowerToys Issue #37539)

### 1.3 기술 스택
| 구분 | 기술 |
|------|------|
| Framework | .NET 10 |
| UI | WPF + lepoco/wpfui (Fluent Design) |
| Pattern | MVVM (CommunityToolkit.Mvvm) |
| Language | C# 13, XAML |
| IDE | Visual Studio 2026 / VSCode |

---

## 2. 기능 요구사항 (Phase 0-7)

### 2.1 핵심 기능 (MVP) ✅ 완료

#### FR-001: 파일/폴더 선택
- [x] 드래그 앤 드롭으로 파일/폴더 추가
- [x] 폴더 선택 시 하위 항목 자동 포함
- [x] 선택 항목 목록 표시
- [x] 개별 항목 제거 기능
- [x] 전체 선택/해제 기능

#### FR-002: 검색/치환
- [x] 텍스트 검색 (일반 문자열)
- [x] 정규식 검색 지원 (Generated Regex)
- [x] 대소문자 구분 옵션
- [x] 치환 문자열 입력
- [x] 정규식 오류 표시 (InfoBar)

#### FR-003: 실시간 미리보기
- [x] 변경 전/후 파일명 비교 표시
- [x] 변경될 항목 자동 업데이트
- [x] 충돌(중복 파일명) 경고 표시
- [x] 선택/변경/충돌 개수 실시간 표시

#### FR-004: 이름 변경 실행
- [x] 미리보기 확인 후 실행
- [x] 진행률 표시 (ProgressBar)
- [x] 실행 취소(Undo) 기능
- [x] 오류 처리 (권한, 잠금, 파일 없음 등)

### 2.2 확장 기능 ✅ 완료

#### FR-005: 열거형 변수
- [x] `${count}` - 순번 (001, 002, ...)
- [x] `${count:start=10}` - 시작 번호 지정
- [x] `${count:step=5}` - 증가값 지정
- [x] `${count:digits=3}` - 자릿수 지정

#### FR-006: 날짜/시간 변수
- [x] `${now:yyyy-MM-dd}` - 현재 날짜
- [x] `${created:yyyy}` - 파일 생성일
- [x] `${modified:yyyyMMdd}` - 파일 수정일
- [x] 사용자 정의 날짜 형식 지원

#### FR-007: 프리셋 기능 ⭐ (PowerRename에 없는 기능)
- [x] 검색/치환 패턴 저장
- [x] 프리셋 이름 지정 (ContentDialog)
- [x] 프리셋 목록 관리 (추가/삭제)
- [x] JSON 파일로 로컬 저장 (%LocalAppData%\WpfRename\presets.json)

#### FR-008: 대소문자 변환
- [x] None (변환 없음)
- [x] 모두 소문자
- [x] 모두 대문자
- [x] Title Case (첫 글자 대문자)

#### FR-009: 다국어 지원
- [x] 한국어 (ko-KR)
- [x] 영어 (en-US, 기본)
- [x] 리소스 파일 기반 (.resx)
- [x] 시스템 언어 자동 감지

#### FR-010: About 다이얼로그
- [x] 버전 정보 표시
- [x] 라이선스 정보 (MIT)
- [x] GitHub 링크
- [x] 기술 스택 표시

### 2.3 향후 계획 (v0.9+)

#### FR-011: EXIF 메타데이터 (이미지 파일)
- [ ] `${exif:DateTaken}` - 촬영 날짜
- [ ] `${exif:Camera}` - 카메라 모델
- [ ] `${exif:Width}x${exif:Height}` - 해상도

#### FR-012: 필터링
- [ ] 파일만 / 폴더만 선택
- [ ] 확장자 필터 (*.jpg, *.png 등)
- [ ] 정규식 필터

#### FR-013: 설정 페이지
- [ ] 테마 선택 (다크/라이트/시스템)
- [ ] 언어 선택
- [ ] 기본 옵션 저장

---

## 2-A. UI 개선 요구사항 (2026-01-18 추가) ✅ 완료

### 배경
- PowerRename UI 레이아웃 참조
- 왼쪽 컨트롤 패널 + 오른쪽 데이터 뷰 구조
- 공간 활용 효율성 증대

### UI-001: 2-열 레이아웃
- [x] Grid.ColumnDefinitions 사용 (왼쪽 380px + 오른쪽 *)
- [x] 왼쪽: 모든 입력 컨트롤
- [x] 오른쪽: 파일 목록 및 상태바

### UI-002: 왼쪽 컨트롤 패널 재배치
- [x] 프리셋 선택 (ComboBox + 아이콘 버튼)
- [x] 검색 입력 (세로 레이아웃, 레이블 상단)
- [x] 치환 입력 (세로 레이아웃, 레이블 상단)
- [x] 옵션 섹션
  - [x] 정규식 사용 체크박스
  - [x] 대소문자 구분 체크박스
  - [x] 대소문자 변환 ComboBox
- [x] 정규식 오류 InfoBar

### UI-003: 정규식 도움말
- [x] Expander 컨트롤 사용
- [x] 기본 패턴 설명 (., *, +, ?, ^, $)
- [x] 문자 클래스 설명 ([abc], \d, \w, \s)
- [x] 그룹 및 치환 예시
- [x] ScrollViewer로 긴 내용 처리

### UI-004: 하단 아이콘 버튼
- [x] 상단 메뉴바 제거
- [x] 설정 버튼 (⚙️, Settings24 아이콘)
- [x] 정보 버튼 (ℹ️, Info24 아이콘)
- [x] About 다이얼로그 연결

### UI-005: 오른쪽 데이터 패널
- [x] 파일 목록 레이블 ("파일 목록...")
- [x] DataGrid (3개 열: 체크박스, 원본명, 변경명, 폴더 경로)
- [x] 드래그 앤 드롭 지원 유지
- [x] 진행률 표시 (ProgressBar)
- [x] 상태바 (선택/변경/충돌 개수)
- [x] 액션 버튼 (Undo, Clear, Rename)

---

## 3. 비기능 요구사항

### 3.1 성능
| 항목 | 목표 |
|------|------|
| 1,000개 파일 미리보기 | < 1초 |
| 10,000개 파일 미리보기 | < 5초 |
| UI 응답성 | 렌더링 지연 없음 |

### 3.2 사용성
- Windows 11 Fluent Design 스타일
- 다크/라이트 테마 지원
- 한국어/영어 다국어 지원

### 3.3 호환성
- Windows 10 21H2 이상
- Windows 11
- .NET 10 Runtime 필요

---

## 4. UI 설계

### 4.1 메인 화면 레이아웃 (v0.7.0 이전)
```
┌─────────────────────────────────────────────────────┐
│  Help ▼                                              │
├─────────────────────────────────────────────────────┤
│  [프리셋 ▼]  [저장] [삭제]                          │
│  [검색: ________]           [치환: ________]        │
│  ☑ 정규식  ☑ 대소문자 구분  [대소문자 변환 ▼]      │
├─────────────────────────────────────────────────────┤
│  ☑ │ 원본 파일명          │ 변경 후 파일명  │ 📁  │
│  ─────────────────────────────────────────────────  │
│  ☑ │ IMG_001.JPG          │ photo_001.jpg   │ C:\│
│  ☑ │ IMG_002.JPG          │ photo_002.jpg   │ C:\│
├─────────────────────────────────────────────────────┤
│  선택: 150개  변경: 148개  충돌: 2개                │
│                    [Undo] [Clear] [이름 변경]       │
└─────────────────────────────────────────────────────┘
```

### 4.1-A 메인 화면 레이아웃 (v0.8.0 UI 개선 후)
```
┌─────────────────────────────────────────────────────────────┐
│  왼쪽 패널 (380px)       │  오른쪽 패널 (가변)             │
│                          │                                  │
│  Preset:                 │  파일 목록 (드래그 앤 드롭):    │
│  [프리셋 선택 ▼] 💾 🗑️  │  ☑ │ 원본 │ 변경 │ 폴더 경로   │
│                          │  ───┼──────┼──────┼───────────  │
│  Search:                 │  ☑ │ IMG  │photo │ C:\Photos   │
│  [____________]          │  ☑ │ 001  │_001  │             │
│                          │  ☑ │ .JPG │.jpg  │             │
│  Replace:                │  ...│      │      │             │
│  [____________]          │                                  │
│  [변수 툴팁]             │  ─────────────                  │
│                          │  [ProgressBar]                   │
│  Options:                │                                  │
│  ☑ Use Regex             │  선택:150 변경:148 충돌:2       │
│  ☑ Case Sensitive        │           [Undo][Clear][Rename] │
│  Case Transform:         │                                  │
│  [None ▼]                │                                  │
│                          │                                  │
│  ▶ 정규식 도움말         │                                  │
│    (펼치면 패턴 설명)    │                                  │
│                          │                                  │
│  ⚙️ ℹ️                  │                                  │
└─────────────────────────────────────────────────────────────┘
```

### 4.2 화면 목록
| 화면 | 설명 | 상태 |
|------|------|------|
| MainWindow | 메인 작업 화면 (2-열 레이아웃) | ✅ 완료 |
| AboutDialog | 정보/라이선스/기술 스택 | ✅ 완료 |
| SettingsPage | 설정 (테마, 언어) | 🔜 계획 |

---

## 5. 데이터 모델

### 5.1 RenameItem
```csharp
public class RenameItem
{
    public string OriginalPath { get; set; }
    public string OriginalName { get; set; }
    public string NewName { get; set; }
    public bool IsDirectory { get; set; }
    public bool HasConflict { get; set; }
    public bool IsSelected { get; set; }
}
```

### 5.2 Preset
```csharp
public class Preset
{
    public string Name { get; set; }
    public string SearchPattern { get; set; }
    public string ReplacePattern { get; set; }
    public bool UseRegex { get; set; }
    public bool CaseSensitive { get; set; }
}
```

---

## 6. 참고 자료

- [PowerToys PowerRename 소스코드](https://github.com/microsoft/PowerToys/tree/main/src/modules/powerrename)
- [lepoco/wpfui 문서](https://wpfui.lepo.co/)
- [CommunityToolkit.Mvvm 문서](https://learn.microsoft.com/en-us/dotnet/communitytoolkit/mvvm/)

---

## 7. 구현 현황

### 완료된 기능 (v0.8.0)
✅ **Phase 0-7 모든 기능**
- 프로젝트 설정, 문서화
- WPF UI (Fluent Design)
- MVVM 패턴 (CommunityToolkit.Mvvm)
- 파일/폴더 선택 (드래그 앤 드롭, 재귀 탐색)
- 검색/치환 엔진 (정규식, 대소문자 구분)
- 실시간 미리보기
- 파일 이름 변경 실행 + Undo
- 프리셋 저장/불러오기 (JSON)
- 변수 지원 (${count}, ${now}, ${created}, ${modified})
- 다국어 지원 (한국어, 영어)
- About 다이얼로그

✅ **UI 개선 (v0.8.0)**
- 2-열 레이아웃 (PowerRename 스타일)
- 정규식 도움말 Expander
- 하단 아이콘 버튼 (설정, 정보)
- 상단 메뉴바 제거

### 다음 단계
- [ ] 설정 페이지 구현
- [ ] 추가 변수 (파일 크기, 확장자 등)
- [ ] EXIF 메타데이터 지원
- [ ] 설치 프로그램

---

## 8. 변경 이력

| 날짜 | 버전 | 변경 내용 |
|------|------|----------|
| 2026-01-18 | 0.1.0 | 초안 작성 |
| 2026-01-18 | 0.8.0 | Phase 0-7 완료, UI 개선 요구사항 추가 |
