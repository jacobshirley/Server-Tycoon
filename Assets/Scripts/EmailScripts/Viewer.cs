using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class Viewer : MonoBehaviour {

    public GameObject mainViewer;
    public GameObject previewer;
    public GameObject interactionButtons;
    public GameObject replyButton;

    public Client clientRef;

    public Mail mail;

    private int childPosOfClicked;
    //private string scenario;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
        //This makes it so if the user opens an email, receives another email, then deletes the one they have open, the correct email is deleted
        //This needs to be changed to update the position on a time trigger after that has been implemented.
		if (Input.GetKeyDown(KeyCode.P))
        {
            childPosOfClicked++;
        }
	}

    public void ButtonClicked(int childPos, string newScenario)
    {
        Debug.Log("SUCCESS!!!");
        interactionButtons.SetActive(true);
        childPosOfClicked = childPos;
        replyButton.GetComponent<ReplyButtonmanager>().SetScenario(newScenario);
        replyButton.GetComponent<ReplyButtonmanager>().clientRef = clientRef;
        replyButton.GetComponent<ReplyButtonmanager>().mail = mail;
    }

    public void DeleteClicked()
    {
        Debug.Log(childPosOfClicked + "Deleted");
        mainViewer.transform.GetChild(0).GetComponent<Text>().text = "";
        mainViewer.transform.GetChild(1).GetComponent<Text>().text = "";
        mainViewer.transform.GetChild(2).GetComponent<Text>().text = "";
        interactionButtons.SetActive(false);
        previewer.transform.GetChild(childPosOfClicked).GetComponent<ButtonManager>().DestroyButton();
        GameData.storage.EmailList.RemoveAt(childPosOfClicked);

        clientRef.satisfaction -= 10;
    }
}
