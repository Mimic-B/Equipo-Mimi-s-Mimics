using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    public float patrolSpeed = 2f;
    public float attackSpeed = 3f;
    public float patrolDistance = 5f;
    public float detectionRange = 3f;
    public float attackDelay = 1f;
    public int damage = 10;

    private Vector2 startPoint;
    private bool movingRight = true;
    private bool playerDetected = false;
    private bool attacking = false;

    public Transform player;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask playerLayer;

    private Rigidbody2D rb;
    private Animator animator;

    private void Start()
    {
        startPoint = transform.position;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!playerDetected && !attacking)
        {
            Patrol();
        }

        DetectPlayer();
    }

    private void Patrol()
    {
        if (movingRight)
        {
            rb.velocity = new Vector2(patrolSpeed, rb.velocity.y);
            if (Vector2.Distance(startPoint, transform.position) >= patrolDistance)
            {
                movingRight = false;
                Flip();
            }
        }
        else
        {
            rb.velocity = new Vector2(-patrolSpeed, rb.velocity.y);
            if (Vector2.Distance(startPoint, transform.position) <= 0)
            {
                movingRight = true;
                Flip();
            }
        }
    }

    private void DetectPlayer()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer <= detectionRange)
        {
            playerDetected = true;
            StopPatrol();
            MoveTowardsPlayer();
        }
        else
        {
            playerDetected = false;
        }
    }

    private void StopPatrol()
    {
        rb.velocity = Vector2.zero;
    }

    private void MoveTowardsPlayer()
    {
        if (playerDetected && !attacking)
        {
            Vector2 targetPosition = new Vector2(player.position.x, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, attackSpeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, targetPosition) <= 0.1f)
            {
                StartCoroutine(AttackPlayer());
            }
        }
    }

    private System.Collections.IEnumerator AttackPlayer()
    {
        attacking = true;
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(attackDelay);

        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);

        foreach (Collider2D player in hitPlayers)
        {
            player.GetComponent<Health>().TakeDamage(damage);
        }

        attacking = false;
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    private void Flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
}

