using UnityEngine;

/// <summary>
/// Component that manages spawning prefabs with a delay.
/// </summary>
public class Spawner : MonoBehaviour
{
    [SerializeField]
    private float _spawnDelay = 3f;
    private float _timeToSpawnTimer;

    [Header("Spawn Settings")]

    [SerializeField]
    private Vector3 _offset;
    [SerializeField]
    private GameObject _prefabToSpawn;
    [SerializeField]
    private GameObject _spawnedInstance;
    
    [Header("Gizmo Settings")]

    [Range(0, 1)]
    [SerializeField]
    private float _opacity = .5f;

    [SerializeField]
    private Color _gizmoColor = Color.blue;
    [SerializeField]
    private Vector3 _scale;

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

    private void OnDrawGizmos()
    {
        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);
        Gizmos.color = Color.Lerp(_gizmoColor, Color.clear, _opacity);
        Gizmos.DrawCube(Vector3.up * _scale.y / 2f, _scale);
        Gizmos.color = _gizmoColor;
        Gizmos.DrawRay(Vector3.zero, Vector3.forward * .4f);
    }

    private void SpawnPrefab()
    {
        _spawnedInstance = Instantiate(_prefabToSpawn, transform.position + _offset, _prefabToSpawn.transform.rotation) as GameObject;
        _spawnedInstance.transform.SetParent(this.transform);
        _timeToSpawnTimer = 0;
    }
}
