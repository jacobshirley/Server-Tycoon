using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplyButtonmanager : MonoBehaviour {

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
        Debug.Log("reply clicked");
        switch (scenario)
        {
            case "maze":
                Debug.Log("Maze");
                break;
            case "bin":
                Debug.Log("Bin");
                break;
            case "code":
                Debug.Log("Code");
                break;
            default:
                Debug.Log("Else");
                break;
        }
    }

    public void Logout()
    {
        Debug.Log("Returning to main view");
        //This button returns to the office view
    }
}
