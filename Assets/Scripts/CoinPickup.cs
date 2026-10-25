using System;
using PrimeTween;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    public event EventHandler OnCollected;

    [SerializeField] private float destroyDelay = 0.5f;
    public void DestroySelf()
    {
        GetComponent<Collider2D>().enabled = false;
        OnCollected?.Invoke(this, EventArgs.Empty);   // announce it; whoever cares reacts
        Destroy(gameObject, destroyDelay);
    }
}
