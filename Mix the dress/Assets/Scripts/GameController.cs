using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static bool gameover;
    public static bool Won;
    public GameObject WinPanel;
    public GameObject LosePanel;
    void Start()
    {
        gameover = false;
        Won = false;
    }

    void Update()
    {
        if (gameover)
        {
            //open lose panel
            Time.timeScale = 0;
            LosePanel.SetActive(true);
        }
        if (Won)
        {
            //open lose panel
            Time.timeScale = 0;
            WinPanel.SetActive(true);
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene("MainScene");
    }

}
