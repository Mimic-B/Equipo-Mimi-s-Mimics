using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enterorexitarea : MonoBehaviour
{
   [SerializeField] UnityEvent Enter;
   [SerializeField] UnityEvent Exit;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            Enter.Invoke();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            Exit.Invoke();
        }
    }
}
