using UnityEngine;
using System.Collections;
using L4.Unity.Common;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    private int _healthCache;
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
    [SerializeField]
    private Slider _healthSlider;

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
        }

        if (_healthSlider == null)
        {
            _healthSlider = GetComponentInChildren<Slider>();
        }
    }

    private void Update()
    {
        HealthUpdate();
    }

    public void ResumeButtonClicked()
    {
        _level.Resume();
    }

    private void HealthUpdate()
    {
        int health = _healthComponent.CurrentHealth;
        if (health != _healthCache)
        {
            _healthCache = health;

            var healthPercentage = _healthComponent.CurrentHealthPercentage;

            _textComponent.text = string.Format(
                "{0} / {1} ({2}%)",
                _healthComponent.CurrentHealth,
                _healthComponent.MaxHealth,
                Mathf.RoundToInt(healthPercentage * 100f));

            _healthSlider.value = healthPercentage;
        }
    }
}
