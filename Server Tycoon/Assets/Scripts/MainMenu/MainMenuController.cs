using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {

    public GameObject main;
    public GameObject options;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void NewGame()
    {
        SceneManager.LoadScene("game");
    }

    public void LoadGame()
    {

    }

    public void Options()
    {
        options.SetActive(true);
        main.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
