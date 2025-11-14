using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private int score;
    private float time;
    private bool IsTimerStart;

    private void Awake()
    {
        Instance = this;
        IsTimerStart = false;
    }

    private void Start()
    {
        Lander.Instance.OnCoinPickup += Lander_OnCoinPickup;
        Lander.Instance.OnLanded += Lander_OnLanded;
        Lander.Instance.OnStateChange += Lander_OnStateChange;
    }

    private void Lander_OnStateChange(object sender, Lander.OnStateChangeEventAgrs e)
    {
        IsTimerStart = e.new_state == Lander.States.Normal;
    }

    private void Update()
    {
        if (IsTimerStart)
        {
            time += Time.deltaTime;
        }
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
    }

    public int GetScore()
    {
        return score;
    }
    public float GetTime()
    {
        return time;
    }
}
