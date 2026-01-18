using WpfRename.Models;

namespace WpfRename.Services;

/// <summary>
/// 프리셋 저장/로드 서비스 인터페이스
/// </summary>
public interface IPresetService
{
    /// <summary>
    /// 모든 프리셋 로드
    /// </summary>
    Task<List<Preset>> LoadPresetsAsync();

    /// <summary>
    /// 프리셋 저장
    /// </summary>
    Task SavePresetAsync(Preset preset);

    /// <summary>
    /// 프리셋 삭제
    /// </summary>
    Task DeletePresetAsync(string presetName);

    /// <summary>
    /// 프리셋 존재 여부 확인
    /// </summary>
    Task<bool> PresetExistsAsync(string presetName);
}
