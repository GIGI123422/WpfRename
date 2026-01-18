# PROJECT_LOG.md - 프로젝트 진행 일지

> WpfRename 프로젝트의 의사결정 과정과 진행 상황을 기록합니다.  
> 최종 수정: 2026-01-18

---

## 프로젝트 개요

| 항목 | 내용 |
|------|------|
| 프로젝트명 | WpfRename |
| 목적 | PowerRename WPF 재구현 + WPF/MVVM 학습 |
| 시작일 | 2026-01-18 |
| 저장소 | https://github.com/GIGI123422/WpfRename |
| 로컬 경로 | `C:\Project\create_by_ai\WpfRename` |

---

## 2026-01-18 (Day 1) - 프로젝트 설정

### 🎯 오늘의 목표
- [x] 기술 스택 결정
- [x] 프로젝트 구조 설정
- [x] GitHub Repository 생성
- [x] 기본 문서 작성
- [x] VS 2026에서 WPF 프로젝트 생성

---

### 📋 의사결정 기록

#### 결정 1: UI 프레임워크 선택

**후보:**
| 프레임워크 | 장점 | 단점 |
|-----------|------|------|
| WinUI 3 | PowerRename 원본과 동일 | 학습 곡선 높음 |
| WPF | 드래그앤드롭 디자이너, 풍부한 자료 | 구형 기술 |
| Avalonia | 크로스플랫폼 | Windows 전용 기능 제한 |

**결정:** WPF + lepoco/wpfui

**이유:**
1. WinForms 경험 있음 → WPF가 자연스러운 다음 단계
2. XAML 디자이너로 시각적 UI 편집 가능
3. lepoco/wpfui로 Fluent Design 적용 가능
4. 학습 후 WinUI 3로 전환 시 80% 코드 재사용 가능 (MVVM/XAML 동일)

---

#### 결정 2: .NET 버전 선택

**후보:**
| 버전 | 지원 종료 | 특징 |
|------|----------|------|
| .NET 8 | 2026년 11월 | 2년간 안정화, 자료 풍부 |
| .NET 9 | 2026년 11월 | STS, 8과 동일 지원 종료 |
| .NET 10 | 2028년 11월 | LTS, 최신 |

**결정:** .NET 10

