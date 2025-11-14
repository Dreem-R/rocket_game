using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LandedUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI TitleText;
    [SerializeField] private TextMeshProUGUI StatsText;
    [SerializeField] private TextMeshProUGUI NextButtonText;
    [SerializeField] private Button NextButton;

    private Action NextButtonAction;

    private void Awake()
    {
        NextButton.onClick.AddListener(() => {
            NextButtonAction();
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
            NextButtonText.text = "CONTINUE";
            NextButtonAction = GameManager.Instance.NextLevel;
        }
        else
        {
            TitleText.text = "<color=#ff0000>Crash!</color>";
            NextButtonText.text = "Retry";
            NextButtonAction = GameManager.Instance.RetryLevel;
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
