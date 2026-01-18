namespace WpfRename.Services;

/// <summary>
/// 변수 처리 서비스 인터페이스
/// </summary>
public interface IVariableService
{
    /// <summary>
    /// 문자열에서 변수를 실제 값으로 치환
    /// </summary>
    /// <param name="input">입력 문자열</param>
    /// <param name="index">항목 인덱스 (열거형 변수용)</param>
    /// <param name="filePath">파일 경로 (날짜 변수용)</param>
    /// <returns>변수가 치환된 문자열</returns>
    string ProcessVariables(string input, int index, string? filePath = null);

    /// <summary>
    /// 입력 문자열에 변수가 포함되어 있는지 확인
    /// </summary>
    bool ContainsVariables(string input);
}
