using System.Collections;
using MoreMountains.Feedbacks;
using PrimeTween;
using UnityEngine;

public class CoinVisual : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private MMF_Player idleFeedback;

    // Timings for idle feedback
    [SerializeField] private float minWaitTime = 1f;
    [SerializeField] private float maxWaitTime = 3f;

    // Timings for flash effect
    [SerializeField] private float minFlashWait = 1.5f;
    [SerializeField] private float maxFlashWait = 3.5f;
    [SerializeField] private float flashDuration = 0.12f;

    private Material flashMaterial;
    private static readonly int FlashAmountId = Shader.PropertyToID("_FlashAmount");

    private void Awake()
    {
        flashMaterial = spriteRenderer.material;
    }

    private void Start()
    {
        StartCoroutine(IdleLoop());
        StartCoroutine(FlashLoop());
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
    
    private IEnumerator FlashLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minFlashWait, maxFlashWait));
            yield return Tween.MaterialProperty(flashMaterial, FlashAmountId,
                startValue: 1f, endValue: 0f, duration: flashDuration, ease: Ease.OutQuad)
                .ToYieldInstruction();
        }
    }

    private void OnDestroy()
    {
        Tween.StopAll(onTarget: flashMaterial);
        Destroy(flashMaterial);
    }
}
