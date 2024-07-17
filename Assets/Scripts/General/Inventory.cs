using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> items = new List<Item>(); // Lista de ítems en el inventario
    public int coins; // Monedas del jugador

    private Health health;

    private void Start()
    {
        health = GetComponent<Health>();
    }
    public void AddItem(Item newItem)
    {
        items.Add(newItem);
        Debug.Log("Item agregado al inventario: " + newItem.itemName);

        ApplyItemEffect(newItem);
    }

    private void ApplyItemEffect(ItemaumentoHP itemaumentoHP)
    {
        if (itemaumentoHP.maxhpincrease > 0)
        {
            Health.IncreaseHP(itemaumentoHP.maxhpincrease);
        }
    }
}

