using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class ItemaumentoHP : ScriptableObject
{
    public string itemName; // Nombre del �tem
    public Sprite icon; // Icono del �tem
    public int value; // Valor del �tem (por ejemplo, cantidad de salud que restaura)
    public float maxhpincrease;
}
