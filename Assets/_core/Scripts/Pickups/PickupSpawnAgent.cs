using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawnAgent : MonoBehaviour
{
    [SerializeField]
    private Vector3 _offset;
    [SerializeField]
    private AbstractPickup PickupToSpawn;
    [SerializeField]
    private GameObject _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            Transform myTransform = this.gameObject.transform;
            GameObject prefab = PickupToSpawn.PickupPrefab;
            var instance = Instantiate(prefab, myTransform.position + _offset, prefab.transform.rotation) as GameObject;
            instance.name = string.Format("{0}_{1}", this.gameObject.name, "instance");
            instance.transform.SetParent(myTransform);
            _instance = instance;
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        GameObject obj = collider.gameObject;
        if (obj.tag == "Player" && _instance != null)
        {
            PickupToSpawn.DoPickup(obj);
            Destroy(_instance);
            _instance = null;
        }
    }
}
