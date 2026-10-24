using System.Collections;
using MoreMountains.Feedbacks;
using UnityEngine;

public class CoinVisual : MonoBehaviour
{
    [SerializeField] private MMF_Player idleFeedback;
    [SerializeField] private float minWaitTime = 1f;
    [SerializeField] private float maxWaitTime = 3f;

    void Start()
    {
        StartCoroutine(IdleLoop());
    }
    private IEnumerator IdleLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));
            idleFeedback.PlayFeedbacks();
            yield return new WaitWhile(() => idleFeedback.IsPlaying);
        }
    }
}
