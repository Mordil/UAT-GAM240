using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Component that manages the Player's HUD.
/// </summary>
public class HUDManager : MonoBehaviour
{
    [SerializeField]
    private Image _equippedWeaponImage;

    private void Awake()
    {
        var weaponAgent = GetComponentInParent<WeaponAgent>();
        if (weaponAgent != null)
        {
            weaponAgent.OnEquippedWeapon.AddListener((newWeapon) => { _equippedWeaponImage.gameObject.SetActive(true); });
        }
    }
}
