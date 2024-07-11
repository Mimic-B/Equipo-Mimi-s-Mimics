using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundAttackEnemy : MonoBehaviour
{
    public float detectionRange = 5f; // Rango de detección del jugador
    public float attackDelay = 1f; // Retraso antes de que ocurra el ataque
    public GameObject alertPrefab; // Prefab de alerta que aparece antes del ataque
    public GameObject attackPrefab; // Prefab de la raíz, cristal, etc., que ataca al jugador
    public Transform player;

    private bool playerDetected = false;
    private bool isAttacking = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
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
        if (Vector2.Distance(transform.position, player.position) <= detectionRange)
        {
            playerDetected = true;
            if (!isAttacking)
            {
                StartCoroutine(Attack());
            }
        }
        else
        {
            playerDetected = false;
        }
    }

    private System.Collections.IEnumerator Attack()
    {
        isAttacking = true;

        Vector2 playerPosition = new Vector2(player.position.x, player.position.y - 1); // Posición debajo del jugador
        GameObject alert = Instantiate(alertPrefab, playerPosition, Quaternion.identity);

        yield return new WaitForSeconds(attackDelay);

        Destroy(alert);
        Instantiate(attackPrefab, playerPosition, Quaternion.identity);

        yield return new WaitForSeconds(2f); // Cooldown antes de que el enemigo pueda atacar de nuevo

        isAttacking = false;
    }
}

