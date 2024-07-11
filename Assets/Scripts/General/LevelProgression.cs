using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelProgression : MonoBehaviour
{
    public int currentLevel = 1;  // Nivel actual del jugador
    private int maxLevel = 5;     // Número máximo de niveles en el juego (ajústalo según necesites)
    public int currentScore = 0;  // Puntaje actual del nivel

    private void Start()
    {
        // Cargar el nivel y el puntaje guardado si existen
        currentLevel = PlayerPrefs.GetInt("CurrentLevel", 1);
        currentScore = PlayerPrefs.GetInt("ScoreLevel" + currentLevel, 0);
    }

    // Método para avanzar al siguiente nivel y guardar el puntaje
    public void CompleteLevel(int score)
    {
        // Guardar el puntaje del nivel completado
        PlayerPrefs.SetInt("ScoreLevel" + currentLevel, score);

        if (currentLevel < maxLevel)
        {
            currentLevel++;
            currentScore = 0;  // Reiniciar el puntaje para el nuevo nivel
            PlayerPrefs.SetInt("CurrentLevel", currentLevel);
        }
        else
        {
            Debug.Log("¡Has completado todos los niveles!");
        }
    }

    // Método para cargar el lobby
    public void LoadLobby()
    {
        SceneManager.LoadScene("LobbyScene");
    }

    // Método para cargar un nivel específico
    public void LoadLevel(int level)
    {
        if (level <= currentLevel)
        {
            SceneManager.LoadScene("Level" + level);
            currentScore = PlayerPrefs.GetInt("ScoreLevel" + level, 0);
        }
        else
        {
            Debug.Log("¡Debes completar el nivel anterior antes de acceder a este nivel!");
        }
    }

    // Método para actualizar el puntaje del nivel actual
    public void UpdateScore(int score)
    {
        currentScore += score;
        PlayerPrefs.SetInt("ScoreLevel" + currentLevel, currentScore);
    }

    // Método para obtener el puntaje de un nivel específico
    public int GetScore(int level)
    {
        return PlayerPrefs.GetInt("ScoreLevel" + level, 0);
    }
}

