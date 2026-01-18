using System.Text.RegularExpressions;

namespace WpfRename.Services;

/// <summary>
/// 파일 이름 변경 서비스 구현
/// </summary>
public class RenameService : IRenameService
{
    /// <summary>
    /// 텍스트 변환 수행
    /// </summary>
    public string Transform(string input, string searchPattern, string replacePattern, bool useRegex, bool caseSensitive)
    {
        if (string.IsNullOrEmpty(input) || string.IsNullOrEmpty(searchPattern))
        {
            return input;
        }

        try
        {
            if (useRegex)
            {
                return TransformWithRegex(input, searchPattern, replacePattern, caseSensitive);
            }
            else
            {
                return TransformWithPlainText(input, searchPattern, replacePattern, caseSensitive);
            }
        }
        catch (RegexMatchTimeoutException)
        {
            // 정규식 타임아웃 시 원본 반환
            return input;
        }
        catch (ArgumentException)
        {
            // 잘못된 정규식 패턴 시 원본 반환
            return input;
        }
    }

    /// <summary>
    /// 일반 텍스트 검색/치환
    /// </summary>
    private string TransformWithPlainText(string input, string searchPattern, string replacePattern, bool caseSensitive)
    {
        var comparison = caseSensitive
            ? StringComparison.Ordinal
            : StringComparison.OrdinalIgnoreCase;

        return input.Replace(searchPattern, replacePattern, comparison);
    }

    /// <summary>
    /// 정규식 검색/치환
    /// </summary>
    private string TransformWithRegex(string input, string searchPattern, string replacePattern, bool caseSensitive)
    {
        var options = RegexOptions.None;
        if (!caseSensitive)
        {
            options |= RegexOptions.IgnoreCase;
        }

        // 타임아웃 설정 (1초)
        var regex = new Regex(searchPattern, options, TimeSpan.FromSeconds(1));
        return regex.Replace(input, replacePattern);
    }

    /// <summary>
    /// 정규식 패턴 유효성 검사
    /// </summary>
    public string? ValidateRegexPattern(string pattern)
    {
        if (string.IsNullOrEmpty(pattern))
        {
            return null;
        }

        try
        {
            // 정규식 컴파일 시도
            _ = new Regex(pattern, RegexOptions.None, TimeSpan.FromSeconds(1));
            return null;
        }
        catch (RegexParseException ex)
        {
            return $"정규식 오류: {ex.Message}";
        }
        catch (ArgumentException ex)
        {
            return $"잘못된 패턴: {ex.Message}";
        }
    }
}
