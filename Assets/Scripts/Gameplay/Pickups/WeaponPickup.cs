using System;
using UnityEngine;

public class WeaponPickup : BasePickup
{
    public override PickupType Type { get { return PickupType.Weapon; } }
    
    [SerializeField]
    private GameObject _weaponPrefab;
    public GameObject WeaponPrefab { get { return _weaponPrefab; } }

    public override void OnPickup(CharacterManager manager)
    {
        Destroy(this.gameObject);
    }
}
