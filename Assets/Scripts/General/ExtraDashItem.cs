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
        // Este m�todo queda vac�o porque el uso real del �tem lo haremos en el inventario al a�adir el �tem
    }

    public void Use(PlayerDash playerDash)
    {
        // Incrementar el n�mero de dashes disponibles en el script PlayerDash
        playerDash.IncreaseDashes(extraDashes);
        Debug.Log("Usando item de dash extra: " + _itemName);
    }
}


