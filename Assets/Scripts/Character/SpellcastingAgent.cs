using UnityEngine;
using System.Collections;
using System;

public class SpellcastingAgent : MonoBehaviour
{
    private struct SpellNames
    {
        public const string FIREBALL = "fireball";
    }

    [SerializeField]
    private Transform _spawnPosition;

    [SerializeField]
    private GameObject _fireballPrefab;

    private void SpellcastAnimationFinished(string spellName)
    {
        switch (spellName)
        {
            case SpellNames.FIREBALL:
                Instantiate(_fireballPrefab, _spawnPosition.position, _fireballPrefab.transform.rotation);
                break;

            default:
                throw new NotImplementedException();
        }
    }
}
