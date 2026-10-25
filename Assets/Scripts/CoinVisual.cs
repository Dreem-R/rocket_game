using System.Collections;
using MoreMountains.Feedbacks;
using PrimeTween;
using UnityEngine;

public class CoinVisual : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private MMF_Player idleFeedback;
    [SerializeField] private ScorePopup scorePopupPrefab;
    [SerializeField] private GameObject coinPickupVFXPrefab;

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
        GetComponent<CoinPickup>().OnCollected += CoinPickup_OnCollected;
    }

    private void CoinPickup_OnCollected(object sender, System.EventArgs e)
    {
        StopAllCoroutines();
        idleFeedback.StopFeedbacks();
        Tween.MaterialProperty(flashMaterial, FlashAmountId,
                startValue: 0f, endValue: 1f, duration: flashDuration, ease: Ease.OutQuad);
        Tween.Scale(spriteRenderer.transform, startValue: Vector3.one, endValue: Vector3.zero,
            duration: 0.2f, ease: Ease.InBack);
        Instantiate(coinPickupVFXPrefab, transform.position, Quaternion.identity);
        Instantiate(scorePopupPrefab, transform.position, Quaternion.identity).Setup("+500");
        Debug.Log("CoinVisual_OnCollected");
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
