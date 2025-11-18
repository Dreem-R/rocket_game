using System;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private const int MUSIC_VOLUME_MAX = 10;

    public static MusicManager Instance { get; private set; }
    private static int MusicVolume = 5;
    private static float musicTime;
    private event EventHandler OnMusicVolumeChanged;

    private AudioSource musicAudioSource;

    private void Awake()
    {
        Instance = this;
        musicAudioSource = GetComponent<AudioSource>();
        musicAudioSource.time = musicTime;
        musicAudioSource.volume = GetMusicVolumeNormalized();
    }

    void Update()
    {
        musicTime = musicAudioSource.time;
    }

    public void ChangeMusicVolume()
    {
        MusicVolume = (MusicVolume + 1) % MUSIC_VOLUME_MAX;
        musicAudioSource.volume = GetMusicVolumeNormalized();
        OnMusicVolumeChanged?.Invoke(this, EventArgs.Empty);
    }
    public int GetMusicVolume()
    {
        return MusicVolume;
    }
    private float GetMusicVolumeNormalized()
    {
        return ((float)MusicVolume / MUSIC_VOLUME_MAX);
    }
}
