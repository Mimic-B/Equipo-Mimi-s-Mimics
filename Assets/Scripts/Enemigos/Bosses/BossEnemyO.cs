using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyO : MonoBehaviour
{
    public GameObject projectilePrefab; // Prefab del proyectil
    public Transform attackPoint; // Punto desde donde se lanzan los proyectiles
    public float detectionRange = 10f; // Rango de detección del jugador
    public float meleeRange = 2f; // Rango de ataque cuerpo a cuerpo
    public float attackCooldown = 2f; // Tiempo de espera entre ataques
    public float launchForce = 10f; // Fuerza de lanzamiento del proyectil
    public int meleeDamage = 20; // Daño del ataque cuerpo a cuerpo
    public float attackDelay = 0.5f; // Retraso antes de golpear con el ataque cuerpo a cuerpo

    private Transform player;
    private bool playerDetected = false;
    private bool isAttacking = false;

    private Animator animator;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!isAttacking)
        {
            DetectPlayer();
        }
    }

    private void DetectPlayer()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer <= detectionRange)
        {
            playerDetected = true;
            if (!isAttacking)
            {
                if (distanceToPlayer > meleeRange)
                {
                    StartCoroutine(RangedAttack());
                }
                else
                {
                    StartCoroutine(MeleeAttack());
                }
            }
        }
        else
        {
            playerDetected = false;
        }
    }

    private System.Collections.IEnumerator RangedAttack()
    {
        isAttacking = true;

        // Animación de ataque a distancia (si la tienes)
        animator.SetTrigger("RangedAttack");

        yield return new WaitForSeconds(attackDelay);

        // Genera el proyectil y lo lanza hacia el jugador
        GameObject projectile = Instantiate(projectilePrefab, attackPoint.position, Quaternion.identity);
        LaunchProjectile(projectile);

        yield return new WaitForSeconds(attackCooldown);

        isAttacking = false;
    }

    private void LaunchProjectile(GameObject projectile)
    {
        Vector2 targetPosition = player.position;

        // Calcula la dirección del lanzamiento
        Vector2 direction = (targetPosition - (Vector2)projectile.transform.position).normalized;

        // Obtén el Rigidbody2D del proyectil
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

        // Aplica la fuerza de lanzamiento
        rb.AddForce(direction * launchForce, ForceMode2D.Impulse);
    }

    private System.Collections.IEnumerator MeleeAttack()
    {
        isAttacking = true;

        // Animación de ataque cuerpo a cuerpo
        animator.SetTrigger("MeleeAttack");

        yield return new WaitForSeconds(attackDelay);

        // Verifica si el jugador está en el rango de ataque cuerpo a cuerpo
        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(attackPoint.position, meleeRange);

        foreach (Collider2D hit in hitPlayers)
        {
            if (hit.CompareTag("Player"))
            {
                hit.GetComponent<Health>().TakeDamage(meleeDamage);
            }
        }

        yield return new WaitForSeconds(attackCooldown);

        isAttacking = false;
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, meleeRange);
    }
}
