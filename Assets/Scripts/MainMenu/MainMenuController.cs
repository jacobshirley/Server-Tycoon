using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {

    public GameObject main;
    public GameObject options;
    public TextMeshProUGUI title, optionsTitle;
    public float time = 0.5f;
    public float initTime = 0.1f;
    private bool init = true;
    private string[] titleArray = { "S", "e", "r", "v", "e", "r", " ", "T", "y", "c", "o", "o", "n" };
    private string titleString = "";
    private int titleCount = 0;
    public GameObject confirmation;

    public List<Button> buttons = new List<Button>(4);

	// Use this for initialization
	void Start () {
       buttons.Add(GameObject.Find("NewGameBtn").GetComponent<Button>());
        buttons.Add(GameObject.Find("LoadGameBtn").GetComponent<Button>());
        buttons.Add(GameObject.Find("OptionsBtn").GetComponent<Button>());
        buttons.Add(GameObject.Find("ExitBtn").GetComponent<Button>());

    }

    // Update is called once per frame
    void Update () {
        if (init)
        {

            initTime -= Time.deltaTime;
            if (initTime <= 0)
            {
                initTime = 0.1f;
                titleString += titleArray[titleCount];
                title.text = titleString + "_";
                titleCount++;
            }
            if (title.text.Equals("Server Tycoon_"))
            {
                GameObject.Find("NewGameBtn").GetComponent<Button>().Select();
                init = false;
            }
        }
        else
        {
            time -= Time.deltaTime;
            if (time <= 0)
            {
                time = 0.5f;
                if (title.text.Equals("Server Tycoon"))
                {
                    title.text = "Server Tycoon_";
                }
                else
                {
                    title.text = "Server Tycoon";
                }
            }
        }

	}

    public void NewGame(){
      confirmation.SetActive(true);
      foreach (Button b in buttons)
        {
            b.enabled = false;
        }
    }

    public void LoadGame()
    {
      SceneManager.LoadScene("base");
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

    public void Accept(){
  		File.WriteAllText(Application.persistentDataPath + "/Save.json", "");
        foreach (Button b in buttons)
        {
            b.enabled = true;
        }
        SceneManager.LoadScene("Introduction");
    }

    public void Decline(){
      confirmation.SetActive(false);
        foreach (Button b in buttons)
        {
            b.enabled = true;
        }
    }
}
