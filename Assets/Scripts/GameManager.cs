using System;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private static int CurrentLevel = 1;
    private static int TotalScore = 0;

    public static void ResetStaticData()
    {
        CurrentLevel = 1;
        TotalScore = 0;
    }

    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUnPaused;

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

        GameInput.Instance.OnMenuButtonPressed += GameInput_OnMenuButtonPressed;

        IsTimerStart = false;
        LoadCurrentLevel();
    }

    private void GameInput_OnMenuButtonPressed(object sender, EventArgs e)
    {
        PauseUnpauseGame();
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
        GameLevel gamelevel = GetGameLevel();
        GameLevel SpawnedGameLevel = Instantiate(gamelevel, Vector3.zero, Quaternion.identity);
        CinemachineCamera.Target.TrackingTarget = SpawnedGameLevel.GetCameraStartTargetTransformation();
        Lander.Instance.transform.position = SpawnedGameLevel.GetStartPosition();
        CineMachineCameraZoom.Instance.SetOrthographicSize(SpawnedGameLevel.GetLevelOrthographicSize());
    }

    private GameLevel GetGameLevel()
    {
        foreach (GameLevel gamelevel in GameLevelList)
        {
            if (gamelevel.GetLevelNumber() == CurrentLevel)
            {
                return gamelevel;
            }
        }
        return null;
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
        TotalScore += score;
        CurrentLevel++;

        //Check if there are any more Levels Left
        if(GetGameLevel() == null)
        {
            SceneLoader.LoadScene(SceneLoader.Scenes.GameOverScene);
        }
        else
        {
            SceneLoader.LoadScene(SceneLoader.Scenes.GameScene);
        }
    }
    public void RetryLevel()
    { 
        SceneLoader.LoadScene(SceneLoader.Scenes.GameScene);
    }

    public void PauseUnpauseGame()
    {
        if(Time.deltaTime == 0f)
        {
            UnPauseGame();
        }
        else
        {
            PauseGame();
        }
    }

    public int getTotalScore()
    {
        return TotalScore;
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        OnGamePaused?.Invoke(this, EventArgs.Empty);
    }
    public void UnPauseGame()
    {
        Time.timeScale = 1f;
        OnGameUnPaused?.Invoke(this, EventArgs.Empty);
    }
}
