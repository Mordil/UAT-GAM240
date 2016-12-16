using L4.Unity.Common;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Component that manages the Player's HUD.
/// </summary>
public class HUDManager : MonoBehaviour
{
    private GameplayLevel _level;

    [Header("HUD Canvases")]
    [SerializeField]
    private Canvas _gameHUD;
    [SerializeField]
    private Canvas _pauseMenu;

    [Header("Health")]
    [SerializeField]
    private Health _healthComponent;
    [SerializeField]
    private Text _textComponent;

    [Header("Equipment")]
    [SerializeField]
    private Image _equippedWeaponImage;

    private void Awake()
    {
        _level = GameManager.Instance.CurrentScene.As<GameplayLevel>();

        _level.OnLevelPaused.AddListener(() =>
        {
            _gameHUD.gameObject.SetActive(false);
            _pauseMenu.gameObject.SetActive(true);
        });
        _level.OnLevelResumed.AddListener(() =>
        {
            _gameHUD.gameObject.SetActive(true);
            _pauseMenu.gameObject.SetActive(false);
        });

        if (_healthComponent == null)
        {
            _healthComponent = GetComponentInParent<Health>();

            if (_healthComponent == null)
            {
                this.enabled = false;
            }

            _healthComponent.OnKilled.AddListener(() => {  this.gameObject.SetActive(false); });
            _healthComponent.OnHealthGained.AddListener(UpdateHealth);
            _healthComponent.OnHealthLost.AddListener(UpdateHealth);
        }

        var weaponAgent = GetComponentInParent<WeaponAgent>();
        if (weaponAgent != null)
        {
            weaponAgent.OnEquippedWeapon.AddListener((newWeapon) => { _equippedWeaponImage.gameObject.SetActive(true); });
        }
    }

    private void Start()
    {
        UpdateHealth();
    }

    /// <summary>
    /// Button click callback for resuming the game from the pause menu.
    /// </summary>
    public void ResumeButtonClicked()
    {
        _level.Resume();
    }

    /// <summary>
    /// Button click callback for quitting the current game and returning to the main menu from the pause menu.
    /// </summary>
    public void QuitButtonClicked()
    {
        GameManager.Instance.LoadLevel(ProjectSettings.Level.MainMenu);
    }

    private void UpdateHealth()
    {
        _textComponent.text = string.Format(
            "{0} / {1} ({2}%)",
            _healthComponent.CurrentHealth,
            _healthComponent.MaxHealth,
            Mathf.RoundToInt(_healthComponent.CurrentHealthPercentage * 100f));
    }
}
