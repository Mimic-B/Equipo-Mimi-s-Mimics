using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBoostItem : MonoBehaviour, IInventoryItem
{
    public string itemName => "Damage Boost Item";
    public int damageBoostAmount = 5;

    public void Use(MeleeAttack meleeAttack)
    {
        if (meleeAttack != null)
        {
            meleeAttack.IncreaseDamage(damageBoostAmount);
            Debug.Log("Damage increased by " + damageBoostAmount);
        }
        else
        {
            Debug.LogError("MeleeAttack script not found!");
        }
    }

    void IInventoryItem.Use()
    {
        // No implementation needed for this example.
    }
}



