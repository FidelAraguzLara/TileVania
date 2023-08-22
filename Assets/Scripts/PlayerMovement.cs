using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float m_RunSpeed;
    [SerializeField] float m_JumpSpeed;
    [SerializeField] float m_ClimbingSpeed;
    [SerializeField] Vector2 m_DeathForce;
    [SerializeField] Transform m_Gun;
    [SerializeField] GameObject m_Bullet;

    Vector2 m_MoveInput;
    float m_GravityScale;
    CapsuleCollider2D m_BodyCollider;
    BoxCollider2D m_GroundCollider;
    Rigidbody2D m_RigidBody;
    Animator m_Animator;
    bool m_IsAlive = true;

    void Start()
    {
        m_RigidBody = GetComponent<Rigidbody2D>();
        m_Animator = GetComponent<Animator>();
        m_BodyCollider = GetComponent<CapsuleCollider2D>();
        m_GroundCollider = GetComponent<BoxCollider2D>();
        m_GravityScale = m_RigidBody.gravityScale;
    }

    
    void Update()
    {
        if (!m_IsAlive) { return; }

        ClimbLadder();
        Run();
        FlipSprite();
        Die();
    }

    void OnMove(InputValue a_InputValue)
    {
        if (!m_IsAlive) { return; }

        m_MoveInput = a_InputValue.Get<Vector2>();
        //Debug.Log(m_MoveInput);
    }

    void OnFire()
    {
        if (!m_IsAlive) { return; }

        Instantiate(m_Bullet, m_Gun.position, transform.rotation);
    }

    void OnJump(InputValue a_InputValue)
    {
        if (!m_IsAlive) { return; }

        if (a_InputValue.isPressed && m_GroundCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            m_RigidBody.velocity += new Vector2(0f, m_JumpSpeed);
        }
    }

    void Run()
    {
        Vector2 a_PlayerVelocity = new Vector2(m_MoveInput.x * m_RunSpeed, m_RigidBody.velocity.y);
        m_RigidBody.velocity = a_PlayerVelocity;

        bool a_PlayerHasHorizontalSpeed = Mathf.Abs(m_RigidBody.velocity.x) > Mathf.Epsilon;

        m_Animator.SetBool("IsRunning", a_PlayerHasHorizontalSpeed);
    }

    void ClimbLadder()
    {
        if (m_GroundCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            bool a_PlayerHasVerticalSpeed = Mathf.Abs(m_RigidBody.velocity.y) > Mathf.Epsilon;

            Vector2 a_ClimbVelocity = new Vector2(m_RigidBody.velocity.x, m_MoveInput.y * m_ClimbingSpeed);
            m_RigidBody.velocity = a_ClimbVelocity;
            m_RigidBody.gravityScale = 0f;
            m_Animator.SetBool("IsClimbing", a_PlayerHasVerticalSpeed);
        }
        else
        {
            m_RigidBody.gravityScale = m_GravityScale;
        }
    }

    void FlipSprite()
    {
        bool a_PlayerHasHorizontalSpeed = Mathf.Abs(m_RigidBody.velocity.x) > Mathf.Epsilon;

        if (a_PlayerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(m_RigidBody.velocity.x), 1f);
        }
    }

    void Die()
    {
        if (m_BodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemies", "Hazards")))
        {
            m_IsAlive = false;
            m_Animator.SetTrigger("Dying");
            m_RigidBody.velocity = m_DeathForce;
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }
    }
}
