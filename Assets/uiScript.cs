using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class uiScript : MonoBehaviour
{

    public GameObject howToPlayMenu;
    public GameObject normalMenu;

    private void Start()
    {
        UnloadHowToPlay();
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        SceneManager.LoadScene(2);
    }

    public void LoadHowToPlay()
    {
        howToPlayMenu.SetActive(true);
        normalMenu.SetActive(false);
    }

    public void UnloadHowToPlay()
    {
        howToPlayMenu.SetActive(false);
        normalMenu.SetActive(true);
    }



}
