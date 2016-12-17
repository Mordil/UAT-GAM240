using System;
using UnityEngine;

/// <summary>
/// Master class that manages behaviours for Characters.
/// </summary>
public class CharacterManager : MonoBehaviour
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
    private Animator _animator;
    [SerializeField]
    private Transform _myTransform;

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

        if (_myTransform == null)
        {
            _myTransform = gameObject.transform;
        }
    }

    protected virtual void Update()
    {
        if (_myTransform.position.y <= -10)
        {
            _healthComponent.Kill();
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

        var camera = GetComponentInChildren<Camera>();
        if (camera != null)
        {
            camera.gameObject.SetActive(false);
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
