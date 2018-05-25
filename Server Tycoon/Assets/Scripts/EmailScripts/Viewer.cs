using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Viewer : MonoBehaviour {

    public GameObject mainViewer;
    public GameObject previewer;
    public GameObject interactionButtons;
    public GameObject replyButton;

    private int childPosOfClicked;
    //private string scenario;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ButtonClicked(int childPos, string newScenario)
    {
        Debug.Log("SUCCESS!!!");
        interactionButtons.SetActive(true);
        childPosOfClicked = childPos;
        //scenario = newScenario;
        replyButton.GetComponent<ReplyButtonmanager>().SetScenario(newScenario);
    }

    public void DeleteClicked()
    {
        //GameObject.Destroy(
        Debug.Log(childPosOfClicked + "Deleted");
        mainViewer.transform.GetChild(0).GetComponent<Text>().text = "";
        mainViewer.transform.GetChild(1).GetComponent<Text>().text = "";
        mainViewer.transform.GetChild(2).GetComponent<Text>().text = "";
        interactionButtons.SetActive(false);
        previewer.transform.GetChild(childPosOfClicked).GetComponent<ButtonManager>().DestroyButton();
        
    }
}
