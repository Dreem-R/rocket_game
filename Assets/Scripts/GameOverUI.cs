using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI FinalScoreText;
    [SerializeField] private Button MainMenuButton;

    private void Awake()
    {
        MainMenuButton.onClick.AddListener(() => { SceneLoader.LoadScene(SceneLoader.Scenes.MainMenuScene); });

        FinalScoreText.text = "Final Score: " + GameManager.Instance.getTotalScore();
    }
}
