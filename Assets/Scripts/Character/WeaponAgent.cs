using UnityEngine;
using System.Collections;

public class WeaponAgent : MonoBehaviour
{
    private const string WEAPON_ANIMATION_INDEX = "Weapon Animation Index";

    public bool HasWeaponEquipped { get { return CurrentWeapon != null; } }

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
        newWeapon.transform.localPosition = AttachmentPoint.transform.localPosition;
        newWeapon.transform.localRotation = AttachmentPoint.transform.localRotation;

        CurrentWeapon = newWeapon.GetComponent<Weapon>();
        CurrentWeapon.SetOwner(GetComponent<CharacterManager>());

        AnimatorComponent.SetInteger(WEAPON_ANIMATION_INDEX, (int)CurrentWeapon.Animation);
    }

    public Weapon GetEquippedWeapon()
    {
        return CurrentWeapon;
    }
}
