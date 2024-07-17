using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // needed for the image variable
using TMPro;
public class HealthBar : MonoBehaviour
{
    [SerializeField] Health playerHealth;
    [SerializeField] Image totalhealthBar;
    [SerializeField] Image currenthealthBar;
    [SerializeField] TextMeshProUGUI text;

    private void Start()
    {
        UpdateHP();
     totalhealthBar.fillAmount = playerHealth.currentHealth / playerHealth.maxHealth; // totalHealth onlt needs to be updated once, so in Start()
    }

    private void Update()
    {
        UpdateHP();
     currenthealthBar.fillAmount = playerHealth.currentHealth / playerHealth.maxHealth; // keep the currenthealthBar updated
    }

    void UpdateHP()
    {
        text.text = playerHealth.currentHealth + "/" + playerHealth.maxHealth;
    }
}
