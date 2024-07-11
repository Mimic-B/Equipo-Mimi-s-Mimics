using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviour
{
    public LevelProgression levelProgression;
    public CoinManager coinManager;
    public Button[] levelButtons;
    public Text[] scoreTexts;  // Textos para mostrar los puntajes
    public Text coinText;      // Texto para mostrar la cantidad total de monedas

    private void Start()
    {
        // Activar o desactivar botones de nivel según el progreso del jugador y mostrar los puntajes
        int currentLevel = PlayerPrefs.GetInt("CurrentLevel", 1);

        for (int i = 0; i < levelButtons.Length; i++)
        {
            int level = i + 1;
            if (level <= currentLevel)
            {
                levelButtons[i].interactable = true;
                int levelToLoad = level; // Necesario para la captura de variables en lambdas
                levelButtons[i].onClick.AddListener(() => LoadLevel(levelToLoad));
            }
            else
            {
                levelButtons[i].interactable = false;
            }

            // Mostrar el puntaje del nivel
            int score = levelProgression.GetScore(level);
            scoreTexts[i].text = "Score: " + score;
        }

        // Mostrar la cantidad total de monedas
        UpdateCoinText();
    }

    // Método para cargar un nivel específico
    public void LoadLevel(int level)
    {
        levelProgression.LoadLevel(level);
    }

    // Método para actualizar el texto de las monedas
    public void UpdateCoinText()
    {
        int totalCoins = coinManager.GetTotalCoins();
        coinText.text = "Coins: " + totalCoins;
    }
}
