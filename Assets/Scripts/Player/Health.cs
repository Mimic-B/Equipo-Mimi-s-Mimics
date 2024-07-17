using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float startingHealth; // Vida inicial del jugador
    [SerializeField] private float maxHealth; // Vida m�xima del jugador
    public float currentHealth { get; private set; } // Vida actual del jugador, solo puede ser modificada en TakeDamage o AddHealth
    private Animator anim; // Referencia al animador para las animaciones de da�o y muerte
    private bool dead; // Verifica si el jugador est� muerto
    [SerializeField] UnityEvent onPlayerDeath;

    [Header("iFrames")]
    [SerializeField] private float invulnerabilityDuration; // Duraci�n de la invulnerabilidad despu�s de ser golpeado
    [SerializeField] private int numberOfFlashes; // N�mero de veces que el jugador parpadea en rojo antes de regresar a su estado normal
    private SpriteRenderer spriteRend; // Necesario para cambiar el color del jugador (rojo) cuando es golpeado

    private void Awake()
    {
        currentHealth = startingHealth; // Se establece la vida inicial del jugador
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float _damage)
    {
        // Reducir la vida del jugador
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, maxHealth); // Asegura que la vida no sea menor que 0 ni mayor que maxHealth

        // Verificar si el jugador est� muerto o herido
        if (currentHealth > 0)
        {
            // El jugador solo est� herido
            anim.SetTrigger("Hurt"); // Animaci�n de da�o
            StartCoroutine(Invulnerability()); // Mostrar los iFrames (el jugador parpadea en rojo)
        }
        else
        {
            // El jugador muri� debido al da�o
            if (!dead)
            {
                // Este if statement asegura que la animaci�n de muerte solo se ejecute una vez
                anim.SetTrigger("die"); // Animaci�n de muerte
                GetComponent<PlayerMovement>().enabled = false; // Deshabilitar el movimiento del jugador, no puede moverse cuando est� muerto
                dead = true;
                onPlayerDeath?.Invoke(); // Invoca eventos adicionales en la muerte del jugador
            }
        }
    }

    // Este m�todo es para los coleccionables de vida
    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, maxHealth); // Asegura que la vida no sea menor que 0 ni mayor que maxHealth
    }

    // M�todo para iFrames - el jugador parpadea en rojo cuando es golpeado y tiene un breve per�odo de invulnerabilidad
    private IEnumerator Invulnerability()
    {
        Physics2D.IgnoreLayerCollision(8, 9, true); // Ignorar colisiones con objetos enemigos | 8 es la capa del jugador, 9 es la capa del enemigo, true para ignorar colisiones
        for (int i = 0; i < numberOfFlashes; i++) // Bucle para los parpadeos, cambiar el n�mero de parpadeos en Unity
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f); // Cambiar el sprite del jugador a rojo, se�alando da�o | 1, 0, 0 = rojo | 0.5f es transparencia
            yield return new WaitForSeconds(invulnerabilityDuration / (numberOfFlashes * 2)); // Esperar un poco
            spriteRend.color = Color.white; // Cambia de nuevo al color original, terminando el parpadeo
            yield return new WaitForSeconds(invulnerabilityDuration / (numberOfFlashes * 2)); // Esperar un poco
        }
        Physics2D.IgnoreLayerCollision(8, 9, false); // Reactivar colisiones, terminando la invulnerabilidad del jugador
    }

    public void IncreaseHP (float amount)
    {
        maxHealth += amount;
    }



}


// test to see if TakeDamage works, press E to take damage
//private void Update()
//{
//   if (Input.GetKeyDown(KeyCode.E))
// {
//   TakeDamage(1); // take 1 damage
//}
//}

