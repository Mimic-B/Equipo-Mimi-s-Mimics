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

   

    void Update()
    {
        // Verificar si el jugador presiona una tecla de dirección para atacar
        if (Input.GetMouseButtonDown(0))
        {
            attackDirection = new Vector2(this.transform.localScale.x, 0);
            attackDirection = attackDirection.normalized;
            Attack();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {

            attackDirection = Vector2.right;
            Attack();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            attackDirection = Vector2.left;
            Attack();
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            attackDirection = Vector2.up;
            Attack();
        }
    }

    void Attack()
    {
        // Reproducir la animación de ataque
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
                // Aquí puedes acceder a un script de enemigo y llamar a un método para aplicar daño
                // Por ejemplo: enemy.GetComponent<Enemy>().TakeDamage(damage);
                enemy.GetComponent<EnemyHealth>().Hurt(damage);
              
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

