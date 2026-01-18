namespace WpfRename.Services;

/// <summary>
/// 파일 이름 변경 서비스 인터페이스
/// </summary>
public interface IRenameService
{
    /// <summary>
    /// 텍스트 변환 수행 (일반 텍스트 또는 정규식)
    /// </summary>
    /// <param name="input">원본 텍스트</param>
    /// <param name="searchPattern">검색 패턴</param>
    /// <param name="replacePattern">치환 패턴</param>
    /// <param name="useRegex">정규식 사용 여부</param>
    /// <param name="caseSensitive">대소문자 구분 여부</param>
    /// <returns>변환된 텍스트</returns>
    string Transform(string input, string searchPattern, string replacePattern, bool useRegex, bool caseSensitive);

    /// <summary>
    /// 정규식 패턴 유효성 검사
    /// </summary>
    /// <param name="pattern">검사할 정규식 패턴</param>
    /// <returns>유효하면 null, 오류가 있으면 오류 메시지</returns>
    string? ValidateRegexPattern(string pattern);
}
