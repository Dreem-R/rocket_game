using PrimeTween;
using TMPro;
using UnityEngine;

public class ScorePopup : MonoBehaviour
{
    [SerializeField] private Transform visual;
    [SerializeField] private SpriteRenderer background;
    [SerializeField] private Color backgroundcolor;
    [SerializeField] private TextMeshPro text;

    [SerializeField] private float floatHeight = 0.8f;

    private void Start()
    {
        visual.localScale = Vector3.zero;
        background.color = Color.white;
    }

    public void Setup(string message)
    {
        text.text = message;

        Sequence pop = Sequence.Create()
            .Chain(Tween.Scale(visual, startValue: Vector3.zero, endValue: new Vector3(0.6f, 1.4f, 1),
            duration: 0.15f, ease: Ease.OutQuad))
            .Chain(Tween.Scale(visual, endValue: new Vector3(1.5f, 0.8f, 1), duration: 0.12f, ease: Ease.OutQuad))
            .Chain(Tween.Scale(visual, endValue: Vector3.one, duration: 0.12f, ease: Ease.OutBack));

        Sequence.Create()
            .Chain(pop)
            .Group(Tween.Color(background, startValue: Color.white, endValue: backgroundcolor, duration: 0.2f,
            startDelay: 0.08f,ease: Ease.InQuad))
            //.Group(Tween.LocalPositionY(visual, startValue: 0, endValue: floatHeight, duration: 0.5f, ease: Ease.OutQuad))
            .ChainDelay(0.25f)
            .Chain(Tween.Scale(visual, endValue: Vector3.zero, duration: 0.2f, ease: Ease.InBack))
            .OnComplete(() => Destroy(gameObject));
    }
}