using UnityEngine;
using System.Collections;
using System;

public class HealthPickup : BasePickup
{
    public override PickupType Type { get { return PickupType.Powerup; } }

    [SerializeField]
    private int _healValue;
    public int HealValue { get { return _healValue; } }

    public override void OnPickup(CharacterManager manager)
    {
        manager.Health.GainHealth(_healValue);
        Destroy(this.gameObject);
    }
}
