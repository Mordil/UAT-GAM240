using UnityEngine;
using UnityEngine.UI;

public class DebugHealthText : MonoBehaviour
{
    [SerializeField]
    private Health _healthComponent;
    [SerializeField]
    private Text _textComponent;

    private void Awake()
    {
        if (_healthComponent == null)
        {
            _healthComponent = GetComponent<Health>();
            
            if (_healthComponent == null)
            {
                this.enabled = false;
            }
        }
    }

    private void Update()
    {
        _textComponent.text = string.Format(
            "Current Health: {0} ({1}%)",
            _healthComponent.CurrentHealth,
            Mathf.RoundToInt(_healthComponent.CurrentHealthPercentage * 100f));
    }
}
