using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AllyController : MonoBehaviour
{
    public GameObject player;          // Referencia al jugador para seguirlo
    public float appearDuration = 1f;  // Duración de la aparición en segundos
    public float disappearDuration = 1f; // Duración de la desaparición en segundos
    public float cooldown = 2f;        // Tiempo de cooldown entre apariciones
    public Animator animator;

    private bool canAppear = true;     // Variable para controlar si se puede aparecer el aliado

    public MeleeAttack MeleeATK;

    void Start()
    {
        // Inicialmente, el aliado no debe estar activo
        gameObject.SetActive(false);
    }

    void Update()
    {
        // Si el aliado puede aparecer y se puede ver al jugador, seguirlo
        if (canAppear && player != null)
        {
            transform.position = player.transform.position;
        }
        
        if (MeleeATK.combo > 0)
        {
            MeleeATK.currentComboTime += Time.deltaTime;
            if (MeleeATK.resetComboTime <= MeleeATK.currentComboTime)
            {
                MeleeATK.currentComboTime = 0;
                MeleeATK.combo = 0;

                animator.SetBool("Attack2", false);
                animator.SetBool("Attack", false);
            }
        }
    }

    public void ActivateAlly()
    {
        if (canAppear)
        {
            canAppear = false;

            // Aparecer el aliado
            gameObject.SetActive(true);

            // Después de la duración de la aparición, desaparecer
            Invoke("DeactivateAlly", appearDuration);
        }
        if (animator != null)
        {
            if (MeleeATK.combo == 0)
            {
                animator.SetTrigger("Attack");
                MeleeATK.combo += 1;
                MeleeATK.currentComboTime = 0;
            }
            else if (MeleeATK.combo == 1)
            {
                animator.SetTrigger("Attack2");
                MeleeATK.combo += 1;
                MeleeATK.currentComboTime = 0;
            }
            else if (MeleeATK.combo == 2)
            {
                animator.SetTrigger("Attack3");
                MeleeATK.combo += 1;
                MeleeATK.currentComboTime = 0;
            }
        }
    }

    void DeactivateAlly()
    {
        // Desaparecer el aliado
        gameObject.SetActive(false);

        // Reiniciar el cooldown para poder aparecer de nuevo
        Invoke("ResetCooldown", cooldown);
    }

    void ResetCooldown()
    {
        // Permitir que el aliado pueda aparecer de nuevo
        canAppear = true;
    }
}

