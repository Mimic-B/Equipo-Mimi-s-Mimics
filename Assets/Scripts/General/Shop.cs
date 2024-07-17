using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public Inventory playerInventory; // Referencia al inventario del jugador
    public GameObject shopUI; // UI de la tienda
    public Button[] itemButtons; // Botones para los ítems de la tienda
    public IInventoryItem[] itemsForSale; // Ítems que se pueden comprar en la tienda
    public int[] itemPrices; // Precios de los ítems

    private void Start()
    {
        for (int i = 0; i < itemButtons.Length; i++)
        {
            int index = i;
            itemButtons[i].onClick.AddListener(() => BuyItem(index));
        }
    }

    private void BuyItem(int index)
    {
        if (playerInventory.coins >= itemPrices[index])
        {
            playerInventory.coins -= itemPrices[index];
            playerInventory.AddItem(itemsForSale[index]);
            Debug.Log("Item comprado: " + itemsForSale[index].itemName);
        }
        else
        {
            Debug.Log("No tienes suficientes monedas");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            shopUI.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            shopUI.SetActive(false);
        }
    }
}

