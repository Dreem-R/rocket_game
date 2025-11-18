using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private const int SOUND_VOLUME_MAX = 10;

    public static SoundManager Instance { get; private set; }
    private static int soundvolume = 6;

    [SerializeField] private AudioClip FuelPickupAudio;
    [SerializeField] private AudioClip CoinPickupAudio;
    [SerializeField] private AudioClip LandingAudio;
    [SerializeField] private AudioClip CrashAudio;

    public event EventHandler OnSoundVolumeChange;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Lander.Instance.OnFuelPickup += Lander_OnFuelPickup;
        Lander.Instance.OnCoinPickup += Lander_OnCoinPickup;
        Lander.Instance.OnLanded += Lander_OnLanded;
    }

    private void Lander_OnLanded(object sender, Lander.OnLandedEventArgs e)
    {
        switch (e.landingType)
        {
            case Lander.LandingType.Success:
                AudioSource.PlayClipAtPoint(LandingAudio, Camera.main.transform.position,GetSoundVolumeNormalized());
                break;

            default:
                AudioSource.PlayClipAtPoint(CrashAudio, Camera.main.transform.position);
                break;
        }
    }

    private void Lander_OnCoinPickup(object sender, System.EventArgs e)
    {
        AudioSource.PlayClipAtPoint(CoinPickupAudio, Camera.main.transform.position,GetSoundVolumeNormalized());
    }

    private void Lander_OnFuelPickup(object sender, System.EventArgs e)
    {
        AudioSource.PlayClipAtPoint(FuelPickupAudio, Camera.main.transform.position, GetSoundVolumeNormalized());
    }
    public void ChangeSoundVolume()
    { 
        soundvolume = (soundvolume + 1) % SOUND_VOLUME_MAX;
        OnSoundVolumeChange?.Invoke(this, EventArgs.Empty);
    }
    public int GetSoundVolume()
    {
        return soundvolume;
    }
    public float GetSoundVolumeNormalized()
    {
        return ((float)soundvolume / SOUND_VOLUME_MAX);
    }
}
