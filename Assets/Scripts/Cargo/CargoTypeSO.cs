using UnityEngine;
using static UnityEngine.Rendering.GPUSort;

[CreateAssetMenu(menuName = "Cargo/Cargo Type")]
public class CargoTypeSO : ScriptableObject
{
    public Sprite icon;
    public Cargo cargoPrefab;
    public int deliveryScore;
}