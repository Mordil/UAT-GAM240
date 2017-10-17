using UnityEngine;
using UnityEngine.Events;

public class HealthAgent : MonoBehaviour, ICharacterAgent
{
    public UnityEvent OnKilled;
    public UnityEvent<float, float> OnHealthChanged;

    [SerializeField]
    private float _currentHealth;
    public float CurrentHealth { get { return _currentHealth; } }

    [SerializeField]
    private float _maxHealth;

    private void Update()
    {
        if (_currentHealth <= 0)
        {
            OnKilled.Invoke();
        }
    }

    public void Initialize() {}

    public void Initialize(float maxHealth)
    {
        _maxHealth = maxHealth;
        _currentHealth = _maxHealth;
    }

    public void ModifyHealth(float amount)
    {
        _currentHealth = Mathf.Clamp(_currentHealth + amount, 0, _maxHealth);
        //OnHealthChanged.Invoke(amount, _currentHealth);
    }
}
