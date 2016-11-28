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
    private Transform _iKTarget;
    /// <summary>
    /// The reference point IK limbs should attach to.
    /// </summary>
    /// <seealso cref="Transform"/>
    public Transform IKTarget { get { return _iKTarget; } }

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
