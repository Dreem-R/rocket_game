using Unity.Cinemachine;
using UnityEngine;

public class CineMachineCameraZoom : MonoBehaviour
{
    private static float NORMAL_ORTHOGRAPHICSIZE = 10f;

    public static CineMachineCameraZoom Instance;

    [SerializeField] private CinemachineCamera cinemachineCamera;

    private float ZoomSpeed = 2.5f;
    private float TargetOrthoGraphicSize = 10f;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        /*Using Lerp so that camera soom happens slow and smoothly
            - Using Time.deltaTime * ZoomSpeed
        */
        cinemachineCamera.Lens.OrthographicSize = Mathf.Lerp(
            cinemachineCamera.Lens.OrthographicSize,
            TargetOrthoGraphicSize,
            Time.deltaTime * ZoomSpeed);
    }

    public void SetOrthographicSize(float New_OrthographicSize)
    {
        TargetOrthoGraphicSize = New_OrthographicSize;
    }
    
    public void SetNormalOrthographicSize()
    {
        SetOrthographicSize(NORMAL_ORTHOGRAPHICSIZE);
    }
}
