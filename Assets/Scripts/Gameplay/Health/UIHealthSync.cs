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
    
    private void Awake()
    {
        if (_healthSlider == null)
        {
            _healthSlider = GetComponentInChildren<Slider>();
        }

        if (_healthComponent == null)
        {
            _healthComponent = GetComponentInParent<Health>();

            if (_healthComponent == null)
            {
                this.enabled = false;
            }

            // add event handlers
            _healthComponent.OnKilled.AddListener(() => { this.gameObject.SetActive(false); });
            _healthComponent.OnHealthGained.AddListener(UpdateHealth);
            _healthComponent.OnHealthLost.AddListener(UpdateHealth);
        }
    }
	
	private void Start()
    {
        UpdateHealth();
    }

    private void UpdateHealth()
    {
        _healthSlider.value = _healthComponent.CurrentHealthPercentage;
    }
}
