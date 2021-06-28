using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] AudioClip coinSFX;
    [SerializeField] int coinScore = 1;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        AudioSource.PlayClipAtPoint(coinSFX, Camera.main.transform.position);
        FindObjectOfType<GameSession>().AddCoin(coinScore);
        
        Destroy(gameObject);
    }
}
