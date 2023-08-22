using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float m_BulletSpeed;
    float m_HorizontalSpeed;

    Rigidbody2D m_RigidBody;

    PlayerMovement m_PlayerMovement;

    void Start()
    {
        m_PlayerMovement = FindObjectOfType<PlayerMovement>();
        m_RigidBody = GetComponent<Rigidbody2D>();

        m_HorizontalSpeed = m_PlayerMovement.transform.localScale.x * m_BulletSpeed;
    }

    void Update()
    {
        m_RigidBody.velocity = new Vector2(m_HorizontalSpeed, 0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
