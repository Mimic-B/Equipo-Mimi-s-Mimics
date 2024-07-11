using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    private int totalCoins = 0;

    private void Start()
    {
        // Cargar la cantidad de monedas guardada
        totalCoins = PlayerPrefs.GetInt("TotalCoins", 0);
    }

    // Método para añadir monedas
    public void AddCoins(int amount)
    {
        totalCoins += amount;
        PlayerPrefs.SetInt("TotalCoins", totalCoins);
    }

    // Método para gastar monedas
    public bool SpendCoins(int amount)
    {
        if (totalCoins >= amount)
        {
            totalCoins -= amount;
            PlayerPrefs.SetInt("TotalCoins", totalCoins);
            return true;
        }
        else
        {
            Debug.Log("No tienes suficientes monedas");
            return false;
        }
    }

    // Método para obtener la cantidad total de monedas
    public int GetTotalCoins()
    {
        return totalCoins;
    }
}

