using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button quitButton;

    private void Awake()
    {
        Time.timeScale = 1f;
        playButton.onClick.AddListener(() => {
            GameManager.ResetStaticData();
            SceneLoader.LoadScene(SceneLoader.Scenes.GameScene);
        });
        quitButton.onClick.AddListener(() => {
            Application.Quit();
        });
    }
}
