using System.Globalization;
using WpfRename.Resources;

namespace WpfRename.Services;

/// <summary>
/// 다국어 지원 서비스 구현
/// </summary>
public class LocalizationService : ILocalizationService
{
    private CultureInfo _currentCulture;

    public LocalizationService()
    {
        // 시스템 언어 설정
        _currentCulture = CultureInfo.CurrentUICulture;

        // 지원하지 않는 언어면 영어로 폴백
        if (!AvailableCultures.Any(c => c.Name == _currentCulture.Name))
        {
            _currentCulture = new CultureInfo("en-US");
        }

        SetCulture(_currentCulture.Name);
    }

    /// <summary>
    /// 현재 언어
    /// </summary>
    public CultureInfo CurrentCulture => _currentCulture;

    /// <summary>
    /// 사용 가능한 언어 목록
    /// </summary>
    public List<CultureInfo> AvailableCultures { get; } = new()
    {
        new CultureInfo("en-US"),
        new CultureInfo("ko-KR")
    };

    /// <summary>
    /// 언어 변경
    /// </summary>
    public void SetCulture(string cultureName)
    {
        try
        {
            var culture = new CultureInfo(cultureName);
            _currentCulture = culture;

            // 현재 스레드 문화권 설정
            CultureInfo.CurrentUICulture = culture;
            CultureInfo.CurrentCulture = culture;

            // WPF 리소스 문화권 설정
            System.Threading.Thread.CurrentThread.CurrentUICulture = culture;
            System.Threading.Thread.CurrentThread.CurrentCulture = culture;
        }
        catch (CultureNotFoundException)
        {
            // 지원하지 않는 언어면 영어로 폴백
            SetCulture("en-US");
        }
    }

    /// <summary>
    /// 리소스 문자열 가져오기
    /// </summary>
    public string GetString(string key)
    {
        try
        {
            var value = Strings.ResourceManager.GetString(key, _currentCulture);
            return value ?? key;
        }
        catch
        {
            return key;
        }
    }
}
