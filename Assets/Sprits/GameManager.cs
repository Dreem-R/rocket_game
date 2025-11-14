using System;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private static int CurrentLevel = 1;

    [SerializeField] private List<GameLevel> GameLevelList;
    [SerializeField] private CinemachineCamera CinemachineCamera;

    private int score;
    private float time;
    private bool IsTimerStart;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Lander.Instance.OnCoinPickup += Lander_OnCoinPickup;
        Lander.Instance.OnLanded += Lander_OnLanded;
        Lander.Instance.OnStateChange += Lander_OnStateChange;

        IsTimerStart = false;
        LoadCurrentLevel();
    }

    private void Lander_OnStateChange(object sender, Lander.OnStateChangeEventAgrs e)
    {
        IsTimerStart = e.new_state == Lander.States.Normal;
        if(e.new_state == Lander.States.Normal)
        {
            CinemachineCamera.Target.TrackingTarget = Lander.Instance.transform;
            CineMachineCameraZoom.Instance.SetNormalOrthographicSize();
        }
    }

    private void Update()
    {
        if (IsTimerStart)
        {
            time += Time.deltaTime;
        }
    }
    private void LoadCurrentLevel()
    {
        foreach(GameLevel gamelevel in GameLevelList)
        {
            if(gamelevel.GetLevelNumber() == CurrentLevel)
            {
                GameLevel SpawnedGameLevel = Instantiate(gamelevel, Vector3.zero, Quaternion.identity);
                Lander.Instance.transform.position = SpawnedGameLevel.GetStartPosition();
                CinemachineCamera.Target.TrackingTarget = SpawnedGameLevel.GetCameraStartTargetTransformation();
                CineMachineCameraZoom.Instance.SetOrthographicSize(SpawnedGameLevel.GetLevelOrthographicSize());
            }
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

    public void NextLevel()
    {
        CurrentLevel++;
        SceneManager.LoadScene(0);
    }
    public void RetryLevel()
    { 
        SceneManager.LoadScene(0);
    }
}
