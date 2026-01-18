namespace WpfRename.Models;

/// <summary>
/// 검색/치환 프리셋
/// </summary>
public class Preset
{
    /// <summary>
    /// 프리셋 이름
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// 검색 패턴
    /// </summary>
    public string SearchPattern { get; set; } = string.Empty;

    /// <summary>
    /// 치환 패턴
    /// </summary>
    public string ReplacePattern { get; set; } = string.Empty;

    /// <summary>
    /// 정규식 사용 여부
    /// </summary>
    public bool UseRegex { get; set; }

    /// <summary>
    /// 대소문자 구분 여부
    /// </summary>
    public bool CaseSensitive { get; set; }

    /// <summary>
    /// 대소문자 변환 인덱스
    /// </summary>
    public int CaseTransformIndex { get; set; }

    /// <summary>
    /// 생성 날짜
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    /// <summary>
    /// 설명 (선택)
    /// </summary>
    public string? Description { get; set; }
}
