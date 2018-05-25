using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReplyButtonmanager : MonoBehaviour {
    public Client clientRef;
    public Mail mail;

    private string scenario;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

    public void SetScenario(string newScenario)
    {
        scenario = newScenario;
    }

    public void Clicked()
    {
        GameData.ScenarioClient = clientRef;
        GameData.ScenarioEmail = mail;

        Debug.Log("reply clicked");
        GameObject.Find("Time").GetComponent<timeManager>().enabled = false;
        //GameObject.Find("Content").GetComponent<Viewer>().DeleteClicked();
        switch (scenario)
        {
            case "maze":
                Debug.Log("Maze");
                SceneManager.LoadScene("maze");
                break;
            case "bin":
                SceneManager.LoadScene("Binary Game");
                Debug.Log("Bin");
                break;
            case "code":
                SceneManager.LoadScene("Programming Scene");
                Debug.Log("Code");
                break;
            case "logic":
                Debug.Log("Logic");
                SceneManager.LoadScene("Logic Gate Scenario");
                break;
            default:
                Debug.Log("Else");
                break;
        }
    }
}
