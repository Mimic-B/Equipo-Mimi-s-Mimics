using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    public float attackRange = 1f; // Alcance del ataque
    public int damage = 10; // Daño del ataque
    public Transform attackPoint; // Punto desde donde se lanza el ataque
    public Animator animator; // Controlador de animaciones

    private Vector2 attackDirection; // Dirección del ataque
    public int combo = 0;
    public float resetComboTime = 1;
    public float currentComboTime = 0;

    void Update()
    {
        // Verificar si el jugador presiona una tecla de dirección para atacar
        if (Input.GetMouseButtonDown(0))
        {
            attackDirection = new Vector2(this.transform.localScale.x, 0);
            attackDirection = attackDirection.normalized;
            Attack();
        }

        if(combo > 0)
        {
            currentComboTime += Time.deltaTime;
            if(resetComboTime <= currentComboTime)
            {
                currentComboTime = 0;
                combo = 0;

                animator.SetBool("Attack2", false);
                animator.SetBool("Attack3", false);
            }
        }
        
    }

    void Attack()
    {
        // Reproducir la animación de ataque
        if (animator != null)
        {
            if(combo == 0) { 
                animator.SetTrigger("Attack");
                combo +=1;
                currentComboTime = 0;
            }
            else if (combo == 1)
            {
                animator.SetTrigger("Attack2");
                combo += 1;
                currentComboTime = 0;
            }
            else if (combo == 2)
            {
                animator.SetTrigger("Attack3");
                combo += 1;
                currentComboTime = 0;
            }
        }

        // Detectar enemigos en el rango del ataque usando etiquetas
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position + (Vector3)attackDirection * attackRange, attackRange);

        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.CompareTag("Enemy"))
            {
                bool parry = enemy.GetComponent<EnemyControllerParry>().Parry(3);
                // Aquí puedes acceder a un script de enemigo y llamar a un método para aplicar daño
                // Por ejemplo: enemy.GetComponent<Enemy>().TakeDamage(damage);
                if(parry == false) { 
                    enemy.GetComponent<EnemyHealth>().Hurt(damage);
                }
              
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position + (Vector3)attackDirection * attackRange, attackRange);
    }

   
}

