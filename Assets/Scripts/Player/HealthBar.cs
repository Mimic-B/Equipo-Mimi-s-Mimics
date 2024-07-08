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

    private void Start()
    {
        if (totalhealthBar) totalhealthBar.fillAmount = playerHealth.currentHealth / 100; // totalHealth onlt needs to be updated once, so in Start()
    }

    private void Update()
    {
        if (totalhealthBar) currenthealthBar.fillAmount = playerHealth.currentHealth / 100; // keep the currenthealthBar updated
    }
}
