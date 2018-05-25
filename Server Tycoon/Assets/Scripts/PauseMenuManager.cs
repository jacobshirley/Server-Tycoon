using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuManager : MonoBehaviour {
    public Canvas menu;
    public Canvas options;
    public GameObject player;

    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ResumeGame();
        }
    }
    public void ResumeGame()
    {
        menu.gameObject.SetActive(false);
        player.SetActive(true);
    }

    public void Options()
    {
        menu.gameObject.SetActive(false);
        options.gameObject.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
