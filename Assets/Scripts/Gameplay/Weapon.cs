﻿using UnityEngine;
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
    private Transform _iKTarget;
    public Transform IKTarget { get { return _iKTarget; } }
}
