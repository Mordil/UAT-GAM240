using L4.Unity.Common;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages the different screens for a level ending.
/// </summary>
public class GameOverScreenManager : MonoBehaviour
{
    [SerializeField]
    private Canvas _lossScreen;
    [SerializeField]
    private Canvas _winScreen;
    private Image _background;

    [Space(10)]

    [SerializeField]
    private float _transitionSpeed;

    [SerializeField]
    [Tooltip("The color the background should start out at, and be Lerped from.")]
    private Color _startColor;
    [SerializeField]
    [Tooltip("The color the background should end at, and be Lerped to.")]
    private Color _endColor;

    private void Awake()
    {
        if (GameManager.Instance.CurrentScene.As<GameplayLevel>().GameWasWon)
        {
            _winScreen.gameObject.SetActive(true);
            _background = _winScreen.GetComponentInChildren<Image>();
        }
        else
        {
            _lossScreen.gameObject.SetActive(true);
            _background = _lossScreen.GetComponentInChildren<Image>();
        }
    }
    
	private void Start()
    {
        _background.color = _startColor;
	}
	
	private void Update()
    {
        _background.color = Color.Lerp(_background.color, _endColor, _transitionSpeed * Time.deltaTime);
	}
}
