using UnityEngine;

public abstract class AbstractPickup : ScriptableObject
{
    public enum PickupType { Powerup, Weapon }

    public abstract PickupType Type { get; }

    [SerializeField]
    private AudioClip _pickupSFX;
    public AudioClip PickupSFX { get { return _pickupSFX; } }

    [SerializeField]
    private GameObject _pickupPrefab;
    public GameObject PickupPrefab { get { return _pickupPrefab; } }

    public abstract void DoPickup(GameObject target);

    public T As<T>()
        where T : AbstractPickup
    {
        return (T)this;
    }
}
