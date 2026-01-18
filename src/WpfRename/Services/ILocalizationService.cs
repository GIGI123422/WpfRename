using System.Globalization;

namespace WpfRename.Services;

/// <summary>
/// 다국어 지원 서비스 인터페이스
/// </summary>
public interface ILocalizationService
{
    /// <summary>
    /// 현재 언어 가져오기
    /// </summary>
    CultureInfo CurrentCulture { get; }

    /// <summary>
    /// 언어 변경
    /// </summary>
    void SetCulture(string cultureName);

    /// <summary>
    /// 리소스 문자열 가져오기
    /// </summary>
    string GetString(string key);

    /// <summary>
    /// 사용 가능한 언어 목록
    /// </summary>
    List<CultureInfo> AvailableCultures { get; }
}
