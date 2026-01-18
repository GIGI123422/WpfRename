using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;

namespace WpfRename.Services;

/// <summary>
/// 변수 처리 서비스 구현
/// </summary>
public partial class VariableService : IVariableService
{
    // 변수 패턴: ${변수명} 또는 ${변수명:옵션}
    [GeneratedRegex(@"\$\{([^:}]+)(?::([^}]+))?\}", RegexOptions.Compiled)]
    private static partial Regex VariablePattern();

    /// <summary>
    /// 입력 문자열에 변수가 포함되어 있는지 확인
    /// </summary>
    public bool ContainsVariables(string input)
    {
        return VariablePattern().IsMatch(input);
    }

    /// <summary>
    /// 문자열에서 변수를 실제 값으로 치환
    /// </summary>
    public string ProcessVariables(string input, int index, string? filePath = null)
    {
        if (string.IsNullOrEmpty(input) || !ContainsVariables(input))
        {
            return input;
        }

        return VariablePattern().Replace(input, match =>
        {
            var variableName = match.Groups[1].Value.ToLowerInvariant();
            var options = match.Groups[2].Success ? match.Groups[2].Value : null;

            return variableName switch
            {
                "count" => ProcessCountVariable(index, options),
                "now" => ProcessNowVariable(options),
                "created" => ProcessCreatedVariable(filePath, options),
                "modified" => ProcessModifiedVariable(filePath, options),
                _ => match.Value // 알 수 없는 변수는 그대로 반환
            };
        });
    }

    /// <summary>
    /// 열거형 변수 처리: ${count} 또는 ${count:start=10,step=2,digits=3}
    /// </summary>
    private string ProcessCountVariable(int index, string? options)
    {
        int start = 1;
        int step = 1;
        int digits = 3;

        if (!string.IsNullOrEmpty(options))
        {
            foreach (var option in options.Split(','))
            {
                var parts = option.Split('=');
                if (parts.Length != 2) continue;

                var key = parts[0].Trim().ToLowerInvariant();
                var value = parts[1].Trim();

                switch (key)
                {
                    case "start":
                        if (int.TryParse(value, out var s)) start = s;
                        break;
                    case "step":
                        if (int.TryParse(value, out var st)) step = st;
                        break;
                    case "digits":
                        if (int.TryParse(value, out var d)) digits = d;
                        break;
                }
            }
        }

        var count = start + (index * step);
        return count.ToString($"D{digits}");
    }

    /// <summary>
    /// 현재 날짜/시간 변수 처리: ${now} 또는 ${now:yyyy-MM-dd}
    /// </summary>
    private string ProcessNowVariable(string? format)
    {
        var now = DateTime.Now;
        return string.IsNullOrEmpty(format)
            ? now.ToString("yyyy-MM-dd_HHmmss")
            : now.ToString(format);
    }

    /// <summary>
    /// 파일 생성일 변수 처리: ${created} 또는 ${created:yyyy}
    /// </summary>
    private string ProcessCreatedVariable(string? filePath, string? format)
    {
        if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
        {
            return "${created}"; // 파일이 없으면 변수 그대로 반환
        }

        try
        {
            var created = File.GetCreationTime(filePath);
            return string.IsNullOrEmpty(format)
                ? created.ToString("yyyy-MM-dd")
                : created.ToString(format);
        }
        catch
        {
            return "${created}";
        }
    }

    /// <summary>
    /// 파일 수정일 변수 처리: ${modified} 또는 ${modified:yyyyMMdd}
    /// </summary>
    private string ProcessModifiedVariable(string? filePath, string? format)
    {
        if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
        {
            return "${modified}"; // 파일이 없으면 변수 그대로 반환
        }

        try
        {
            var modified = File.GetLastWriteTime(filePath);
            return string.IsNullOrEmpty(format)
                ? modified.ToString("yyyy-MM-dd")
                : modified.ToString(format);
        }
        catch
        {
            return "${modified}";
        }
    }
}
