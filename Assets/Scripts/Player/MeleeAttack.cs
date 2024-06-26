using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    public float attackRange = 1f; // Alcance del ataque
    public int damage = 10; // Da�o del ataque
    public Transform attackPoint; // Punto desde donde se lanza el ataque
    public Animator animator; // Controlador de animaciones

    private Vector2 attackDirection; // Direcci�n del ataque

   

    void Update()
    {
        // Verificar si el jugador presiona una tecla de direcci�n para atacar
        if (Input.GetMouseButtonDown(0))
        {
            attackDirection = new Vector2(this.transform.localScale.x, 0);
            attackDirection = attackDirection.normalized;
            Attack();
        }
        
    }

    void Attack()
    {
        // Reproducir la animaci�n de ataque
        if (animator != null)
        {
            animator.SetTrigger("Attack");
        }

        // Detectar enemigos en el rango del ataque usando etiquetas
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position + (Vector3)attackDirection * attackRange, attackRange);

        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.CompareTag("Enemy"))
            {
                bool parry = enemy.GetComponent<EnemyControllerParry>().Parry(3);
                // Aqu� puedes acceder a un script de enemigo y llamar a un m�todo para aplicar da�o
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

