using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform target;
    public float speed = 5f;

    void Update()
    {
        if (target != null)
        {
            Vector3 direction = target.position - transform.position;

            direction.Normalize();

            transform.position += direction * speed * Time.deltaTime;
        }
    }
}
