using UnityEngine;

public class LanderVisuals : MonoBehaviour
{

    [SerializeField] private ParticleSystem LeftThrusterPracticeSystem;
    [SerializeField] private ParticleSystem MiddleThrusterPracticeSystem;
    [SerializeField] private ParticleSystem RightThrusterPracticeSystem;

    private Lander lander;

    private void Awake()
    {
        lander = GetComponent<Lander>();

        lander.OnUpForce += Lander_OnUpForce;
        lander.OnLeftForce += Lander_OnLeftForce;
        lander.OnRightForce += Lander_OnRightForce;
        lander.OnBeforeForce += Lander_OnBeforeForce;

        SetEnableThrusterPraticeSystem(LeftThrusterPracticeSystem, false);
        SetEnableThrusterPraticeSystem(MiddleThrusterPracticeSystem, false);
        SetEnableThrusterPraticeSystem(RightThrusterPracticeSystem, false);
    }

    private void Lander_OnBeforeForce(object sender, System.EventArgs e)
    {
        SetEnableThrusterPraticeSystem(LeftThrusterPracticeSystem, false);
        SetEnableThrusterPraticeSystem(MiddleThrusterPracticeSystem, false);
        SetEnableThrusterPraticeSystem(RightThrusterPracticeSystem, false);
    }

    private void Lander_OnRightForce(object sender, System.EventArgs e)
    {
        SetEnableThrusterPraticeSystem(RightThrusterPracticeSystem, true);
    }

    private void Lander_OnLeftForce(object sender, System.EventArgs e)
    {
        SetEnableThrusterPraticeSystem(LeftThrusterPracticeSystem, true);
    }

    private void Lander_OnUpForce(object sender, System.EventArgs e)
    {
        SetEnableThrusterPraticeSystem(LeftThrusterPracticeSystem, true);
        SetEnableThrusterPraticeSystem(MiddleThrusterPracticeSystem, true);
        SetEnableThrusterPraticeSystem(RightThrusterPracticeSystem, true);
    }

    private void SetEnableThrusterPraticeSystem(ParticleSystem particleSystem, bool enabled)
    {
        ParticleSystem.EmissionModule emissionModule = particleSystem.emission;
        emissionModule.enabled = enabled;
    }
}
