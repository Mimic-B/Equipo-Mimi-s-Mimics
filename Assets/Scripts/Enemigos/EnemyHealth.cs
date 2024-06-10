using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int health;
    [SerializeField] int currentHealth;
    [SerializeField] UnityEvent onEnemyDeath;
    [SerializeField] Image healthBar;


    public void Start()
    {
        currentHealth = health;
        UpdateCurrentHeath();
    }

    public void Hurt(int damage)
    {
        currentHealth -= damage;
        UpdateCurrentHeath();
        if (currentHealth <= 0)
        {
            onEnemyDeath.Invoke();
            Destroy(gameObject);
        }
    }

    public void Heal(int damage)
    {
        UpdateCurrentHeath();
        currentHealth += damage;
    }

    void UpdateCurrentHeath()
    {
        healthBar.fillAmount = (1.0f * currentHealth)/health;
    }
}
