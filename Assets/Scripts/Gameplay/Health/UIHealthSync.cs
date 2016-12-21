using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Component that handles syncing a health slider UI element with a particular character.
/// </summary>
public class UIHealthSync : MonoBehaviour
{
    [SerializeField]
    private Health _healthComponent;
    [SerializeField]
    private Slider _healthSlider;
    [SerializeField]
    private Text _textComponent;

    private void Awake()
    {
        if (_healthComponent == null)
        {
            _healthComponent = GetComponentInParent<Health>();

            if (_healthComponent == null)
            {
                this.enabled = false;
            }
        }

        if (_healthSlider == null)
        {
            _healthSlider = GetComponentInChildren<Slider>();
        }

        if (_textComponent == null)
        {
            _textComponent = GetComponentInChildren<Text>();
        }

        // add event handlers
        _healthComponent.OnHealthGained.AddListener(UpdateHealth);
        _healthComponent.OnHealthLost.AddListener(UpdateHealth);
    }
	
	private void Start()
    {
        UpdateHealth();
    }

    private void UpdateHealth()
    {
        float healthPercent = _healthComponent.CurrentHealthPercentage;

        _healthSlider.value = healthPercent;

        if (_textComponent != null)
        {
            _textComponent.text = string.Format(
                "{0} / {1} ({2}%)",
                _healthComponent.CurrentHealth,
                _healthComponent.MaxHealth,
                Mathf.RoundToInt(healthPercent * 100f));
        }
    }
}
