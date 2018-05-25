using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ButtonManager : MonoBehaviour {

    public GameObject emailPreview;
    public Button emailPreviewBtn;
    public Text sender, subject, message;
    public GameObject viewPrefab;
    public Transform parent;
    public int childPos;
    public string associatedScenario;
   


    // Use this for initialization
    void Start () {
        emailPreviewBtn.onClick.AddListener(OpenEmail);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetVals(string x, string y, string z, string scenario)
    {
        sender.text = x;
        subject.text = y;
        message.text = z;
        associatedScenario = scenario;
    }

    public void OpenEmail()
    {
        Debug.Log(sender.text + " Clicked");
        emailPreview.transform.GetComponentInParent<Viewer>().mainViewer.transform.GetChild(0).GetComponent<Text>().text = sender.text;
        emailPreview.transform.GetComponentInParent<Viewer>().mainViewer.transform.GetChild(1).GetComponent<Text>().text = subject.text;
        emailPreview.transform.GetComponentInParent<Viewer>().mainViewer.transform.GetChild(2).GetComponent<Text>().text = message.text;
        childPos = emailPreview.transform.GetSiblingIndex();
        emailPreview.transform.GetComponentInParent<Viewer>().ButtonClicked(childPos, associatedScenario);

    }

    public void DestroyButton()
    {
        emailPreview.SetActive(false);
        GameObject.Destroy(emailPreview);
        GameObject.Destroy(emailPreviewBtn);
    }

}
