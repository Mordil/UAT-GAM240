using UnityEngine;

/// <summary>
/// Class that handles <seealso cref="Weapon"/> instances for characters.
/// </summary>
public class WeaponAgent : MonoBehaviour
{
    /// <summary>
    /// Returns true if there is a reference to a Weapon found.
    /// </summary>
    public bool HasWeaponEquipped { get { return CurrentWeapon != null; } }

    /// <summary>
    /// The current Weapon equipped by the character.
    /// </summary>
    /// <seealso cref="Weapon"/>
    [SerializeField]
    protected Weapon CurrentWeapon;

    /// <summary>
    /// The Animator (if exists) attached to the GameObject.
    /// </summary>
    /// <seealso cref="Animator"/>
    [SerializeField]
    protected Animator AnimatorComponent;
    /// <summary>
    /// The position weapons should be placed and attached to for animations.
    /// </summary>
    /// <seealso cref="Transform"/>
    [SerializeField]
    protected Transform AttachmentPoint;

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
    /// Unity animation lifecycle event.
    /// </summary>
    protected virtual void OnAnimatorIK()
    {
        if (CurrentWeapon != null)
        {
            if (CurrentWeapon.IKTarget != null)
            {
                // give IK data to the animation system
                AnimatorComponent.SetIKPosition(AvatarIKGoal.RightHand, CurrentWeapon.IKTarget.position);
                AnimatorComponent.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
                AnimatorComponent.SetIKRotation(AvatarIKGoal.RightHand, CurrentWeapon.IKTarget.rotation);
                AnimatorComponent.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
            }
            else
            {
                AnimatorComponent.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
                AnimatorComponent.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);
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

        // set the parent in the hierarchy and its transform
        newWeapon.transform.SetParent(AttachmentPoint);
        newWeapon.transform.localPosition = AttachmentPoint.transform.localPosition;
        newWeapon.transform.localRotation = AttachmentPoint.transform.localRotation;

        // store the reference and assign its owner as the master CharacterManager
        CurrentWeapon = newWeapon.GetComponent<Weapon>();
        CurrentWeapon.SetOwner(GetComponent<CharacterManager>());

        // Update the animation index
        AnimatorComponent.SetInteger(AnimationParameters.Arissa.Integers.WEAPON_ANIMATION, (int)CurrentWeapon.AnimationStyle);
    }

    /// <summary>
    /// Returns the currently equipped weapon, or null if one isn't.
    /// </summary>
    /// <returns><see cref="Weapon"/> or null</returns>
    public Weapon GetEquippedWeapon()
    {
        return CurrentWeapon;
    }
}
