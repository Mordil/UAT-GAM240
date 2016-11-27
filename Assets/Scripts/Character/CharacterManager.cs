using UnityEngine;
using System.Collections;
using System;

public class CharacterManager : MonoBehaviour
{
    [SerializeField]
    private Health _healthComponent;
    public Health Health { get { return _healthComponent; } }

    [SerializeField]
    protected WeaponAgent WeaponAgentComponent;

    /// <summary>
    /// Unity lifecycle event.
    /// </summary>
    protected virtual void Awake()
    {
        if (_healthComponent == null)
        {
            _healthComponent = GetComponent<Health>();
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
        GameObject colliderObj = otherObj.gameObject;

        if (!TryHandlePickup(colliderObj))
        {
            if (!TryHandleDamage(colliderObj))
            {
                return;
            }
        }
    }

    /// <summary>
    /// Kills the character.
    /// </summary>
    public void Kill()
    {
        this.gameObject.SetActive(false);
    }

    private bool TryHandlePickup(GameObject obj)
    {
        var success = false;
        var pickup = obj.GetComponent<BasePickup>();

        if (pickup != null)
        {
            pickup.OnPickup(this);
            BasePickup.PickupType type = pickup.Type;
            success = true;

            switch (type)
            {
                case BasePickup.PickupType.Weapon:
                    WeaponAgentComponent.HandleNewWeapon(pickup.As<WeaponPickup>());
                    break;

                case BasePickup.PickupType.Powerup:
                    break;

                default:
                    throw new NotImplementedException(String.Format("Unhandled pickup type: {0} for Characters", type.ToString()));
            }
        }

        return success;
    }

    private bool TryHandleDamage(GameObject obj)
    {
        var success = false;
        var weapon = obj.GetComponent<Weapon>();

        if (weapon != null)
        {
            success = true;
            Health.TakeDamage(weapon.Damage);
        }

        return success;
    }
}
