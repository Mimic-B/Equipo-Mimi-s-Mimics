using UnityEngine;

public class EnemyMeleeAttack : MonoBehaviour
{
    public EnemyControllerParry enemyControllerParry;
    public float attackRange = 1f; // Rango de ataque del enemigo
    public int attackDamage = 10; // Da�o del ataque
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
        // Ejecutar animaci�n de ataque
        animator.SetTrigger("Attack");

        // Verificar si el jugador est� en el rango de ataque
        Collider2D hitPlayer = Physics2D.OverlapCircle(transform.position, attackRange, playerLayer);
        if (hitPlayer != null)
        {
            // Aplicar da�o al jugador (asumiendo que el jugador tiene un script PlayerHealth)
            Health playerHealth = hitPlayer.GetComponent<Health>();
            if (playerHealth != null)
            {
                // Aqu� podr�as verificar si el jugador ha realizado un parry exitoso
                if (enemyControllerParry.pn.isParryPsobleNow)
                {
                    // Si el parry es posible, el ataque del enemigo es bloqueado y el enemigo es aturdido
                    enemyControllerParry.Parry(2f); // La duraci�n del aturdimiento puede ser ajustada
                }
                else
                {
                    // Si el parry no es posible, el jugador recibe da�o
                    playerHealth.TakeDamage(attackDamage);
                }
            }
        }

        // Iniciar el cooldown de ataque
        canAttack = false;
        attackTimer = attackCooldown;
    }

    // M�todo para visualizar el rango de ataque en la escena
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}

