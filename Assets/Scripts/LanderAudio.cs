using UnityEngine;

public class LanderAudio : MonoBehaviour
{
    [SerializeField] private AudioSource ThrusterAudioSource;

    private Lander lander;
    private void Awake()
    {
        lander = GetComponent<Lander>();
    }

    private void Start()
    {
        lander.OnBeforeForce += Lander_OnBeforeForce;
        lander.OnRightForce += Lander_OnRightForce;
        lander.OnUpForce += Lander_OnUpForce;
        lander.OnLeftForce += Lander_OnLeftForce;

        SoundManager.Instance.OnSoundVolumeChange += SoundManager_OnSoundVolumeChange;

        ThrusterAudioSource.Pause();
    }

    private void SoundManager_OnSoundVolumeChange(object sender, System.EventArgs e)
    {
        ThrusterAudioSource.volume = SoundManager.Instance.GetSoundVolumeNormalized();
    }

    private void Lander_OnLeftForce(object sender, System.EventArgs e)
    {
        if (!ThrusterAudioSource.isPlaying)
        {
            ThrusterAudioSource.Play();
        }
    }

    private void Lander_OnUpForce(object sender, System.EventArgs e)
    {
        if (!ThrusterAudioSource.isPlaying)
        {
            ThrusterAudioSource.Play();
        }
    }

    private void Lander_OnRightForce(object sender, System.EventArgs e)
    {
        if (!ThrusterAudioSource.isPlaying)
        {
            ThrusterAudioSource.Play();
        }
    }

    private void Lander_OnBeforeForce(object sender, System.EventArgs e)
    {
        if (ThrusterAudioSource.isPlaying)
        {
            ThrusterAudioSource.Pause();
        }
    }
}
