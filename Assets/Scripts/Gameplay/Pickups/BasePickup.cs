using UnityEngine;

/// <summary>
/// Base class for all pickup items.
/// </summary>
public abstract class BasePickup : MonoBehaviour
{
    /// <summary>
    /// The type of pickup this object represents.
    /// </summary>
    public enum PickupType
    {
        /// <summary>
        /// A powerup such as a damage buff, health, etc.
        /// </summary>
        Powerup,
        /// <summary>
        /// An equippable weapon.
        /// </summary>
        Weapon,
        /// <summary>
        /// An equippable piece of armor.
        /// </summary>
        Armor
    }

    /// <summary>
    /// The type this pickup represents.
    /// </summary>
    public abstract PickupType Type { get; }

    [SerializeField]
    private AudioSource _audioSource;

    [SerializeField]
    private AudioClip _pickupSFX;

    /// <summary>
    /// Unity component lifecycle event for initialization.
    /// </summary>
    protected virtual void Awake()
    {
        if (_audioSource == null)
        {
            _audioSource = GetComponentInChildren<AudioSource>();
        }
    }

    /// <summary>
    /// Method for applying logic for the specific pickup type.
    /// </summary>
    /// <param name="manager">The <see cref="CharacterManager"/> responsible for the character that picked up this item.</param>
    public virtual void OnPickup(CharacterManager manager)
    {
        AudioSource.PlayClipAtPoint(_pickupSFX, transform.position);
    }

    /// <summary>
    /// Casts this pickup to the type provided.
    /// </summary>
    /// <typeparam name="T">The type of Pickup to cast to.</typeparam>
    /// <returns>This as the type desired.</returns>
    public T As<T>()
        where T : BasePickup
    {
        return (T)this;
    }
}
