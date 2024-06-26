using UnityEngine;

public class EnemyControllerParry : MonoBehaviour
{
    public IsPArryPosible pn;
    public bool isStunned = false; // Booleano que indica si el enemigo est� aturdido
    public float stunTimer = 0f; // Temporizador para el aturdimiento
    [SerializeField] Animator animator;
    // M�todo para aplicar el parry al enemigo
    public bool Parry(float stunDuration)
    {
        if (pn.isParryPsobleNow && !isStunned)
        {
            // Realizar acciones relacionadas con el parry aqu�
            isStunned = true; // El enemigo est� aturdido

            // Puedes ejecutar aqu� alguna animaci�n de aturdimiento o efecto visual
            animator.SetBool("Stuned", true);
            // Iniciar el temporizador de aturdimiento
            stunTimer = stunDuration;

            // Aqu� podr�as desactivar temporalmente la capacidad de ser parried, si es necesario
            // Puedes implementar esa l�gica seg�n tus necesidades
            pn.isParryPsobleNow = false;

            // L�gica adicional, por ejemplo, detener movimiento, desactivar comportamientos, etc.
            return true;
        }
        return false;
    }

    void Update()
    {
        // Actualizar el temporizador de aturdimiento si el enemigo est� aturdido
        if (isStunned)
        {
            stunTimer -= Time.deltaTime;

            if (stunTimer <= 0f)
            {
                // Cuando el temporizador de aturdimiento llega a cero, restaurar el estado
                isStunned = false;
                pn.isParryPsobleNow = true; // Restaurar la capacidad de ser parried, si es necesario
                animator.SetBool("Stuned", false);
                // Aqu� podr�as reactivar comportamientos, animaciones, etc.
            }
        }
    }
}

