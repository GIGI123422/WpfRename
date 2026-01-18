<<<<<<< HEAD
# WpfRename

> PowerRename의 WPF 재구현 - 현대적인 파일 일괄 이름 변경 도구

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE)
[![.NET](https://img.shields.io/badge/.NET-10.0-512BD4)](https://dotnet.microsoft.com/)
[![WPF](https://img.shields.io/badge/WPF-Fluent%20Design-0078D4)](https://github.com/lepoco/wpfui)

---

## 🎯 소개

WpfRename은 [Microsoft PowerToys PowerRename](https://github.com/microsoft/PowerToys)의 WPF 재구현 프로젝트입니다.

**만든 이유:**
- PowerRename의 대용량 파일 처리 시 성능 문제 개선
- WPF/XAML/MVVM 패턴 학습
- 프리셋 저장 기능 추가 (PowerRename에 없는 기능)

---

## ✨ 기능

### 핵심 기능
- 📝 정규식 기반 검색/치환
- 👁️ 실시간 미리보기
- 📂 드래그 앤 드롭 파일 추가
- ↩️ 실행 취소(Undo) 지원

### 확장 기능
- 💾 **프리셋 저장/불러오기** ⭐ (PowerRename에 없음)
- 🔢 열거형 변수 (`${count}`)
- 📅 날짜 변수 (`${created}`, `${modified}`)
- 🔤 대소문자 변환

---

## 📸 스크린샷

> (개발 진행 후 추가 예정)

---

## 🚀 시작하기

### 요구사항
- Windows 10 (21H2 이상) 또는 Windows 11
- [.NET 10 Runtime](https://dotnet.microsoft.com/download/dotnet/10.0)

### 설치

#### 방법 1: 릴리스 다운로드
> (릴리스 후 추가 예정)

#### 방법 2: 소스에서 빌드
```bash
git clone https://github.com/YOUR_USERNAME/WpfRename.git
cd WpfRename
dotnet build src/WpfRename/WpfRename.csproj
dotnet run --project src/WpfRename/WpfRename.csproj
```

---

## 📖 사용법

1. 파일 또는 폴더를 앱에 드래그 앤 드롭
2. 검색할 패턴 입력 (일반 텍스트 또는 정규식)
3. 치환할 텍스트 입력
4. 미리보기에서 결과 확인
5. "이름 변경" 클릭

### 변수 사용 예시
| 입력 | 결과 |
|------|------|
| `IMG_${count:digits=3}` | IMG_001, IMG_002, IMG_003... |
| `${modified:yyyy-MM-dd}_photo` | 2026-01-18_photo |

---

## 🛠️ 기술 스택

| 구분 | 기술 |
|------|------|
| Framework | .NET 10 |
| UI | WPF + [lepoco/wpfui](https://github.com/lepoco/wpfui) |
| Pattern | MVVM ([CommunityToolkit.Mvvm](https://github.com/CommunityToolkit/dotnet)) |
| Language | C# 13 |

---

## 📁 프로젝트 구조

```
WpfRename/
├── docs/                    # 문서
│   └── REQUIREMENTS.md      # 요구사항 정의서
├── src/
│   └── WpfRename/           # 메인 프로젝트
├── tests/
│   └── WpfRename.Tests/     # 단위 테스트
├── TODO.md                  # 작업 목록
├── CHANGELOG.md             # 변경 이력
└── CLAUDE.md                # AI 협업 가이드
```

---

## 🤝 기여

이슈와 Pull Request를 환영합니다!

1. Fork
2. Feature 브랜치 생성 (`git checkout -b feature/amazing-feature`)
3. 커밋 (`git commit -m 'Add amazing feature'`)
4. Push (`git push origin feature/amazing-feature`)
5. Pull Request 생성

---

## 📝 라이선스

MIT License - 자세한 내용은 [LICENSE](LICENSE) 참조

---

## 🙏 감사

- [Microsoft PowerToys](https://github.com/microsoft/PowerToys) - 원본 PowerRename
- [lepoco/wpfui](https://github.com/lepoco/wpfui) - Fluent Design 컴포넌트
- [CommunityToolkit](https://github.com/CommunityToolkit/dotnet) - MVVM 툴킷

---

## 📊 프로젝트 상태

🚧 **개발 중** - MVP 진행 중
=======
# WpfRename
>>>>>>> 2253e0c5475cb63f04716caad98d597552d1d442
