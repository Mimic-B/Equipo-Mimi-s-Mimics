using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager instance; // Singleton instance

    private Vector2 checkpointPosition;

    private void Awake()
    {
        // Asegurarse de que solo haya una instancia de CheckpointManager
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Persistir entre escenas
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Establecer la posición del checkpoint
    public void SetCheckpoint(Vector2 newCheckpointPosition)
    {
        checkpointPosition = newCheckpointPosition;
    }

    // Obtener la posición del checkpoint
    public Vector2 GetCheckpointPosition()
    {
        return checkpointPosition;
    }
}
