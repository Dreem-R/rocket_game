using System.Collections;
using UnityEngine;

public class PopupTester : MonoBehaviour
{
    [SerializeField] private ScorePopup scorePopupPrefab;

    void Start()
    {
        StartCoroutine(TestPopup());
    }

    private IEnumerator TestPopup()
    {
        while (true)
        {
            yield return new WaitForSeconds(3f);
            ScorePopup popup = Instantiate(scorePopupPrefab);
            popup.Setup("+100");
        }
    }
}
