using UnityEngine;
using System.Collections;
using System;

public class SpellcastingAgent : MonoBehaviour
{
    public struct SpellNames
    {
        public const string FIREBALL = "fireball";
    }

    [SerializeField]
    private WeaponAgent _weaponAgent;

    [SerializeField]
    private Transform _unarmedSpawnPosition;
    [SerializeField]
    private Transform _armedSpawnPosition;

    [SerializeField]
    private GameObject _fireballPrefab;

    private void Awake()
    {
        if (_weaponAgent == null)
        {
            _weaponAgent = GetComponent<WeaponAgent>();
        }
    }

    private void SpellcastAnimationFinished(string spellName)
    {
        Transform spawnPosition = GetSpawnPosition();

        switch (spellName)
        {
            case SpellNames.FIREBALL:
                var collisionScript = (Instantiate(_fireballPrefab, spawnPosition.position, _fireballPrefab.transform.rotation) as GameObject)
                    .GetComponentInChildren<DigitalRuby.PyroParticles.FireCollisionForwardScript>();
                collisionScript.Spawner = this.gameObject;
                break;

            default:
                throw new NotImplementedException();
        }
    }

    private Transform GetSpawnPosition()
    {
        if (_weaponAgent != null)
        {
            return (_weaponAgent.HasWeaponEquipped) ? _armedSpawnPosition : _unarmedSpawnPosition;
        }

        return _unarmedSpawnPosition;
    }
}
