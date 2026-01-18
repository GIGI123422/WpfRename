# WpfRename 요구사항 정의서

> 버전: 0.1.0  
> 최종 수정: 2026-01-18  
> 상태: 초안

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

## 2. 기능 요구사항

### 2.1 핵심 기능 (MVP)

#### FR-001: 파일/폴더 선택
- [ ] 드래그 앤 드롭으로 파일/폴더 추가
- [ ] 폴더 선택 시 하위 항목 자동 포함
- [ ] 선택 항목 목록 표시
- [ ] 개별 항목 제거 기능

#### FR-002: 검색/치환
- [ ] 텍스트 검색 (일반 문자열)
- [ ] 정규식 검색 지원
- [ ] 대소문자 구분 옵션
- [ ] 치환 문자열 입력

#### FR-003: 실시간 미리보기
- [ ] 변경 전/후 파일명 비교 표시
- [ ] 변경될 항목 하이라이트
- [ ] 충돌(중복 파일명) 경고 표시

#### FR-004: 이름 변경 실행
- [ ] 미리보기 확인 후 실행
- [ ] 진행률 표시
- [ ] 실행 취소(Undo) 기능

### 2.2 확장 기능 (v0.2+)

#### FR-005: 열거형 변수
- [ ] `${count}` - 순번 (001, 002, ...)
- [ ] `${count:start=10}` - 시작 번호 지정
- [ ] `${count:step=5}` - 증가값 지정
- [ ] `${count:digits=3}` - 자릿수 지정

#### FR-006: 날짜/시간 변수
- [ ] `${now:yyyy-MM-dd}` - 현재 날짜
- [ ] `${created:yyyy}` - 파일 생성일
- [ ] `${modified:yyyyMMdd}` - 파일 수정일

#### FR-007: 프리셋 기능 ⭐ (PowerRename에 없는 기능)
- [ ] 검색/치환 패턴 저장
- [ ] 프리셋 이름 지정
- [ ] 프리셋 목록 관리 (추가/수정/삭제)
- [ ] 프리셋 가져오기/내보내기 (JSON)

#### FR-008: 대소문자 변환
- [ ] 모두 소문자
- [ ] 모두 대문자
- [ ] Title Case (첫 글자 대문자)
- [ ] 확장자만 소문자

### 2.3 고급 기능 (v0.3+)

#### FR-009: EXIF 메타데이터 (이미지 파일)
- [ ] `${exif:DateTaken}` - 촬영 날짜
- [ ] `${exif:Camera}` - 카메라 모델
- [ ] `${exif:Width}x${exif:Height}` - 해상도

#### FR-010: 필터링
- [ ] 파일만 / 폴더만 선택
- [ ] 확장자 필터 (*.jpg, *.png 등)
- [ ] 정규식 필터

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

### 4.1 메인 화면 레이아웃
```
┌─────────────────────────────────────────────────────┐
│  [프리셋 ▼]  [검색: ________] [치환: ________]      │
│  ☑ 정규식  ☑ 대소문자 구분  [대소문자 변환 ▼]      │
├─────────────────────────────────────────────────────┤
│  원본 파일명              │  변경 후 파일명          │
│  ─────────────────────────┼─────────────────────────│
│  IMG_001.JPG              │  photo_001.jpg          │
│  IMG_002.JPG              │  photo_002.jpg          │
│  IMG_003.JPG              │  photo_003.jpg          │
│  ...                      │  ...                    │
├─────────────────────────────────────────────────────┤
│  선택: 150개  변경: 148개  충돌: 2개                │
│                              [취소]  [이름 변경]    │
└─────────────────────────────────────────────────────┘
```

### 4.2 화면 목록
| 화면 | 설명 |
|------|------|
| MainWindow | 메인 작업 화면 |
| PresetDialog | 프리셋 관리 다이얼로그 |
| SettingsPage | 설정 (테마, 언어) |
| AboutDialog | 정보/라이선스 |

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

## 7. 변경 이력

| 날짜 | 버전 | 변경 내용 |
|------|------|----------|
| 2026-01-18 | 0.1.0 | 초안 작성 |
