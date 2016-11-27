using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    private const int DEFAULT_MAX_HEALTH = 10;

    /// <summary>
    /// The max health this object can reach.
    /// </summary>
    public int MaxHealth { get { return _maxHealth; } }
    /// <summary>
    /// The current health of the object.
    /// </summary>
    public int CurrentHealth { get { return _currentHealth; } }
    /// <summary>
    /// The current health of the object as a percentage (in decimal format).
    /// </summary>
    public float CurrentHealthPercentage { get { return (float)_currentHealth / (float)_maxHealth; } }
    
    /// <summary>
    /// Raised when the object is killed.
    /// </summary>
    [Tooltip("Raised when the object is killed.")]
    public UnityEvent OnKilled;
    /// <summary>
    /// Raised each time the object loses health.
    /// </summary>
    [Tooltip("Raised each time the object loses health.")]
    public UnityEvent OnHealthLost;
    /// <summary>
    /// Raised each time the object gains health.
    /// </summary>
    [Tooltip("Raised each time the object gains health.")]
    public UnityEvent OnHealthGained;

    [SerializeField]
    [Tooltip("Can the health go over the max value.")]
    private bool _allowOverhealing;
    private bool _hasCalledOnKilled;

    [SerializeField]
    [Tooltip("The max health this object can reach.")]
    private int _maxHealth = DEFAULT_MAX_HEALTH;
    [SerializeField]
    [Tooltip("The health level this object will start at.")]
    private int _initialHealth = DEFAULT_MAX_HEALTH;
    [SerializeField]
    [Tooltip("The current health of the object.")]
    private int _currentHealth;

    #region Unity Lifecycle
    private void Awake()
    {
        _currentHealth = _initialHealth;
    }

    private void Update()
    {
        if (_currentHealth <= 0 && !_hasCalledOnKilled)
        {
            OnKilled.Invoke();
            _hasCalledOnKilled = true;
        }
    }
    #endregion

    /// <summary>
    /// Makes the object lose health by the provided amount.
    /// </summary>
    /// <param name="amount">The amount of health to lose.</param>
    public void TakeDamage(int amount)
    {
        SetNewHealth(_currentHealth - amount);
        OnHealthLost.Invoke();
    }

    /// <summary>
    /// Gives the object more health, clamped within its max health unless it is allowed to be overhealed.
    /// </summary>
    /// <param name="amount">The amount of health to gain.</param>
    public void GainHealth(int amount)
    {
        SetNewHealth(_currentHealth + amount);
        OnHealthGained.Invoke();
    }

    /// <summary>
    /// Automatically kills this object.
    /// </summary>
    public void Kill()
    {
        _currentHealth = 0;
        OnHealthLost.Invoke();
    }

    /// <summary>
    /// Resets the object to be at its max health.
    /// </summary>
    public void ResetToFullHealth()
    {
        _currentHealth = _maxHealth;
        OnHealthGained.Invoke();
    }

    private void SetNewHealth(int newValue)
    {
        _currentHealth = (_allowOverhealing) ? newValue : ClampedHealth(newValue);
    }

    private int ClampedHealth(int value)
    {
        return Mathf.Clamp(value, 0, _maxHealth);
    }
}
