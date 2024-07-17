using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class ItemaumentoHP : ScriptableObject
{
    public string itemName; // Nombre del ítem
    public Sprite icon; // Icono del ítem
    public int value; // Valor del ítem (por ejemplo, cantidad de salud que restaura)
    public float maxhpincrease;
}

