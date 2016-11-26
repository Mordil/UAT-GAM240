﻿using UnityEngine;
using System.Collections;

public class WeaponAgent : MonoBehaviour
{
    public const string WEAPON_ANIMATION_INDEX = "Weapon Animation Index";

    [SerializeField]
    protected Weapon CurrentWeapon;
    [SerializeField]
    protected Animator AnimatorComponent;
    [SerializeField]
    protected Transform AttachmentPoint;

    protected virtual void Awake()
    {
        if (AnimatorComponent == null)
        {
            AnimatorComponent = GetComponent<Animator>();
        }

        AnimatorComponent.SetInteger(WEAPON_ANIMATION_INDEX, 0);
    }

    protected virtual void OnAnimatorIK()
    {
        if (CurrentWeapon != null)
        {
            if (CurrentWeapon.IKTarget != null)
            {
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

    public void HandleNewWeapon(WeaponPickup pickup)
    {
        var prefab = pickup.As<WeaponPickup>().WeaponPrefab;
        var newWeapon = Instantiate(prefab) as GameObject;

        newWeapon.transform.SetParent(AttachmentPoint);
        newWeapon.transform.localPosition = prefab.transform.localPosition;
        newWeapon.transform.localRotation = prefab.transform.localRotation;

        CurrentWeapon = newWeapon.GetComponent<Weapon>();
        AnimatorComponent.SetInteger(WEAPON_ANIMATION_INDEX, (int)CurrentWeapon.Animation);
    }
}
