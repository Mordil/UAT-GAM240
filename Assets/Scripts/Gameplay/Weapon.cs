using UnityEngine;

/// <summary>
/// A generic weapon.
/// </summary>
public class Weapon : MonoBehaviour
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
    private CharacterManager _owner;
    /// <summary>
    /// The character that is the logical owner of this weapon.
    /// </summary>
    /// <seealso cref="CharacterManager"/>
    public CharacterManager Owner { get { return _owner; } }

    [SerializeField]
    private Transform _weaponVisual;
    /// <summary>
    /// The reference point IK limbs should attach to.
    /// </summary>
    /// <seealso cref="Transform"/>
    public Transform WeaponVisual { get { return _weaponVisual; } }

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
        if (_weaponVisual == null)
        {
            // grab the component from the first child
            _weaponVisual = this.gameObject.GetComponentsInChildren<Transform>()[1];
        }
    }

    /// <summary>
    /// Sets the owner of this weapon.
    /// </summary>
    /// <param name="newOwner">The character manager of the character holding this weapon.</param>
    /// <seealso cref="CharacterManager"/>
    public void SetOwner(CharacterManager newOwner)
    {
        _owner = newOwner;
    }
}
