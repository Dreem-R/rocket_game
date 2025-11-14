using UnityEngine;

public class GameLevel : MonoBehaviour
{
    [SerializeField] private int LevelNumber;
    [SerializeField] private Transform LanderStartPosition;

    public int GetLevelNumber()
    {
        return LevelNumber;
    }
    public Vector3 GetStartPosition()
    {
        return LanderStartPosition.position;
    }
}
