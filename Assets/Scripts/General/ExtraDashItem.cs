using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Extra Dash Item", menuName = "Inventory/Extra Dash Item")]
public class ExtraDashItem : ScriptableObject, IInventoryItem
{
    [SerializeField] private string _itemName;
    [SerializeField] private int extraDashes = 1;

    public string itemName => _itemName;

    public void Use()
    {
        // Este método queda vacío porque el uso real del ítem lo haremos en el inventario al añadir el ítem
    }

    public void Use(PlayerDash playerDash)
    {
        // Incrementar el número de dashes disponibles en el script PlayerDash
        playerDash.IncreaseDashes(extraDashes);
        Debug.Log("Usando item de dash extra: " + _itemName);
    }
}


