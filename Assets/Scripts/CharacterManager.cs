using UnityEngine;
using System.Collections;
using System;

public class CharacterManager : MonoBehaviour
{
    [SerializeField]
    protected Health HealthComponent;
    [SerializeField]
    protected WeaponAgent WeaponAgentComponent;

    /// <summary>
    /// Unity lifecycle event.
    /// </summary>
    protected virtual void Awake()
    {
        if (HealthComponent == null)
        {
            HealthComponent = GetComponent<Health>();
        }

        if (WeaponAgentComponent == null)
        {
            WeaponAgentComponent = GetComponent<WeaponAgent>();
        }
    }

    /// <summary>
    /// Unity event callback when a trigger has collided with this object.
    /// </summary>
    /// <param name="otherObj">The object collided with.</param>
    protected void OnTriggerEnter(Collider otherObj)
    {
        var pickup = otherObj.GetComponent<BasePickup>();

        if (pickup != null)
        {
            HandlePickup(pickup);
        }
    }

    /// <summary>
    /// Kills the character.
    /// </summary>
    public void Kill()
    {
        this.gameObject.SetActive(false);
    }

    private void HandlePickup(BasePickup pickup)
    {
        pickup.OnPickup(this);
        BasePickup.PickupType type = pickup.Type;

        switch (type)
        {
            case BasePickup.PickupType.Weapon:
                WeaponAgentComponent.HandleNewWeapon(pickup.As<WeaponPickup>());
                break;

            default:
                throw new NotImplementedException();
        }
    }
}
