using UnityEngine;

public class EnemyMeleeAttack : MonoBehaviour
{
    public EnemyControllerParry enemyControllerParry;
    public float attackRange = 1f; // Rango de ataque del enemigo
    public int attackDamage = 10; // Daño del ataque
    public LayerMask playerLayer; // Capa del jugador
    public float attackCooldown = 1f; // Tiempo de espera entre ataques

    private float attackTimer = 0f; // Temporizador para el cooldown de ataque
    private bool canAttack = true; // Indica si el enemigo puede atacar
    [SerializeField] Animator animator;

    void Update()
    {
        // Reducir el temporizador de cooldown de ataque
        attackTimer -= Time.deltaTime;

        if (attackTimer <= 0f)
        {
            canAttack = true;
        }

        // Verificar si el enemigo puede atacar
        if (canAttack && !enemyControllerParry.isStunned)
        {
            Attack();
        }
    }

    void Attack()
    {
        // Ejecutar animación de ataque
        animator.SetTrigger("Attack");

        // Verificar si el jugador está en el rango de ataque
        Collider2D hitPlayer = Physics2D.OverlapCircle(transform.position, attackRange, playerLayer);
        if (hitPlayer != null)
        {
            // Aplicar daño al jugador (asumiendo que el jugador tiene un script PlayerHealth)
            Health playerHealth = hitPlayer.GetComponent<Health>();
            if (playerHealth != null)
            {
                // Aquí podrías verificar si el jugador ha realizado un parry exitoso
                if (enemyControllerParry.pn.isParryPsobleNow)
                {
                    // Si el parry es posible, el ataque del enemigo es bloqueado y el enemigo es aturdido
                    enemyControllerParry.Parry(2f); // La duración del aturdimiento puede ser ajustada
                }
                else
                {
                    // Si el parry no es posible, el jugador recibe daño
                    playerHealth.TakeDamage(attackDamage);
                }
            }
        }

        // Iniciar el cooldown de ataque
        canAttack = false;
        attackTimer = attackCooldown;
    }

    // Método para visualizar el rango de ataque en la escena
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}

