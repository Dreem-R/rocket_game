using UnityEngine;
using UnityEngine.Scripting;

public class CargoZone : MonoBehaviour
{
    private enum Mode { Pickup, Drop }

    [SerializeField] private Mode mode;
    [SerializeField] private CargoTypeSO cargoType;
    [SerializeField] private float requiredTime=1.5f;

    [Header("Visual")]
    [SerializeField] private SpriteRenderer background;
    [SerializeField] private SpriteRenderer ZoneProgress;
    [SerializeField] private SpriteRenderer icon;
    [SerializeField] private Color pickupColor = new Color(0.2f, 0.6f, 1f);
    [SerializeField] private Color dropColor = new Color(0.6f, 0.6f, 0.6f);


    private Lander landerInside;
    private float timer;
    private Material Progress_material;
    private static readonly int FillId = Shader.PropertyToID("_Fill");

    private void Awake()
    {
        Progress_material = ZoneProgress.material;
    }

    private void OnValidate()
    {
        if (background == null || icon == null) return;

        if (mode == Mode.Pickup)
        {
            background.color = pickupColor;
        }
        else if (mode == Mode.Drop)
        {
            background.color = dropColor;
        }

        if (cargoType != null)
        {
            icon.sprite = cargoType.icon;
        }
    }
    private void Update()
    {
        if (landerInside == null) return;
        timer += Time.deltaTime;
        Progress_material.SetFloat(FillId, (timer / requiredTime));
        if (timer > requiredTime) {
            if (mode == Mode.Pickup) CompletePickup();
        }
    }

    private void CompletePickup()
    {
        Progress_material.SetFloat(FillId, 1f);
        Instantiate(cargoType.cargoPrefab)
                .Attach(landerInside.GetComponent<Rigidbody2D>());
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.TryGetComponent(out Lander lander)) return;
        landerInside = collision.GetComponent<Lander>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.TryGetComponent(out Lander lander)) return;
        landerInside = null;
        timer = 0;
        Progress_material.SetFloat(FillId, 0f);
    }

    private void OnDestroy()
    {
        Destroy(Progress_material);
    }
}