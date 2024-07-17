using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Max Health Item", menuName = "Inventory/Max Health Item")]
public class MaxHealthItem : ScriptableObject, IInventoryItem
{
    [SerializeField] private string _itemName;
    [SerializeField] private float _maxHealthIncrease;

    public string itemName => _itemName;
    public float maxHealthIncrease => _maxHealthIncrease;

    public void Use()
    {
        // Implementar el comportamiento del ítem aquí
        Debug.Log("Usando item de aumento de vida máxima: " + _itemName);
    }
}