**이유:**
1. 여러 AI 학습 프로젝트를 `C:\Project\create_by_ai\`에서 관리 예정
2. 프로젝트마다 다른 런타임 버전은 관리 복잡
3. .NET 8 선택 시 10개월 후 마이그레이션 필요
4. .NET 10 선택 시 2028년까지 마이그레이션 불필요
5. **장기 유지보수 비용 > 초기 안정성 리스크**

---

#### 결정 3: 프로젝트 이름

**후보:**
| 이름 | 의미 |
|------|------|
| PowerRenameWpf | 원본 명시 |
| WpfRename | 기술 + 기능 |
| FluentRename | UI 스타일 강조 |

**결정:** WpfRename

**이유:**
1. 간결함
2. 학습 프로젝트임을 암시 (WPF 기술 강조)
3. 원본과 구분되면서 목적 명확

---

#### 결정 4: 프로젝트 구조

**참고:** David Fowler의 .NET 프로젝트 구조 (GitHub Gist, ⭐1,488)

**결정된 구조:**
```
WpfRename/
├── docs/                    # 문서 (하위 폴더 없이 단순하게)
│   ├── REQUIREMENTS.md
│   ├── LESSONS_LEARNED.md
│   └── PROJECT_LOG.md
├── src/
│   └── WpfRename/           # 메인 프로젝트
├── tests/
│   └── WpfRename.Tests/     # 테스트 (나중에)
├── .editorconfig
├── .gitattributes
├── .gitignore
├── CHANGELOG.md
├── CLAUDE.md
├── Directory.Build.props
├── global.json
├── LICENSE
├── README.md
├── TODO.md
└── WpfRename.sln
```

**이유:**
1. 업계 표준 (src/tests 분리)
2. 소규모 프로젝트이므로 docs/ 하위 폴더 불필요
3. AI 협업 문서(CLAUDE.md, TODO.md)는 루트에 배치하여 접근성 향상

---

#### 결정 5: 문서화 체계

**AI 시대 워크플로우:**
```
1. REQUIREMENTS.md  →  요구사항 정의 (AI와 공유하는 "계약서")
2. TODO.md          →  작업 로드맵 (Phase별 체크리스트)
3. CLAUDE.md        →  AI 협업 규칙 (코딩 스타일, 금지사항)
```

**추가 문서:**
```
4. LESSONS_LEARNED.md  →  시행착오 기록 (기술적 학습)
5. PROJECT_LOG.md      →  의사결정 기록 (프로젝트 맥락)
```

**이유:**
1. AI에게 명확한 컨텍스트 제공
2. 나중에 "왜 이렇게 했지?" 추적 가능
3. 학습 내용 축적 → 다음 프로젝트에 재사용

---

#### 결정 6: 라이선스

**결정:** MIT License

**이유:**
1. PowerToys가 MIT 라이선스
2. 파생 작업 허용, 상업적 사용 가능
3. 학습 프로젝트에 적합 (제약 최소)

---

#### 결정 7: 차별화 기능

**PowerRename에 없는 기능:** 프리셋 저장/불러오기

**배경:**
- PowerToys Issue #37539에서 요청됨
- 아직 구현되지 않음
- 실제 사용자 니즈가 있는 기능

**구현 계획:**
- JSON 파일로 프리셋 저장
- 검색/치환 패턴 + 옵션 저장
- 가져오기/내보내기로 공유 가능

---

### 🔧 진행 상황

| 단계 | 상태 | 비고 |
|------|------|------|
| 1. 문서 파일 생성 | ✅ 완료 | Claude Desktop에서 생성 |
| 2. GitHub Repository 생성 | ✅ 완료 | GIGI123422/WpfRename |
| 3. 로컬 clone 및 파일 배치 | ✅ 완료 | `C:\Project\create_by_ai\WpfRename` |
| 4. VS 2026에서 WPF 프로젝트 생성 | ✅ 완료 | .NET 10, wpfui 설치 |
| 5. VSCode + Claude 개발 시작 | ⏳ 다음 | - |

---

### 🐛 발생한 이슈

#### 이슈 1: Git push rejected
- **증상:** `! [rejected] main -> main (fetch first)`
- **원인:** GitHub에서 README 자동 생성 옵션 체크
- **해결:** `git pull --allow-unrelated-histories` 후 push
- **교훈:** 빈 repo 생성 시 모든 옵션 체크 해제

#### 이슈 2: LF/CRLF 경고
- **증상:** `warning: LF will be replaced by CRLF`
- **원인:** Linux에서 생성된 파일을 Windows에서 사용
- **해결:** `.gitattributes` 파일 추가
- **교훈:** 크로스플랫폼 프로젝트는 줄바꿈 정책 명시 필요

#### 이슈 3: VS 프로젝트 폴더 중복
- **증상:** `src/WpfRename/WpfRename/` 구조 생성
- **원인:** VS 프로젝트 생성 옵션 오해
- **해결:** 폴더 수동 정리
- **교훈:** Location과 "Place solution and project in same directory" 옵션 주의

---

### 💡 다음 단계

1. **VSCode + Claude 확장프로그램으로 개발 환경 전환**
2. **Phase 1 시작:** 기본 UI 레이아웃 (TODO.md 참조)
3. **lepoco/wpfui 적용:** Fluent Design 테마

---

### 📊 시간 기록

| 작업 | 소요 시간 |
|------|----------|
| 기술 스택 논의 및 결정 | ~1시간 |
| 문서 구조 설계 | ~30분 |
| 파일 생성 및 GitHub 설정 | ~30분 |
| 이슈 해결 (Git, VS) | ~30분 |
| 학습 내용 정리 | ~30분 |
| **총계** | **~3시간** |

---

## 변경 이력

| 날짜 | 내용 |
|------|------|
| 2026-01-18 | Day 1: 프로젝트 초기 설정 완료 |
