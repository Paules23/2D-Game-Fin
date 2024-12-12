using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; 

public class SceneController : MonoBehaviour
{
    public int localPoints = 0; // Puntos acumulados localmente
    private string puntosClave = "PuntosNivel1";  // Clave para PlayerPrefs
    private string bestScoreClave = "BestScore"; // Clave para el mejor puntaje
    public TMP_Text pointsText;
    public GameObject gameOver;
    public GameObject puntuacion;

    public void Start()
    {
        localPoints = 0;
        pointsText.text = localPoints.ToString();
        gameOver.SetActive(false);
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
            // Guardar los puntos actuales en PlayerPrefs
            PlayerPrefs.SetInt(puntosClave, localPoints);
            
            // Comparar con el mejor puntaje y actualizar si es necesario
            int bestScore = PlayerPrefs.GetInt(bestScoreClave, 0); // Valor predeterminado 0
            if (localPoints > bestScore)
            {
                PlayerPrefs.SetInt(bestScoreClave, localPoints);
                Debug.Log($"¡Nuevo mejor puntaje: {localPoints}!");
            }
            else
            {
                Debug.Log($"Puntaje final: {localPoints}. Mejor puntaje actual: {bestScore}");
            }

            PlayerPrefs.Save();
        // Cargar otra escena o reiniciar el nivel (opcional)
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        gameOver.SetActive(true);
        puntuacion.SetActive(false);
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
