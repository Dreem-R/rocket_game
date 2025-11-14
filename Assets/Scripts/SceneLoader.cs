using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader
{
    public enum Scenes
    {
        MainMenuScene,
        GameScene,
        GameOverScene,
    }

    public static void LoadScene(Scenes scenes)
    {
        SceneManager.LoadScene(scenes.ToString());
    }
}
