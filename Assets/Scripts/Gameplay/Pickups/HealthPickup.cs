using UnityEngine;

/// <summary>
/// A pickup that adds to the health of any character that collides with it.
/// </summary>
/// <seealso cref="BasePickup"/>
public class HealthPickup : BasePickup
{
    /// <summary>
    /// Returns <see cref="BasePickup.PickupType.Powerup"/>.
    /// </summary>
    /// <seealso cref="BasePickup.PickupType"/>
    public override PickupType Type { get { return PickupType.Powerup; } }

    [SerializeField]
    [Tooltip("The amount of health characters will gain.")]
    private int _healValue;
    /// <summary>
    /// The amount of health this pickup provides.
    /// </summary>
    public int HealValue { get { return _healValue; } }

    /// <summary>
    /// Gives the character health before destroying this <see cref="GameObject"/>.
    /// </summary>
    /// <param name="manager">The <see cref="CharacterManager"/> responsible for the character that picked up this item.</param>
    /// <seealso cref="BasePickup.OnPickup(CharacterManager)"/>
    public override void OnPickup(CharacterManager manager)
    {
        manager.Health.GainHealth(_healValue);
        Destroy(this.gameObject);
    }
}
