using UnityEngine;

/// <summary>
/// A generic weapon.
/// </summary>
[CreateAssetMenu(fileName = "NewWeapon", menuName = "Pickups/Weapon")]
public class Weapon : ScriptableObject
{
    /// <summary>
    /// The index values for different weapon animation styles.
    /// </summary>
    public enum AnimationIndex { None, OneHandedSword }

    [SerializeField]
    private int _damage;
    /// <summary>
    /// The amount of damage this weapon deals.
    /// </summary>
    public int Damage { get { return _damage; } }

    [SerializeField]
    private AnimationIndex _animationStyle;
    /// <summary>
    /// The animation style this weapon works with.
    /// </summary>
    /// <seealso cref="AnimationIndex"/>
    public AnimationIndex AnimationStyle { get { return _animationStyle; } }

    [SerializeField]
    private GameObject _weaponVisual;
    /// <summary>
    /// The reference point IK limbs should attach to.
    /// </summary>
    /// <seealso cref="Transform"/>
    public GameObject WeaponVisual { get { return _weaponVisual; } }

    [SerializeField]
    [Tooltip("The ideal local rotation of the visual when equipped.")]
    private Vector3 _desiredLocalRotation;
    /// <summary>
    /// The ideal local rotation of the visual when equipped.
    /// </summary>
    public Vector3 DesiredLocalRotation { get { return _desiredLocalRotation; } }

    [SerializeField]
    private AudioClip _hitSFX;
    /// <summary>
    /// The audio clip to play as a sound effect when the weapon hits a target.
    /// </summary>
    public AudioClip HitSFX { get { return _hitSFX; } }

    private void Awake()
    {
        Debug.Assert(
            _weaponVisual != null,
            string.Format("Weapon {0} is missing a weapon visual!", this.name));
    }
}
