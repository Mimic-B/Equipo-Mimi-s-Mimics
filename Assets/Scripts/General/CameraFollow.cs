using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // El objetivo a seguir, generalmente el jugador
    public float smoothSpeed = 0.125f; // Velocidad de suavizado
    public Vector3 offset; // Desplazamiento de la c�mara respecto al objetivo

    public Vector2 minPosition; // Posici�n m�nima de la c�mara
    public Vector2 maxPosition; // Posici�n m�xima de la c�mara

    private void LateUpdate()
    {
        if (target == null)
        {
            Debug.LogWarning("Target is not assigned to CameraFollow script.");
            return;
        }

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Limitar la posici�n de la c�mara a los l�mites especificados
        smoothedPosition.x = Mathf.Clamp(smoothedPosition.x, minPosition.x, maxPosition.x);
        smoothedPosition.y = Mathf.Clamp(smoothedPosition.y, minPosition.y, maxPosition.y);

        transform.position = smoothedPosition;
    }
}

