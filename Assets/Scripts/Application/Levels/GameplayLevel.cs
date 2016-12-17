using L4.Unity.Common;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Manages the main gameplay level.
/// </summary>
public class GameplayLevel : SceneBase
{
    // This is unused for now. Will try implementing before turning in...
    private class LevelDefinition
    {
        /// <summary>
        /// Predefined level definitions.
        /// </summary>
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

        /// <summary>
        /// The number of spellcasters to spawn.
        /// </summary>
        public int NumberOfSpellcasters { get { return _numberOfSpellcasters; } }
        /// <summary>
        /// The number of melee AI to spawn.
        /// </summary>
        public int NumberOfMelee { get { return _numberOfMelee; } }

        private int _numberOfSpellcasters;
        private int _numberOfMelee;
    }

    /// <summary>
    /// Was the game won by the player?
    /// </summary>
    public bool GameWasWon { get; private set; }

    /// <summary>
    /// Event emitted when the level has been paused.
    /// </summary>
    public UnityEvent OnLevelPaused;
    /// <summary>
    /// Event emitted when the level has been resumed.
    /// </summary>
    public UnityEvent OnLevelResumed;

    [Header("GameObject Containers")]
    [SerializeField]
    private GameObject _gameOverScreen;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject _environmentContainer;

    [Header("Player Settings")]
    [SerializeField]
    private CharacterManager _player;
    [SerializeField]
    private Transform _playerSpawner;
    [SerializeField]
    private GameObject _playerPrefab;

    [Header("Audio Settings")]
    [SerializeField]
    private AudioSource _audioSource;
    [SerializeField]
    private AudioClip _victoryMusic;
    [SerializeField]
    private AudioClip _deathMusic;

    private int _levelIndex;
    
    private List<GameObject> _enemyList;

    protected override void Awake()
    {
        base.Awake();
        Time.timeScale = 1;

        StartLevel(_levelIndex);
    }

    protected override void Start()
    {
        _enemyList = new List<GameObject>();

        _environmentContainer.GetComponentsInChildren<Spawner>()
            .Select(x => x.SpawnedInstance)
            .ToList()
            .ForEach(instance =>
            {
                _enemyList.Add(instance);
                instance.transform.SetParent(_enemyContainer.transform, true);
            });
    }

    protected override void Update()
    {
        for(int i = 1; i <= _enemyList.Count; i++)
        {
            if (_enemyList[i - 1] != null)
            {
                break;
            }

            if (i == _enemyList.Count)
            {
                PlayNewAudioClip(_victoryMusic);
                GameWasWon = true;
                _gameOverScreen.SetActive(true);
                _player.gameObject.SetActive(false);
                Invoke("ReturnToMainMenu", 10f);
            }
        }
    }

    /// <summary>
    /// Callback for when the player has died.
    /// </summary>
    public void PlayerDied()
    {
        PlayNewAudioClip(_deathMusic);
        _gameOverScreen.SetActive(true);
        Invoke("ReturnToMainMenu", 5f);
    }

    /// <summary>
    /// Callback for pausing gameplay.
    /// </summary>
    public void Pause()
    {
        Time.timeScale = 0;
        OnLevelPaused.Invoke();
    }

    /// <summary>
    /// Callback to resume playing.
    /// </summary>
    public void Resume()
    {
        Time.timeScale = 1;
        OnLevelResumed.Invoke();
    }

    private void ReturnToMainMenu()
    {
        GameManager.Instance.LoadLevel(ProjectSettings.Level.MainMenu);
    }

    private void StartLevel(int levelNumber)
    {
        var levelDef = LevelDefinition.LevelDefinitions[levelNumber];

        var player = Instantiate(_playerPrefab, _playerSpawner.position, _playerSpawner.rotation) as GameObject;
        _player = player.GetComponent<CharacterManager>();
        player.GetComponent<Health>().OnKilled.AddListener(PlayerDied);
    }

    private void PlayNewAudioClip(AudioClip newClip)
    {
        _audioSource.Stop();
        _audioSource.clip = newClip;
        _audioSource.Play();
    }
}
