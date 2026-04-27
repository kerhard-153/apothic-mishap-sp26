using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Xml;



public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;

    public PauseManager pauseManager;

    [Header("Movement")]
    public float moveSpeed = 5f;
    float horizontalMovement;

    [Header("Jumping")]
    public float jumpPower = 10f;
    public int maxJumps = 2;
    int jumpsRemaining;

    [Header("GroundCheck")]
    public Transform groundCheckPos;
    public Vector2 groundCheckSize = new Vector2(0.5f, 0.5f);
    public LayerMask groundLayer;
    private bool isGrounded;

    [Header("Fall Damage")]
    public float fallDamageThreshold = -5f;
    private bool wasGrounded;
    private float minYVelocity;
    private bool hasTakenFallDamage;



    public Animator anim;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        anim = GetComponent<Animator>();
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = new Vector2(horizontalMovement * moveSpeed, rb.linearVelocity.y);
        anim.SetBool("isMoving", horizontalMovement != 0);
        anim.SetBool("isGrounded", isGrounded);

        if (horizontalMovement > 0) {
            transform.localScale = new Vector3(1, 1, 1);
        } else if (horizontalMovement < 0) {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        GroundCheck();

        
    }

    void FixedUpdate()
    {
        float currentY = rb.linearVelocity.y;

        if (!isGrounded)
        {
            if (currentY < minYVelocity)
            {
                minYVelocity = currentY;
            }
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        horizontalMovement = context.ReadValue<Vector2>().x;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (jumpsRemaining > 0)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);
            jumpsRemaining --;

            anim.SetTrigger("isJumping");

            if (jumpsRemaining == 1)
            {
                // play particles
            }
        }
        
    }

    private void GroundCheck()
    {
        isGrounded = Physics2D.OverlapBox(groundCheckPos.position, groundCheckSize, 0, groundLayer);

        // just left the ground
        if (!isGrounded && wasGrounded)
        {
            minYVelocity = 0;
            hasTakenFallDamage = false;
        }

        // tracks fall speed
        if (!isGrounded)
        {
            float currentY = rb.linearVelocity.y;
            if (currentY < minYVelocity)
            {
                minYVelocity = currentY;
            }
        }

        // landed
        if (isGrounded && !wasGrounded)
        {
            if (!hasTakenFallDamage && minYVelocity < fallDamageThreshold)
            {
                GetComponent<PlayerHealth>().TakeDamage(1);
                hasTakenFallDamage = true;
            }
        }

        if (isGrounded)
        {
            jumpsRemaining = maxJumps;
        }

        wasGrounded = isGrounded;

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(groundCheckPos.position, groundCheckSize);
    }

    public void Pause(InputAction.CallbackContext context)
{
    if (context.performed)
    {
        Debug.Log("PAUSE PRESSED");
        pauseManager.TogglePause();
    }
}

}
