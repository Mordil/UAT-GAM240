using System;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Master class that manages spellcasting and spells for Characters.
/// </summary>
/// <seealso cref="ISpellcastingAnimationHandler"/>
public class SpellcastingAgent : MonoBehaviour, ISpellcastingAnimationHandler
{
    public UnityEvent<string> OnSpellCast;

    private IInputController _inputController;

    [SerializeField]
    private WeaponAgent _weaponAgent;
    
    [SerializeField]
    [Tooltip("The position spells should be spawned at when the character is unarmed.")]
    private Transform _unarmedSpawnPosition;
    [SerializeField]
    [Tooltip("The position spells should be spawned at when the character is armed.")]
    private Transform _armedSpawnPosition;

    [SerializeField]
    private GameObject _fireballPrefab;

    private void Awake()
    {
        if (_weaponAgent == null)
        {
            _weaponAgent = GetComponent<WeaponAgent>();
        }

        _inputController = GetComponent<IInputController>();
    }
    
    /// <summary>
    /// Instantiates the prefab for the provided spell.
    /// </summary>
    /// <param name="spellName">The name of the spell to instantiate.</param>
    /// <seealso cref="ISpellcastingAnimationHandler.SpellcastAnimationFinished(string)"/>
    public void SpawnSpell(string spellName)
    {
        // instantiate and do necessary logic for the matching spell
        switch (spellName)
        {
            case SpellNames.FIREBALL:
                SpawnFireball();
                break;

            default:
                throw new NotImplementedException();
        }

        OnSpellCast.Invoke(spellName);
    }

    private void SpawnFireball()
    {
        // get the position based on the character being armed/unarmed
        Transform spawnPosition = GetSpawnPosition();
        Quaternion spawnRotation = gameObject.transform.rotation;

        var controller = _inputController as AIInputController;
        if (controller != null && controller.Target != null)
        {
            spawnPosition.LookAt(controller.Target.position + new Vector3(0, 1.25f, 0));
            spawnRotation = spawnPosition.rotation;
        }

        var collisionScript = (Instantiate(_fireballPrefab, spawnPosition.position, spawnRotation) as GameObject)
            .GetComponentInChildren<DigitalRuby.PyroParticles.FireCollisionForwardScript>();
        collisionScript.Spawner = this.gameObject;
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
