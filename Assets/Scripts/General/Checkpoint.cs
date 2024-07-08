using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{

    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    public Color CheckColor = Color.green;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Guardar la posición del checkpoint
            CheckpointManager.instance.SetCheckpoint(transform.position);
            spriteRenderer.color = CheckColor;
        }
    }
}
