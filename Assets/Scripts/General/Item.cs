using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject, IInventoryItem
{
    [SerializeField] private string _itemName;
    public Sprite itemIcon;
    public string itemName => _itemName;
    
    public void Use()
    {
        // Implementar el comportamiento del ítem aquí
        Debug.Log("Usando item: " + itemName);
    }
}

