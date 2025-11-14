using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class StatsUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI StatsText;
    [SerializeField] private GameObject SpeedUpArrow;
    [SerializeField] private GameObject SpeedDownArrow;
    [SerializeField] private GameObject SpeedRightArrow;
    [SerializeField] private GameObject SpeedLeftArrow;
    [SerializeField] private Image fuelImage;

    // Update is called once per frame
    void Update()
    {
        UpdateStatsText();
    }

    private void UpdateStatsText()
    {
        SpeedRightArrow.SetActive(Lander.Instance.GetSpeedX() >= 0);
        SpeedLeftArrow.SetActive(Lander.Instance.GetSpeedX() < 0);
        SpeedUpArrow.SetActive(Lander.Instance.GetSpeedY() >= 0);
        SpeedDownArrow.SetActive(Lander.Instance.GetSpeedY() < 0);
        fuelImage.fillAmount = Lander.Instance.Get_Normalized_FuelAmount();

        StatsText.text =
            GameManager.Instance.GetScore() + "\n" +
            Mathf.Round(GameManager.Instance.GetTime()) + "\n" +
            Mathf.Abs(Mathf.Round(Lander.Instance.GetSpeedX())) * 10f + "\n" +
            Mathf.Abs(Mathf.Round(Lander.Instance.GetSpeedY())) * 10f + "\n";
    }
}
