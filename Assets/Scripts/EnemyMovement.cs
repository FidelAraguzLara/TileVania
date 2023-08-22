using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float m_EnemySpeed;

    Rigidbody2D m_RigidBody;
    BoxCollider2D m_BoxCollider;

    void Start()
    {
        m_RigidBody = GetComponent<Rigidbody2D>();
        m_BoxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        m_RigidBody.velocity = new Vector2(m_EnemySpeed, 0f);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        m_EnemySpeed = -m_EnemySpeed;
        FlipSprite();
    }

    void FlipSprite()
    {
        transform.localScale = new Vector2(-(Mathf.Sign(m_RigidBody.velocity.x)), 1f);
    }
}
