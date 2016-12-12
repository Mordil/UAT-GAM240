using L4.Unity.Common;
using L4.Unity.Common.Application;
using UnityEngine.SceneManagement;

public class Settings : SettingsBase
{

}

/// <summary>
/// Manager class responsible for overall application state.
/// </summary>
public class GameManager : AppManagerBase<GameManager, Settings>
{
    /// <summary>
    /// Switches the active Unity <see cref="Scene"/> to the one at the provided index.
    /// </summary>
    /// <param name="newLevel">The index of the level to load.</param>
    /// <seealso cref="ProjectSettings.Level"/>
    public void LoadLevel(ProjectSettings.Level newLevel)
    {
        SceneManager.LoadScene((int)newLevel);
    }
}
