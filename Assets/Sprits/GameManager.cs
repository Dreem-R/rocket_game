using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int score;

    private void Start()
    {
        Lander.Instance.OnCoinPickup += Lander_OnCoinPickup;
        Lander.Instance.OnLanded += Lander_OnLanded;
    }

    private void Lander_OnLanded(object sender, Lander.OnLandedEventArgs e)
    {
        addscore(e.score);
    }

    private void Lander_OnCoinPickup(object sender, System.EventArgs e)
    {
        addscore(500);
    }

    private void addscore(int addScoreAmount)
    {
        score += addScoreAmount;
        Debug.Log("Score :" + score);
    }
}
