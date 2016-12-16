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
    [Tooltip("This object will be a sibling to the weapon, as it is used as a reference for the weapon's transform.")]
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
