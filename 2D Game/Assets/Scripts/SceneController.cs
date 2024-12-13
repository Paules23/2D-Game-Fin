using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; 

public class SceneController : MonoBehaviour
{
    public int localPoints = 0; // Puntos acumulados localmente
    private string puntosClave = "PuntosNivel1";  // Clave para PlayerPrefs
    private string bestScoreClave1 = "BestScore1"; // Clave para el mejor puntaje
    private string bestScoreClave2 = "BestScore2"; // Clave para el mejor puntaje
    private string bestScoreClave3= "BestScore3"; // Clave para el mejor puntaje

    public TMP_Text pointsText;
    public GameObject gameOverWin;
    public GameObject gameOverLost;
    public GameObject puntuacion;

    public void Start()
    {
        localPoints = 0;
        pointsText.text = localPoints.ToString();
        gameOverWin.SetActive(false);
        gameOverLost.SetActive(false);
        puntuacion.SetActive(true);
    }

    public void AddPoint()
    {
        localPoints += 1;
        Debug.Log($"Puntos actuales: {localPoints}");
        pointsText.text = localPoints.ToString();
    }

    public void EndGame()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        string bestScoreClave;

        switch (sceneIndex)
        {
            case 2:
                bestScoreClave = bestScoreClave1;
                break;
            case 3:
                bestScoreClave = bestScoreClave2;
                break;
            case 4:
                bestScoreClave = bestScoreClave3;
                break;
            default:
                bestScoreClave = bestScoreClave1; //por defecto
                break;
        }

        // Comparar con el mejor puntaje y actualizar si es necesario
        int bestScore = PlayerPrefs.GetInt(bestScoreClave, 0); // Valor predeterminado 0
        if (localPoints > bestScore)
        {
            PlayerPrefs.SetInt(bestScoreClave, localPoints);
            Debug.Log($"¡New score scene {sceneIndex}: {localPoints}!");
        }
        else
        {
            Debug.Log($"Puntaje final para la escena {sceneIndex}: {localPoints}. Mejor puntaje actual: {bestScore}");
        }

        PlayerPrefs.Save();

        // Mostrar pantalla de game over
        gameOverWin.GetComponent<GameOverController>().SetUp(localPoints);
        gameOverWin.SetActive(true);
        puntuacion.SetActive(false);
    }

    public void Lost()
    {
        Debug.Log("estás en LOST");
        gameOverLost.SetActive(true);
        //pongo un pequenno retraso pq si no se activa todo, prob no sea la forma mas eficiente pero tengo suenno
        puntuacion.SetActive(false);
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void HomeScene()
    {
        SceneManager.LoadScene(0);
    }

    public void Return()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Debug.Log("Saliendo de la aplicación...");
        Application.Quit();
    }

}
