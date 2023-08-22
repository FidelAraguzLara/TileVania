using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] AudioClip m_CoinSound;

    [SerializeField] int m_CoinPoints;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            FindObjectOfType<GameSession>().IncreasePlayerScore(m_CoinPoints);
            AudioSource.PlayClipAtPoint(m_CoinSound, Camera.main.transform.position);
            Destroy(gameObject); 
        }
    }
}
