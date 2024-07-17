using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    public float dashSpeed = 15f;
    public float dashDuration = 0.2f;
    public float doubleTapTime = 0.3f;
    public float dashCooldown = 1.3f; // Tiempo de cooldown entre dashes
    public Color dashColor = Color.red;
    public int baseDashCount = 1; // Número base de dashes disponibles

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    private bool isDashing;
    private float dashTimeLeft;
    private float lastTapTime = 0f;
    private string lastButton = "";
    private bool isInvulnerable = false;
    private float lastDashTime;
    private int remainingDashes;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        remainingDashes = baseDashCount;
    }

    void Update()
    {
        HandleDashInput();
    }

    void FixedUpdate()
    {
        if (isDashing)
        {
            Dash();
        }
    }

    void HandleDashInput()
    {
        if (remainingDashes <= 0) return;

        if (Input.GetKeyDown(KeyCode.A))
        {
            if (Time.time - lastTapTime < doubleTapTime && lastButton == "Horizontal")
            {
                StartDash();
            }
            lastButton = "Horizontal";
            lastTapTime = Time.time;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            if (Time.time - lastTapTime < doubleTapTime && lastButton == "Horizontal")
            {
                StartDash();
            }
            lastButton = "Horizontal";
            lastTapTime = Time.time;
        }
    }

    void StartDash()
    {
        if (Time.time - lastDashTime < dashCooldown || remainingDashes <= 0) return;

        isDashing = true;
        isInvulnerable = true;
        dashTimeLeft = dashDuration;
        lastDashTime = Time.time;
        remainingDashes--;
        rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized * dashSpeed;
        spriteRenderer.color = dashColor;
        Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("Obstacles"), true);
    }

    void Dash()
    {
        if (dashTimeLeft > 0)
        {
            dashTimeLeft -= Time.deltaTime;
        }
        else
        {
            isDashing = false;
            isInvulnerable = false;
            spriteRenderer.color = originalColor;
            Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("Obstacles"), false);
        }
    }

    public bool IsInvulnerable()
    {
        return isInvulnerable;
    }

    public bool IsDashing()
    {
        return isDashing;
    }

    public void IncreaseDashes(int amount)
    {
        remainingDashes += amount;
    }
}
