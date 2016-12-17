using System;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Class that handles <seealso cref="Weapon"/> instances for characters.
/// </summary>
/// <seealso cref="IMeleeAttackAnimationHandler"/>
public class WeaponAgent : MonoBehaviour, IMeleeAttackAnimationHandler
{
    /// <summary>
    /// A <see cref="UnityEvent"/> that emits a <see cref="Weapon"/> object.
    /// </summary>
    [Serializable]
    public class WeaponEvent : UnityEvent<Weapon> { }

    /// <summary>
    /// Returns true if there is a reference to a Weapon found.
    /// </summary>
    public bool HasWeaponEquipped { get { return CurrentWeapon != null; } }

    /// <summary>
    /// Event emitted when a new <see cref="Weapon"/> has been equipped.
    /// </summary>
    public WeaponEvent OnEquippedWeapon;

    /// <summary>
    /// The current <see cref="Weapon"/> equipped by the character.
    /// </summary>
    [SerializeField]
    protected Weapon CurrentWeapon;

    /// <summary>
    /// The <see cref="Animator"/> (if exists) attached to the GameObject.
    /// </summary>
    [SerializeField]
    protected Animator AnimatorComponent;
    /// <summary>
    /// The position weapons should be placed and attached to for animations.
    /// </summary>
    /// <seealso cref="Transform"/>
    [SerializeField]
    [Tooltip("This object will be a sibling to the weapon, as it is used as a reference for the weapon's transform.")]
    protected Transform AttachmentPoint;
    
    [SerializeField]
    [Tooltip("The max distance an object can be in front of this character and still be hit by melee attacks.")]
    private float _meleeAttackDistance = 2f;

    [SerializeField]
    private GameObject _bloodSplatterFX;

    /// <summary>
    /// Unity lifecycle event.
    /// </summary>
    protected virtual void Awake()
    {
        if (AnimatorComponent == null)
        {
            AnimatorComponent = GetComponent<Animator>();
        }

        // set the default animation index
        var index = (CurrentWeapon != null) ? (int)CurrentWeapon.AnimationStyle : 0;
        AnimatorComponent.SetInteger(AnimationParameters.Arissa.Integers.WEAPON_ANIMATION, index);
    }

    /// <summary>
    /// Checks if the character has hit a damageable object and deals necessary damage.
    /// </summary>
    /// <seealso cref="IMeleeAttackAnimationHandler.MeleeAttackHitCheck"/>
    public void MeleeAttackHitCheck()
    {
        RaycastHit info;
        Transform myTransform = transform;

        // get the origin as the "center", as the origin is down near the feet
        Vector3 offsetOrigin = myTransform.position + myTransform.up;
        // get the end position by multiplying forward by the distance and add the current position to offset it.
        Vector3 endPoint = myTransform.forward * _meleeAttackDistance + myTransform.position;

        // do a linecast to see if anything is between us, rather than AT the point casted to
        if (Physics.Linecast(offsetOrigin, endPoint, out info))
        {
            // we hit something, so try to grab a health component
            var health = info.collider.gameObject.GetComponent<Health>();

            // if it has one, and the character has a weapon equipped, get the damage of the weapon and apply it
            if (health != null && HasWeaponEquipped)
            {
                var bloodSplatter = Instantiate(_bloodSplatterFX, endPoint, myTransform.rotation) as GameObject;
                Destroy(bloodSplatter, 1.5f);

                health.TakeDamage(CurrentWeapon.Damage);
            }
        }
    }

    /// <summary>
    /// Does necessary work for equipping a new <see cref="Weapon"/>.
    /// </summary>
    /// <param name="pickup">The weapon picked up.</param>
    /// <seealso cref="WeaponPickup"/>
    public void HandleNewWeapon(WeaponPickup pickup)
    {
        var prefab = pickup.As<WeaponPickup>().WeaponPrefab;
        var newWeapon = Instantiate(prefab) as GameObject;

        // store the reference and assign its owner as the master CharacterManager
        CurrentWeapon = newWeapon.GetComponent<Weapon>();
        CurrentWeapon.SetOwner(GetComponent<CharacterManager>());

        // set the parent in the hierarchy and its transform
        newWeapon.transform.SetParent(AttachmentPoint.parent);
        newWeapon.transform.localPosition = AttachmentPoint.transform.localPosition;
        newWeapon.transform.localRotation = AttachmentPoint.transform.localRotation;

        // Updates the weapon's visual based on the desired settings.
        var rotation = CurrentWeapon.WeaponVisual.transform.localRotation;
        rotation *= Quaternion.Euler(CurrentWeapon.DesiredLocalRotation);

        CurrentWeapon.WeaponVisual.transform.localPosition = new Vector3(0, 0, 0);
        CurrentWeapon.WeaponVisual.transform.localRotation = rotation;

        // Update the animation index
        AnimatorComponent.SetInteger(AnimationParameters.Arissa.Integers.WEAPON_ANIMATION, (int)CurrentWeapon.AnimationStyle);

        // emit the new weapon event
        OnEquippedWeapon.Invoke(CurrentWeapon);
    }

    /// <summary>
    /// Returns the currently equipped <see cref="Weapon"/>, or null if one isn't.
    /// </summary>
    /// <returns><see cref="Weapon"/> or null</returns>
    public Weapon GetEquippedWeapon()
    {
        return CurrentWeapon;
    }
}
