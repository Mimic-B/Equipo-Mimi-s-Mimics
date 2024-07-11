using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    public Transform[] floatingObjects; // Objetos que flotan alrededor del enemigo
    public float rotationSpeed = 100f; // Velocidad de rotación de los objetos
    public float detectionRange = 5f; // Rango de detección del jugador
    public float attackCooldown = 2f; // Tiempo de espera entre ataques

    private Transform player;
    private bool playerDetected = false;
    private bool isAttacking = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        RotateObjects();

        if (!isAttacking)
        {
            DetectPlayer();
        }
    }

    private void RotateObjects()
    {
        foreach (Transform obj in floatingObjects)
        {
            obj.RotateAround(transform.position, Vector3.forward, rotationSpeed * Time.deltaTime);
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

        foreach (Transform obj in floatingObjects)
        {
            StartCoroutine(LaunchObject(obj));
        }

        yield return new WaitForSeconds(attackCooldown);

        isAttacking = false;
    }

    private System.Collections.IEnumerator LaunchObject(Transform obj)
    {
        Vector2 originalPosition = obj.position;
        Vector2 targetPosition = player.position;

        float elapsedTime = 0f;
        float duration = 0.5f; // Duración del lanzamiento

        while (elapsedTime < duration)
        {
            obj.position = Vector2.Lerp(originalPosition, targetPosition, (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(0.5f); // Tiempo que el objeto permanece en el jugador

        elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            obj.position = Vector2.Lerp(targetPosition, originalPosition, (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}

