using UnityEngine;

[CreateAssetMenu(fileName = "New Health Pickup", menuName = "Pickup/Health")]
public class HealthPickup : AbstractPickup
{
    public override PickupType Type { get { return PickupType.Powerup; } }

    [SerializeField]
    [Tooltip("The amount of health characters will gain.")]
    private float _healValue;
    public float HealValue { get { return _healValue; } }

    public override void DoPickup(GameObject target)
    {
        var healthAgent = target.GetComponent<HealthAgent>();
        if (healthAgent != null)
        {
            healthAgent.ModifyHealth(HealValue);
        }
    }
}
