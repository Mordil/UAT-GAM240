using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [SerializeField]
    private Button _applyButton;

    [Header("Audio UI Elements")]
    [SerializeField]
    private AudioMixer _mainMixer;
    [SerializeField]
    private AnimationCurve _volumeDecibelCurve;
    [SerializeField]
    private Slider _masterVolume;
    [SerializeField]
    private Slider _musicVolume;
    [SerializeField]
    private Slider _sfxVolume;
    [SerializeField]
    private Slider _ambientVolume;

    [Header("Video UI Elements")]
    [SerializeField]
    private Dropdown _resolutionDropdown;
    [SerializeField]
    private Dropdown _qualityDropdown;
    [SerializeField]
    private Toggle _fullscreenToggle;
    [SerializeField]
    private Toggle _vsyncToggle;

    private List<string> _availableResolutions;

    private void Awake()
    {
        BuildScreenResolutionsOptions();
        BuildQualityOptions();
    }

    private void OnEnable()
    {
        // audio
        _masterVolume.value = PlayerPrefs.GetFloat(ProjectSettings.PlayerPrefs.MASTER_VOLUME, _masterVolume.maxValue);
        _musicVolume.value = PlayerPrefs.GetFloat(ProjectSettings.PlayerPrefs.MUSIC_VOLUME, _musicVolume.maxValue);
        _sfxVolume.value = PlayerPrefs.GetFloat(ProjectSettings.PlayerPrefs.SFX_VOLUME, _sfxVolume.maxValue);
        _ambientVolume.value = PlayerPrefs.GetFloat(ProjectSettings.PlayerPrefs.AMBIENT_VOLUME, _ambientVolume.maxValue);

        // video
        _fullscreenToggle.isOn = Screen.fullScreen;
        _qualityDropdown.value = QualitySettings.GetQualityLevel();
        _vsyncToggle.isOn = QualitySettings.vSyncCount != 0;
        _resolutionDropdown.value = _availableResolutions.IndexOf(string.Format("{0} x {1}", Screen.currentResolution.width, Screen.currentResolution.height));

        _applyButton.interactable = false;
    }

    /// <summary>
    /// Enables the apply <see cref="Button"/> for the user to click.
    /// </summary>
    public void SetDirty()
    {
        if (!_applyButton.interactable)
        {
            _applyButton.interactable = true;
        }
    }

    /// <summary>
    /// <see cref="Button"/> callback that saves all settings to <see cref="PlayerPrefs"/> and returns the user to the main pause menu.
    /// </summary>
    public void ApplyChanges()
    {
        ApplyVolumeChanges();

        QualitySettings.SetQualityLevel(_qualityDropdown.value);
        Screen.fullScreen = _fullscreenToggle.isOn;
        QualitySettings.vSyncCount = (_vsyncToggle.isOn) ? 1 : 0;

        string selectedResolution = _availableResolutions[_resolutionDropdown.value];
        string[] resolutionDetails = selectedResolution.Split('x');
        
        Screen.SetResolution(
            int.Parse(resolutionDetails[0]),
            int.Parse(resolutionDetails[1]),
            _fullscreenToggle.isOn);

        _applyButton.interactable = false;
    }

    private void BuildScreenResolutionsOptions()
    {
        _resolutionDropdown.ClearOptions();

        _availableResolutions = new List<string>();

        for (int index = 0; index < Screen.resolutions.Length; index++)
        {
            _availableResolutions.Add(string.Format("{0} x {1}", Screen.resolutions[index].width, Screen.resolutions[index].height));
        }

        _availableResolutions = _availableResolutions
            .Distinct()
            .ToList();

        _resolutionDropdown.AddOptions(_availableResolutions);
    }

    private void BuildQualityOptions()
    {
        _qualityDropdown.ClearOptions();
        _qualityDropdown.AddOptions(QualitySettings.names.ToList());
    }

    private void ApplyVolumeChanges()
    {
        // change values on audio mixer
        _mainMixer.SetFloat(ProjectSettings.AudioMixers.Main.Parameters.MASTER_VOLUME, _volumeDecibelCurve.Evaluate(_masterVolume.value));
        _mainMixer.SetFloat(ProjectSettings.AudioMixers.Main.Parameters.MUSIC_VOLUME, _volumeDecibelCurve.Evaluate(_musicVolume.value));
        _mainMixer.SetFloat(ProjectSettings.AudioMixers.Main.Parameters.SFX_VOLUME, _volumeDecibelCurve.Evaluate(_sfxVolume.value));
        _mainMixer.SetFloat(ProjectSettings.AudioMixers.Main.Parameters.AMBIENT_VOLUME, _volumeDecibelCurve.Evaluate(_ambientVolume.value));

        // save player prefs
        PlayerPrefs.SetFloat(ProjectSettings.PlayerPrefs.MASTER_VOLUME, _masterVolume.value);
        PlayerPrefs.SetFloat(ProjectSettings.PlayerPrefs.MUSIC_VOLUME, _musicVolume.value);
        PlayerPrefs.SetFloat(ProjectSettings.PlayerPrefs.SFX_VOLUME, _sfxVolume.value);
        PlayerPrefs.SetFloat(ProjectSettings.PlayerPrefs.AMBIENT_VOLUME, _ambientVolume.value);
    }
}
