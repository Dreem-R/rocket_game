using UnityEngine;

public class GameLevel : MonoBehaviour
{
    [SerializeField] private int LevelNumber;
    [SerializeField] private Transform LanderStartPosition;
    [SerializeField] private Transform CameraTargetPosition;
    [SerializeField] private float LevelOrthographicSize;

    public int GetLevelNumber()
    {
        return LevelNumber;
    }
    public Vector3 GetStartPosition()
    {
        return LanderStartPosition.position;
    }
    public Transform GetCameraStartTargetTransformation()
    {
        return CameraTargetPosition.transform;
    }
    public float GetLevelOrthographicSize()
    {
        return LevelOrthographicSize;
    }
}
