using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuUI : MonoBehaviour
{
    [SerializeField] private Button ResumeButton;
    [SerializeField] private Button QuitButton;
    [SerializeField] private Button SoundVolumeButton;
    [SerializeField] private TextMeshProUGUI SoundVolumeText;
    [SerializeField] private Button MusicVolumeButton;
    [SerializeField] private TextMeshProUGUI MusicVolumeText;
    private void Awake()
    {
        SoundVolumeButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.ChangeSoundVolume();
            SoundVolumeText.text = "SOUND " + SoundManager.Instance.GetSoundVolume();
        });

        MusicVolumeButton.onClick.AddListener(() =>
        {
            MusicManager.Instance.ChangeMusicVolume();
            MusicVolumeText.text = "Music " + MusicManager.Instance.GetMusicVolume();
        });

        ResumeButton.onClick.AddListener(() => {
            GameManager.Instance.UnPauseGame();
        });

        QuitButton.onClick.AddListener(() => { SceneLoader.LoadScene(SceneLoader.Scenes.MainMenuScene); });
    }

    void Start()
    {
        GameManager.Instance.OnGamePaused += GameManager_OnGamePaused;
        GameManager.Instance.OnGameUnPaused += GameManager_OnGameUnPaused;

        SoundVolumeText.text = "SOUND " + SoundManager.Instance.GetSoundVolume();
        MusicVolumeText.text = "Music " + MusicManager.Instance.GetMusicVolume();
        Hide();
    }

    private void GameManager_OnGameUnPaused(object sender, EventArgs e)
    {
        Hide();
    }

    private void GameManager_OnGamePaused(object sender, EventArgs e)
    {
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
