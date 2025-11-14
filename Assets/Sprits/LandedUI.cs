using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LandedUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI TitleText;
    [SerializeField] private TextMeshProUGUI StatsText;
    [SerializeField] private Button NextButton;

    private void Awake()
    {
        NextButton.onClick.AddListener(() => {
            SceneManager.LoadScene(0);
        });
    }
    private void Start()
    {
        Lander.Instance.OnLanded += Lander_OnLanded;

        Hide();
    }

    private void Lander_OnLanded(object sender, Lander.OnLandedEventArgs e)
    {
        if (e.landingType == Lander.LandingType.Success)
        {
            TitleText.text = "SUCCESSFUL LANDING!";
        }
        else
        {
            TitleText.text = "<color=#ff0000>Crash!</color>";
        }
        StatsText.text =
            Mathf.Round(e.landingSpeed * 2f) + "\n" +
            Mathf.Round(e.landingAngle * 100f) + "\n" +
            "x"+ e.ScoreMultiplier + "\n" +
            e.score;

        Show();
    }
    
    private void Show()
    {
        gameObject.SetActive(true);
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
