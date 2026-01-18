using System.IO;

namespace WpfRename.Services;

/// <summary>
/// 파일 시스템 작업 서비스 구현
/// </summary>
public class FileSystemService : IFileSystemService
{
    /// <summary>
    /// 파일 이름 변경
    /// </summary>
    public void RenameFile(string oldPath, string newPath)
    {
        if (!File.Exists(oldPath))
        {
            throw new FileNotFoundException($"파일을 찾을 수 없습니다: {oldPath}");
        }

        if (File.Exists(newPath))
        {
            throw new IOException($"대상 파일이 이미 존재합니다: {newPath}");
        }

        File.Move(oldPath, newPath);
    }

    /// <summary>
    /// 폴더 이름 변경
    /// </summary>
    public void RenameDirectory(string oldPath, string newPath)
    {
        if (!Directory.Exists(oldPath))
        {
            throw new DirectoryNotFoundException($"폴더를 찾을 수 없습니다: {oldPath}");
        }

        if (Directory.Exists(newPath))
        {
            throw new IOException($"대상 폴더가 이미 존재합니다: {newPath}");
        }

        Directory.Move(oldPath, newPath);
    }

    /// <summary>
    /// 파일 존재 여부 확인
    /// </summary>
    public bool FileExists(string path) => File.Exists(path);

    /// <summary>
    /// 폴더 존재 여부 확인
    /// </summary>
    public bool DirectoryExists(string path) => Directory.Exists(path);
}
