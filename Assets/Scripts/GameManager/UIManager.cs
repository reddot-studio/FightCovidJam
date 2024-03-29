﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    public GameObject endCanvas;
    public TextMeshProUGUI finalScore;
    public TextMeshProUGUI bestScore;

    int maxScore;


    public void LoadEndScene()
    {
        if(endCanvas)
            endCanvas.SetActive(true);

        finalScore.text = "Has conseguido " + GameManager.instance.score + " puntos.";
        maxScore = PlayerPrefs.GetInt("maxScore", 0);
        if(maxScore < GameManager.instance.score)
        {
            maxScore = GameManager.instance.score;
            PlayerPrefs.SetInt("maxScore", maxScore);
        }
        bestScore.text = "Mejor puntuación: " + maxScore;
        //GameManager.instance.gameObject.SetActive(false);
    }

    public void CloseGame()
    {
        Application.Quit();
    }

    public static void RestartGame(int i)
    {
        SceneManager.LoadScene(i);
        Debug.Log("Restaring game...");
    }

    // Start is called before the first frame update
    void Awake()
    {
        endCanvas.SetActive(false);
        GameManager.onLose += LoadEndScene;
    }

    public void RestartWithButton(int index)
    {
        RestartGame(index);
        if (endCanvas)
            endCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
