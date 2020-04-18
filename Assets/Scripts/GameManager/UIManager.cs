using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{

    public GameObject endCanvas;
    TextMeshProUGUI scoreText;




    public void LoadEndScene()
    {
        endCanvas.SetActive(true);
        GameManager.instance.gameObject.SetActive(false);
    }

    public void CloseGame()
    {
        Application.Quit();
    }

    public void RestartGame()
    {
        Debug.Log("Restaring game...");
    }

    // Start is called before the first frame update
    void Start()
    {
        endCanvas.SetActive(false);
        GameManager.onLose += LoadEndScene;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
