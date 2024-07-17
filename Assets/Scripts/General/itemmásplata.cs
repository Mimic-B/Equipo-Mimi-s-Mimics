using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class itemm�splata : ScriptableObject
{
    public string itemName; // Nombre del �tem
    public Sprite icon; // Icono del �tem
    public int value; // Valor del �tem (por ejemplo, cantidad de salud que restaura)
}

