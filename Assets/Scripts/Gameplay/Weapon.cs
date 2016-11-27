using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{
    public enum AnimationIndex { None, OneHandedSword }

    [SerializeField]
    private int _damage;
    public int Damage { get { return _damage; } }

    [SerializeField]
    private AnimationIndex _animation;
    public AnimationIndex Animation { get { return _animation; } }

    [SerializeField]
    private CharacterManager _owner;
    public CharacterManager Owner { get { return _owner; } }

    [SerializeField]
    private Transform _iKTarget;
    public Transform IKTarget { get { return _iKTarget; } }

    /// <summary>
    /// Sets the owner of this weapon.
    /// </summary>
    /// <param name="newOwner">The character manager of the character holding this weapon.</param>
    public void SetOwner(CharacterManager newOwner)
    {
        _owner = newOwner;
    }
}
