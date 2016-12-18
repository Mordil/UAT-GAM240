/// <summary>
/// Static class reference for project settings in code.
/// </summary>
public static class ProjectSettings
{
    /// <summary>
    /// Enumeration of available <see cref="UnityEngine.SceneManagement.Scene"/> assets by build index.
    /// </summary>
    public enum Level : int
    {
        MainMenu = 0,
        MainGameplay = 1
    }

    public static class PlayerPrefs
    {
        public const string MASTER_VOLUME = "Master Volume";
        public const string MUSIC_VOLUME = "Music Volume";
        public const string SFX_VOLUME = "SFX Volume";
        public const string AMBIENT_VOLUME = "Ambient Volume";
    }

    public static class AudioMixers
    {
        public static class Main
        {
            public static class Parameters
            {
                public const string MASTER_VOLUME = PlayerPrefs.MASTER_VOLUME;
                public const string MUSIC_VOLUME = PlayerPrefs.MUSIC_VOLUME;
                public const string SFX_VOLUME = PlayerPrefs.SFX_VOLUME;
                public const string AMBIENT_VOLUME = PlayerPrefs.AMBIENT_VOLUME;
            }
        }
    }
}
