using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Collider2D playerCollider;

    public LayerMask platformLayer;
    public float dropDelay = 0.1f;  // Tiempo para caer a través de la plataforma

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S) || (Input.GetKey(KeyCode.S) && Input.GetKeyDown(KeyCode.Space)))
        {
            DropThroughPlatform();
        }
    }

    void DropThroughPlatform()
    {
        // Desactivar colisión entre el jugador y la plataforma
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.1f, platformLayer);

        foreach (Collider2D collider in colliders)
        {
            if (collider != playerCollider)
            {
                StartCoroutine(DisableCollisionTemporarily(collider));
            }
        }
    }

    System.Collections.IEnumerator DisableCollisionTemporarily(Collider2D platformCollider)
    {
        Physics2D.IgnoreCollision(playerCollider, platformCollider, true);
        yield return new WaitForSeconds(dropDelay);
        Physics2D.IgnoreCollision(playerCollider, platformCollider, false);
    }
}

