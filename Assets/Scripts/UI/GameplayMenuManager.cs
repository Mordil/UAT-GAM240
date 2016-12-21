using L4.Unity.Common;
using UnityEngine;

public class GameplayMenuManager : MonoBehaviour
{
    private GameplayLevel _level;

    [Header("Canvases")]
    [SerializeField]
    private Canvas _gameHUD;
    [SerializeField]
    private Canvas _pauseMenu;
    [SerializeField]
    private Canvas _settingsCanvas;

    [Header("Audio SFX")]
    [SerializeField]
    private AudioSource _audioSource;

    private void Awake()
    {
        _level = GameManager.Instance.CurrentScene.As<GameplayLevel>();

        _level.OnLevelPaused.AddListener(() =>
        {
            _audioSource.Play();
            ShowPauseMenu();
        });
        _level.OnLevelResumed.AddListener(() =>
        {
            _audioSource.Play();
            _gameHUD.gameObject.SetActive(true);
            _pauseMenu.gameObject.SetActive(false);
            _settingsCanvas.gameObject.SetActive(false);
        });

        var healthComponent = GetComponentInParent<Health>();

        if (healthComponent == null)
        {
            this.enabled = false;
        }

        healthComponent.OnKilled.AddListener(() => { this.gameObject.SetActive(false); });
    }

    /// <summary>
    /// <see cref="Button"/> click callback for resuming the game from the pause menu.
    /// </summary>
    public void ResumeButtonClicked()
    {
        _level.Resume();
    }

    /// <summary>
    /// <see cref="Button"/> click callback for quitting the current game and returning to the main menu from the pause menu.
    /// </summary>
    public void QuitButtonClicked()
    {
        GameManager.Instance.LoadLevel(ProjectSettings.Level.MainMenu);
    }

    /// <summary>
    /// <see cref="Button"/> click callback for changing pause menus to the settings.
    /// </summary>
    public void OpenSettingsMenu()
    {
        _pauseMenu.gameObject.SetActive(false);
        _settingsCanvas.gameObject.SetActive(true);
    }

    /// <summary>
    /// <see cref="Button"/> click callback for returning to the main pause menu.
    /// </summary>
    public void ShowPauseMenu()
    {
        _gameHUD.gameObject.SetActive(false);
        _settingsCanvas.gameObject.SetActive(false);
        _pauseMenu.gameObject.SetActive(true);
    }
}
