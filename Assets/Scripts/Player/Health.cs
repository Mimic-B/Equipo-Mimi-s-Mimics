using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float startingHealth; // Vida inicial del jugador
    [SerializeField] private float maxHealth; // Vida máxima del jugador
    public float currentHealth { get; private set; } // Vida actual del jugador, solo puede ser modificada en TakeDamage o AddHealth
    private Animator anim; // Referencia al animador para las animaciones de daño y muerte
    private bool dead; // Verifica si el jugador está muerto
    [SerializeField] UnityEvent onPlayerDeath;

    [Header("iFrames")]
    [SerializeField] private float invulnerabilityDuration; // Duración de la invulnerabilidad después de ser golpeado
    [SerializeField] private int numberOfFlashes; // Número de veces que el jugador parpadea en rojo antes de regresar a su estado normal
    private SpriteRenderer spriteRend; // Necesario para cambiar el color del jugador (rojo) cuando es golpeado

    private void Awake()
    {
        currentHealth = startingHealth; // Se establece la vida inicial del jugador
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float _damage)
    {
        
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, maxHealth); 

       
        if (currentHealth > 0)
        {
            
            anim.SetTrigger("Hurt"); 
            StartCoroutine(Invulnerability()); 
        }
        else
        {
            
            if (!dead)
            {
                
                anim.SetTrigger("die"); 
                GetComponent<PlayerMovement>().enabled = false; 
                dead = true;
                onPlayerDeath.Invoke(); 
            }
        }
    }

    // Este método es para los coleccionables de vida
    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, maxHealth); // Asegura que la vida no sea menor que 0 ni mayor que maxHealth
    }

    // Método para aumentar la vida máxima del jugador
    public void IncreaseMaxHealth(float amount)
    {
        maxHealth += amount;
        currentHealth = maxHealth; // Opcional: ajustar la vida actual para que sea igual a la nueva vida máxima
        Debug.Log("Vida máxima del jugador aumentada: " + maxHealth);
    }

    // Método para iFrames - el jugador parpadea en rojo cuando es golpeado y tiene un breve período de invulnerabilidad
    private IEnumerator Invulnerability()
    {
        Physics2D.IgnoreLayerCollision(8, 9, true); // Ignorar colisiones con objetos enemigos | 8 es la capa del jugador, 9 es la capa del enemigo, true para ignorar colisiones
        for (int i = 0; i < numberOfFlashes; i++) // Bucle para los parpadeos, cambiar el número de parpadeos en Unity
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f); // Cambiar el sprite del jugador a rojo, señalando daño | 1, 0, 0 = rojo | 0.5f es transparencia
            yield return new WaitForSeconds(invulnerabilityDuration / (numberOfFlashes * 2)); // Esperar un poco
            spriteRend.color = Color.white; // Cambia de nuevo al color original, terminando el parpadeo
            yield return new WaitForSeconds(invulnerabilityDuration / (numberOfFlashes * 2)); // Esperar un poco
        }
        Physics2D.IgnoreLayerCollision(8, 9, false); // Reactivar colisiones, terminando la invulnerabilidad del jugador
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

