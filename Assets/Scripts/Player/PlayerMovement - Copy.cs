using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 10f;
    public int extraJumps = 1;
    public Transform groundCheck;
    public LayerMask groundLayer;

    public PlayerDash pd;

    private Rigidbody2D rb;
    private bool isGrounded = false;
    private int jumpsLeft;

    public Transform wallCheck;
    public LayerMask wallLayer;
    public float wallSlideSpeed = 2f;
    public Vector2 wallJumpDirection = new Vector2(1, 1);
    public float wallJumpForce = 10f;

    private bool isTouchingWall;
    private bool isWallSliding;
    private bool canWallJump;
    [SerializeField] Animator animator;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        jumpsLeft = extraJumps;
        wallJumpDirection.Normalize();  
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

        if (isGrounded)
        {
            jumpsLeft = extraJumps;
        }



        if (Input.GetKeyDown(KeyCode.Space) && (isGrounded || jumpsLeft > 0 || canWallJump))
        {
            if (canWallJump)
            {
                WallJump();
            }
            else
            {
                Jump();
            }
        }

        CheckWallSlide();
        
    }

    void FixedUpdate()
    {
        if(pd.IsDashing()) return;

        float moveInput = 0;
        
        if (Input.GetKey(KeyCode.A)) 
        {
            moveInput = -1;
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x)*-1, transform.localScale.y, transform.localScale.z);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            moveInput = 1;
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }

        animator.SetFloat("Run", Mathf.Abs(moveInput));

        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        if (isWallSliding && rb.velocity.y < -wallSlideSpeed)
        {
            rb.velocity = new Vector2(rb.velocity.x, -wallSlideSpeed);
        }
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        jumpsLeft--;

        // Si quieres que el jugador pueda cambiar la dirección del salto, puedes agregar esto:
        // if (moveInput != 0)
        // {
        //     rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        // }
    }

    void CheckWallSlide()
    {
        isTouchingWall = Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
        isWallSliding = isTouchingWall && !isGrounded && rb.velocity.y < 0;
        canWallJump = isWallSliding;
    }

    void WallJump()
    {
        Vector2 jumpDirection = wallJumpDirection;
        jumpDirection.x *= (transform.localScale.x > 0) ? -1 : 1; // Ajusta la dirección del salto dependiendo de la orientación del jugador
        rb.velocity = new Vector2(jumpDirection.x * wallJumpForce, jumpDirection.y * wallJumpForce);

        // Restablecer saltos disponibles después de un wall jump
        jumpsLeft = extraJumps;
    }

    public void ForcedJump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, 0.2f);
    }
}
