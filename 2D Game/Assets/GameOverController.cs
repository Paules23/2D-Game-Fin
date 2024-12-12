using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class GameOverController : MonoBehaviour
{
    public TMP_Text finalScoreText;

    public void SetUp(int finalScore)
    {
        finalScoreText.text = finalScore.ToString();
    }

    void Start()
    {
        Debug.Log("estas en Start");
    }

}
