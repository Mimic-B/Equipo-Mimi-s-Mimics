using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootDrop : MonoBehaviour
{
    public GameObject coinPrefab; // Prefab de la moneda
    public GameObject healthPrefab; // Prefab de la vida
    public float coinDropChance = 0.5f; // Probabilidad de soltar moneda (0.5 = 50%)
    public float healthDropChance = 0.2f; // Probabilidad de soltar vida (0.2 = 20%)
    public int minCoinAmount = 1; // Cantidad m�nima de monedas
    public int maxCoinAmount = 5; // Cantidad m�xima de monedas
    public int minHealthAmount = 1; // Cantidad m�nima de vida
    public int maxHealthAmount = 3; // Cantidad m�xima de vida

    public void DropLoot()
    {
        if (Random.value <= coinDropChance)
        {
            int coinAmount = Random.Range(minCoinAmount, maxCoinAmount + 1);
            for (int i = 0; i < coinAmount; i++)
            {
                Instantiate(coinPrefab, transform.position, Quaternion.identity);
            }
        }

        if (Random.value <= healthDropChance)
        {
            int healthAmount = Random.Range(minHealthAmount, maxHealthAmount + 1);
            for (int i = 0; i < healthAmount; i++)
            {
                Instantiate(healthPrefab, transform.position, Quaternion.identity);
            }
        }
    }
}
