using L4.Unity.Common;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>
/// The main menu level object for handling logic at the start screen.
/// </summary>
public class MainMenuLevel : SceneBase
{
    /// <summary>
    /// On click handler for a "Start Game" button.
    /// </summary>
    public void OnNewGameClicked()
    {
        GameManager.Instance.LoadLevel(ProjectSettings.Level.MainGameplay);
    }

    /// <summary>
    /// On click handler for a "Quit Application" button.
    /// </summary>
    public void OnQuitClicked()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
