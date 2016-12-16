using System;
using UnityEngine;

/// <summary>
/// Master class that manages behaviours for Characters.
/// </summary>
/// <seealso cref="IMeleeAttackAnimationHandler"/>
public class CharacterManager : MonoBehaviour, IMeleeAttackAnimationHandler
{
    [SerializeField]
    private Health _healthComponent;
    /// <summary>
    /// The <see cref="Health"/> (if one exists) attached to the GameObject.
    /// </summary>
    public Health Health { get { return _healthComponent; } }

    [SerializeField]
    private WeaponAgent _weaponAgent;
    /// <summary>
    /// The <see cref="WeaponAgent"/> (if one exists) attached to the GameObject.
    /// </summary>
    public WeaponAgent WeaponAgentComponent { get { return _weaponAgent; } }

    [SerializeField]
    [Tooltip("The max distance an object can be in front of this character and still be hit by melee attacks.")]
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
    /// <param name="otherObj">The <see cref="Collider"/> collided with.</param>
    protected void OnTriggerEnter(Collider otherObj)
    {
        GameObject colliderObj = otherObj.gameObject;

        // try handling each discrete case for collision

        if (!TryHandlePickup(colliderObj))
        {
            if (!TryHandleSpell(colliderObj))
            {
                // this is explicitly here for clarity
                return;
            }
        }
    }

    /// <summary>
    /// Kills the character.
    /// </summary>
    public void Kill()
    {
        // Play the animation, disable collision, and schedule invoking the method that will disable this gameobject.
        _animator.SetTrigger(AnimationParameters.Arissa.Triggers.DEATH_ANIMATION);
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

    /// <summary>
    /// Checks if the character has hit a damageable object and deals necessary damage.
    /// </summary>
    /// <seealso cref="IMeleeAttackAnimationHandler.MeleeAttackHitCheck"/>
    public void MeleeAttackHitCheck()
    {
        RaycastHit info;

        // get the origin as the "center", as the origin is down near the feet
        Vector3 offsetOrigin = transform.position + transform.up;
        // get the end position by adding just the max distance to the forward
        Vector3 endPoint = new Vector3(offsetOrigin.x, offsetOrigin.y, offsetOrigin.z + _meleeAttackDistance);

        // do a linecast to see if anything is between us, rather than AT the point casted to
        if (Physics.Linecast(offsetOrigin, endPoint, out info))
        {
            // we hit something, so try to grab a health component
            var health = info.collider.gameObject.GetComponent<Health>();

            // if it has one, and the character has a weapon equipped, get the damage of the weapon and apply it
            if (health != null && WeaponAgentComponent.HasWeaponEquipped)
            {
                health.TakeDamage(WeaponAgentComponent.GetEquippedWeapon().Damage);
            }
        }
    }

    private void HideSelf()
    {
        Destroy(this.gameObject);
    }

    private bool TryHandlePickup(GameObject obj)
    {
        var success = false;
        var pickup = obj.GetComponent<BasePickup>();
        
        if (pickup != null && WeaponAgentComponent != null)
        {
            // since the collided object was a pickup, we'll want to return true
            success = true;

            // call its OnPickup so it handles its logic while we move on to other logic
            pickup.OnPickup(this);

            switch (pickup.Type)
            {
                case BasePickup.PickupType.Weapon:
                    WeaponAgentComponent.HandleNewWeapon(pickup.As<WeaponPickup>());
                    break;

                case BasePickup.PickupType.Powerup:
                    break;

                default:
                    throw new NotImplementedException(String.Format("Unhandled pickup type: {0} for Characters", pickup.Type.ToString()));
            }
        }

        return success;
    }

    private bool TryHandleSpell(GameObject obj)
    {
        var success = false;
        var spell = obj.GetComponentInParent<SpellBase>();

        if (spell != null)
        {
            // since the collided object was a spell, we'll want to return true
            success = true;

            switch (spell.Type)
            {
                case SpellBase.EffectType.Damage:
                    // only handle damage spell collisions if the spell did not come from us
                    var spellCollider = obj.GetComponent<DigitalRuby.PyroParticles.FireCollisionForwardScript>();

                    if (spellCollider != null &&
                        spellCollider.Spawner != this.gameObject)
                    {
                        Health.TakeDamage(spell.As<DamageSpell>().Damage);
                    }
                    break;

                default:
                    throw new NotImplementedException(String.Format("Unhandled spell type: {0} for Characters", spell.Type.ToString()));
            }
        }

        return success;
    }
}
