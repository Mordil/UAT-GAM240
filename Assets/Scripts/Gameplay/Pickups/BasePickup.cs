using UnityEngine;
using System.Collections;

public abstract class BasePickup : MonoBehaviour
{
    public enum PickupType { Powerup, Weapon, Armor }

    public abstract PickupType Type { get; }

    public abstract void OnPickup(CharacterManager manager);

    public T As<T>()
        where T : BasePickup
    {
        return (T)this;
    }
}
