using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> items = new List<Item>(); // Lista de ítems en el inventario
    public int coins; // Monedas del jugador

    public void AddItem(Item newItem)
    {
        items.Add(newItem);
        Debug.Log("Item agregado al inventario: " + newItem.itemName);
    }
}

