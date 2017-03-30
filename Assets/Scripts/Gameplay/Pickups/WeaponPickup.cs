using UnityEngine;

/// <summary>
/// A pickup that provides a weapon to equip.
/// </summary>
/// <seealso cref="BasePickup"/>
[ExecuteInEditMode]
public class WeaponPickup : BasePickup
{
    /// <summary>
    /// Returns <see cref="BasePickup.PickupType.Weapon"/>
    /// </summary>
    /// <seealso cref="BasePickup.PickupType"/>
    public override PickupType Type { get { return PickupType.Weapon; } }
    
    [SerializeField]
    private Weapon _availableWeapon;
    /// <summary>
    /// The weapon data object to be used for instantiation.
    /// </summary>
    public Weapon AvailableWeapon { get { return _availableWeapon; } }
    
    private void Awake()
    {
        if (this.gameObject.transform.childCount == 0)
        {
            Instantiate(_availableWeapon.WeaponVisual, this.transform, false);
        }
    }

    /// <summary>
    /// Destroys this <see cref="GameObject"/>.
    /// </summary>
    /// <param name="manager"></param>
    /// <seealso cref="BasePickup.OnPickup(CharacterManager)"/>
    public override void OnPickup(CharacterManager manager)
    {
        base.OnPickup(manager);

        Destroy(this.gameObject);
    }
}
