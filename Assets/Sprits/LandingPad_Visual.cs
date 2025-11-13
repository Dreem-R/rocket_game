using TMPro;
using UnityEngine;

public class LandingPad_Visual : MonoBehaviour
{
    [SerializeField] private TextMeshPro mutiplier_text_mesh;

    private void Awake()
    {
        LandingPad landing = GetComponent<LandingPad>();
        mutiplier_text_mesh.text = "x" + landing.get_multipier();
    }

}
