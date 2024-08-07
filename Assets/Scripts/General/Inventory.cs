using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<IInventoryItem> items = new List<IInventoryItem>(); // Lista de �tems en el inventario
    public int coins; // Monedas del jugador

    private Health playerHealth;
   
    private MeleeAttack meleeAttack;

    private void Start()
    {
        playerHealth = FindObjectOfType<Health>();
        
        meleeAttack = FindObjectOfType<MeleeAttack>();
    }

    public void AddItem(IInventoryItem newItem)
    {
        items.Add(newItem);
        Debug.Log("Item agregado al inventario: " + newItem.itemName);

        if (newItem is MaxHealthItem maxHealthItem)
        {
            ApplyItemEffect(maxHealthItem);
        }
        else if (newItem is DamageBoostItem damageBoostItem)
        {
            damageBoostItem.Use(meleeAttack);
        }
    }

    private void ApplyItemEffect(MaxHealthItem item)
    {
        if (item.maxHealthIncrease > 0)
        {
            playerHealth.IncreaseMaxHealth(item.maxHealthIncrease);
        }
    }
}

