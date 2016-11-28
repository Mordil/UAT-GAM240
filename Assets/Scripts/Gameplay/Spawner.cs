using UnityEngine;

/// <summary>
/// Component that manages spawning prefabs with a delay.
/// </summary>
public class Spawner : MonoBehaviour
{
    [SerializeField]
    private float _spawnDelay = 3f;
    private float _timeToSpawnTimer;

    [SerializeField]
    private Vector3 _offset;
    [SerializeField]
    private GameObject _prefabToSpawn;
    [SerializeField]
    private GameObject _spawnedInstance;

    private void Awake()
    {
        if (_spawnedInstance == null)
        {
            SpawnPrefab();
        }
    }

    private void Update()
    {
        if (_spawnedInstance == null && _timeToSpawnTimer == 0)
        {
            _timeToSpawnTimer = _spawnDelay;
        }

        if (_timeToSpawnTimer > 0)
        {
            _timeToSpawnTimer -= Time.deltaTime;
        }
        else if (_timeToSpawnTimer < 0)
        {
            SpawnPrefab();
        }
    }

    private void SpawnPrefab()
    {
        _spawnedInstance = Instantiate(_prefabToSpawn, transform.position + _offset, _prefabToSpawn.transform.rotation) as GameObject;
        _spawnedInstance.transform.SetParent(this.transform);
        _timeToSpawnTimer = 0;
    }
}
