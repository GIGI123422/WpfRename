namespace WpfRename.Services;

/// <summary>
/// 파일 시스템 작업 서비스 인터페이스
/// </summary>
public interface IFileSystemService
{
    /// <summary>
    /// 파일 이름 변경
    /// </summary>
    /// <param name="oldPath">원본 전체 경로</param>
    /// <param name="newPath">새 전체 경로</param>
    void RenameFile(string oldPath, string newPath);

    /// <summary>
    /// 폴더 이름 변경
    /// </summary>
    /// <param name="oldPath">원본 전체 경로</param>
    /// <param name="newPath">새 전체 경로</param>
    void RenameDirectory(string oldPath, string newPath);

    /// <summary>
    /// 파일 존재 여부 확인
    /// </summary>
    bool FileExists(string path);

    /// <summary>
    /// 폴더 존재 여부 확인
    /// </summary>
    bool DirectoryExists(string path);
}
