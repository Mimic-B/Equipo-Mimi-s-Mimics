using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 10f;
    public int extraJumps = 0;
    public Transform groundCheck;
    public LayerMask groundLayer;

    public PlayerDash pd;

    private Rigidbody2D rb;
    public bool isGrounded = false;
    private int jumpsLeft;

    public bool isAttacking = false;
    
  
    [SerializeField] Animator animator;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        jumpsLeft = extraJumps;
         
    }

    void Update()
    {

        if(isAttacking) return;

        bool lastGrounded = isGrounded;
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

        if(lastGrounded != isGrounded && isGrounded == true)
        {
            animator.SetTrigger("HitGround");
        }
        if (isGrounded)
        {
            jumpsLeft = extraJumps;
        }



        if (Input.GetKeyDown(KeyCode.Space) && (isGrounded || jumpsLeft > 0 ))
        {
            
           
                Jump();

        }

        
        
    }

    void FixedUpdate()
    {
        if (isAttacking)
        {
            rb.velocity = Vector2.zero;
            return;
        }
        if (pd.IsDashing()) return;

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

       
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        jumpsLeft--;

        float moveInput = 0;
        // Si quieres que el jugador pueda cambiar la dirección del salto, puedes agregar esto:
        if (moveInput != 0)
         {
            
            rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
         }
        animator.SetTrigger("Jump");
        animator.SetBool("HitGround", false);
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
