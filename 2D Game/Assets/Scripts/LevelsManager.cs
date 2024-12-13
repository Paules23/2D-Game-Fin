using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; 

public class LevelsManager : MonoBehaviour
{
    public TMP_Text[] bestScores;

    public void Level1()
    {
        SceneManager.LoadScene(2);
    }
    public void Level2()
    {
        SceneManager.LoadScene(3);
    }
    public void Level3()
    {
        SceneManager.LoadScene(4);
    }

    public void SetScores()
    {
        // Leer los mejores puntajes desde PlayerPrefs
        int bestScore1 = PlayerPrefs.GetInt("BestScore1", 0);
        int bestScore2 = PlayerPrefs.GetInt("BestScore2", 0);
        int bestScore3 = PlayerPrefs.GetInt("BestScore3", 0);

        // Asignar los valores a los textos correspondientes
        if (bestScores.Length > 0) bestScores[0].text = bestScore1.ToString();
        if (bestScores.Length > 1) bestScores[1].text = bestScore2.ToString();
        if (bestScores.Length > 2) bestScores[2].text = bestScore3.ToString();
    }

    public void Start()
    {
        SetScores();
    }

    public void Return()
    {
        SceneManager.LoadScene(0);
    }
}
