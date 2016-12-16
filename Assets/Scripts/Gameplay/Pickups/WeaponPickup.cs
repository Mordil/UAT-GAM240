using UnityEngine;

/// <summary>
/// A pickup that provides a weapon to equip.
/// </summary>
/// <seealso cref="BasePickup"/>
public class WeaponPickup : BasePickup
{
    /// <summary>
    /// Returns <see cref="BasePickup.PickupType.Weapon"/>
    /// </summary>
    /// <seealso cref="BasePickup.PickupType"/>
    public override PickupType Type { get { return PickupType.Weapon; } }
    
    [SerializeField]
    private GameObject _weaponPrefab;
    /// <summary>
    /// The prefab reference to be used for instantiation.
    /// </summary>
    public GameObject WeaponPrefab { get { return _weaponPrefab; } }

    /// <summary>
    /// Destroys this <see cref="GameObject"/>.
    /// </summary>
    /// <param name="manager"></param>
    /// <seealso cref="BasePickup.OnPickup(CharacterManager)"/>
    public override void OnPickup(CharacterManager manager)
    {
        Destroy(this.gameObject);
    }
}
