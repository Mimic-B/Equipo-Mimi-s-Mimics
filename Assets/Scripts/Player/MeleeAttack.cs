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
    public float delayTime = 0.5f;
    public float currentdelayTime = 0;
    public AllyController ally;
    
    public Collider2D attackCollider;

    void Update()
    {
        // Verificar si el jugador presiona una tecla de dirección para atacar
        if (Input.GetMouseButtonDown(0))
        {
            attackDirection = new Vector2(this.transform.localScale.x, 0);
            attackDirection = attackDirection.normalized;
            Attack();
            ally.ActivateAlly();
        }

        if(combo > 0)
        {
            currentdelayTime += Time.deltaTime;
            currentComboTime += Time.deltaTime;
            if(resetComboTime <= currentComboTime)
            {
                currentComboTime = 0;
                currentdelayTime = 0;
                combo = 0;

                animator.SetBool("Attack3", false);
                animator.SetBool("Attack2", false);
                animator.SetBool("Attack", false);
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
            else if (combo == 1 && currentdelayTime > delayTime)
            {
                animator.SetTrigger("Attack2");
                combo += 1;
                currentComboTime = 0;
                currentdelayTime = 0;
            }
            else if (combo == 2 && currentdelayTime > delayTime)
            {
                animator.SetTrigger("Attack3");
                combo += 1;
                currentComboTime = 0;
                currentdelayTime = 0;
            }
        }

        // Detectar enemigos en el rango del ataque usando etiquetas
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position + (Vector3)attackDirection * attackRange, attackRange);

        foreach (Collider2D enemy in hitEnemies)
        {
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if(collision.IsTouching(attackCollider) && attackCollider.isActiveAndEnabled) { 
                Debug.Log("Hit");
                bool parry = collision.gameObject.GetComponent<EnemyControllerParry>().Parry(3);
                // Aquí puedes acceder a un script de enemigo y llamar a un método para aplicar daño
                // Por ejemplo: enemy.GetComponent<Enemy>().TakeDamage(damage);
                if (parry == false)
                {
                    collision.gameObject.GetComponent<EnemyHealth>().Hurt(damage);
                }
            }

        }
    }


    

   
}

