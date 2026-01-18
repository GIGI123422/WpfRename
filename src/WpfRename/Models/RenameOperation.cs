namespace WpfRename.Models;

/// <summary>
/// 이름 변경 작업 기록 (Undo용)
/// </summary>
public class RenameOperation
{
    /// <summary>
    /// 원본 전체 경로
    /// </summary>
    public required string OldPath { get; init; }

    /// <summary>
    /// 변경된 전체 경로
    /// </summary>
    public required string NewPath { get; init; }

    /// <summary>
    /// 폴더 여부
    /// </summary>
    public required bool IsDirectory { get; init; }

    /// <summary>
    /// 작업 성공 여부
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// 오류 메시지 (실패 시)
    /// </summary>
    public string? ErrorMessage { get; set; }
}
