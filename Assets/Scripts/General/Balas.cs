using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balas : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag ("Player"))
        {
            Destroy(gameObject);
        }
        else if (collision.transform.CompareTag ("Estructura"))
        {
            Destroy(gameObject);
        }
    }
}
