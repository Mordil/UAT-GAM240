using UnityEngine;
using System.Collections;
using System;

public class CharacterManager : MonoBehaviour
{
    private struct SpellNames
    {
        public const string FIREBALL = "fireball";
    }

    private const string DEATH_ANIMATION_TRIGGER = "Has Died";

    [SerializeField]
    private Health _healthComponent;
    public Health Health { get { return _healthComponent; } }

    [SerializeField]
    private WeaponAgent _weaponAgent;
    public WeaponAgent WeaponAgentComponent { get { return _weaponAgent; } }

    [SerializeField]
    private float _meleeAttackDistance = 2f;

    [SerializeField]
    private Animator _animator;

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
            _weaponAgent = GetComponent<WeaponAgent>();
        }

        if (_animator == null)
        {
            _animator = GetComponent<Animator>();
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
            return;
        }
    }

    /// <summary>
    /// Kills the character.
    /// </summary>
    public void Kill()
    {
        _animator.SetTrigger(DEATH_ANIMATION_TRIGGER);
        Invoke("HideSelf", 3);

        var rigidbody = GetComponent<Rigidbody>();
        if (rigidbody != null)
        {
            rigidbody.isKinematic = true;
        }

        var collider = GetComponent<Collider>();
        if (collider != null)
        {
            collider.enabled = false;
        }
    }

    private void MeleeAttackHitCheck()
    {
        RaycastHit info;
        Vector3 offsetOrigin = transform.position + transform.up;
        Vector3 endPoint = new Vector3(offsetOrigin.x, offsetOrigin.y, offsetOrigin.z + _meleeAttackDistance);

        if (Physics.Linecast(offsetOrigin, endPoint, out info))
        {
            var health = info.collider.gameObject.GetComponent<Health>();
            if (health != null && WeaponAgentComponent.HasWeaponEquipped)
            {
                health.TakeDamage(WeaponAgentComponent.GetEquippedWeapon().Damage);
            }
        }
    }

    private void HideSelf()
    {
        this.gameObject.SetActive(false);
    }

    private void SpellcastAnimationFinished(string spellName)
    {
        switch (spellName)
        {
            case SpellNames.FIREBALL:
                break;

            default:
                throw new NotImplementedException();
        }
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
}
