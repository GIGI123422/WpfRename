using System.IO;
using System.Text.Json;
using WpfRename.Models;

namespace WpfRename.Services;

/// <summary>
/// 프리셋 저장/로드 서비스 구현 (JSON 파일 사용)
/// </summary>
public class PresetService : IPresetService
{
    private readonly string _presetsFilePath;
    private readonly JsonSerializerOptions _jsonOptions;

    public PresetService()
    {
        // 사용자 AppData 폴더에 저장
        var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        var appFolder = Path.Combine(appDataPath, "WpfRename");
        Directory.CreateDirectory(appFolder);

        _presetsFilePath = Path.Combine(appFolder, "presets.json");

        _jsonOptions = new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNameCaseInsensitive = true
        };
    }

    /// <summary>
    /// 모든 프리셋 로드
    /// </summary>
    public async Task<List<Preset>> LoadPresetsAsync()
    {
        try
        {
            if (!File.Exists(_presetsFilePath))
            {
                return [];
            }

            var json = await File.ReadAllTextAsync(_presetsFilePath);
            var presets = JsonSerializer.Deserialize<List<Preset>>(json, _jsonOptions);
            return presets ?? [];
        }
        catch (Exception)
        {
            // 파일 읽기 오류 시 빈 목록 반환
            return [];
        }
    }

    /// <summary>
    /// 프리셋 저장
    /// </summary>
    public async Task SavePresetAsync(Preset preset)
    {
        var presets = await LoadPresetsAsync();

        // 같은 이름의 프리셋이 있으면 업데이트
        var existing = presets.FirstOrDefault(p => p.Name == preset.Name);
        if (existing != null)
        {
            presets.Remove(existing);
        }

        presets.Add(preset);

        // 이름 순 정렬
        presets = presets.OrderBy(p => p.Name).ToList();

        var json = JsonSerializer.Serialize(presets, _jsonOptions);
        await File.WriteAllTextAsync(_presetsFilePath, json);
    }

    /// <summary>
    /// 프리셋 삭제
    /// </summary>
    public async Task DeletePresetAsync(string presetName)
    {
        var presets = await LoadPresetsAsync();
        var preset = presets.FirstOrDefault(p => p.Name == presetName);

        if (preset != null)
        {
            presets.Remove(preset);
            var json = JsonSerializer.Serialize(presets, _jsonOptions);
            await File.WriteAllTextAsync(_presetsFilePath, json);
        }
    }

    /// <summary>
    /// 프리셋 존재 여부 확인
    /// </summary>
    public async Task<bool> PresetExistsAsync(string presetName)
    {
        var presets = await LoadPresetsAsync();
        return presets.Any(p => p.Name == presetName);
    }
}
