using UnityEngine;
using System.Collections;
using L4.Unity.Common;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Events;

public class GameplayLevel : SceneBase
{
    private class LevelDefinition
    {
        public static LevelDefinition[] LevelDefinitions =
        {
            new LevelDefinition() { _numberOfMelee = 1 },
            new LevelDefinition() { _numberOfMelee = 2 },
            new LevelDefinition() { _numberOfMelee = 3 },
            new LevelDefinition() { _numberOfSpellcasters = 1 },
            new LevelDefinition() { _numberOfSpellcasters = 2 },
            new LevelDefinition() { _numberOfMelee = 1, _numberOfSpellcasters = 1 },
            new LevelDefinition() { _numberOfMelee = 2, _numberOfSpellcasters = 1 }
        };

        public int NumberOfSpellcasters { get { return _numberOfSpellcasters; } }
        public int NumberOfMelee { get { return _numberOfMelee; } }

        private int _numberOfSpellcasters;
        private int _numberOfMelee;
    }

    public UnityEvent OnLevelPaused;
    public UnityEvent OnLevelResumed;

    [SerializeField]
    private GameObject _deathElementsContainer;
    [SerializeField]
    private GameObject _enemyContainer;

    [Header("Player Settings")]
    [SerializeField]
    private CharacterManager _player;
    [SerializeField]
    private Transform _playerSpawner;
    [SerializeField]
    private GameObject _playerPrefab;

    private int _levelIndex;
    
    private List<GameObject> _enemyList;

    protected override void Awake()
    {
        base.Awake();

        if (_enemyContainer != null)
        {
            _enemyList = new List<GameObject>();
            _enemyList = _enemyContainer
                .GetComponentsInChildren<Transform>()
                .Select(x => x.gameObject)
                .ToList();
        }

        StartLevel(_levelIndex);
    }

    public void PlayerDied()
    {
        _deathElementsContainer.SetActive(true);
    }

    public void Pause()
    {
        Time.timeScale = 0;
        OnLevelPaused.Invoke();
    }

    public void Resume()
    {
        Time.timeScale = 1;
        OnLevelResumed.Invoke();
    }

    private void StartLevel(int levelNumber)
    {
        var levelDef = LevelDefinition.LevelDefinitions[levelNumber];

        var player = Instantiate(_playerPrefab, _playerSpawner.position, _playerSpawner.rotation) as GameObject;
        _player = player.GetComponent<CharacterManager>();
        player.GetComponent<Health>().OnKilled.AddListener(() => { PlayerDied(); });
    }
}
