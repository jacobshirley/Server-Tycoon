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
    public Client clientRef;
    public Mail mail;
    public int childPos;
    public string associatedScenario;
    
    void Start () {
        emailPreviewBtn.onClick.AddListener(OpenEmail);
  	}

    public void SetVals(Client client, Mail m)
    {
        this.sender.text = m.fromName;
        this.subject.text = m.subject;
        this.message.text = m.body;
        clientRef = client;
        mail = m;
        associatedScenario = m.scenario;
    }

    public void OpenEmail()
    {
        Debug.Log(sender.text + " Clicked");
        emailPreview.transform.GetComponentInParent<Viewer>().mainViewer.SetActive(true);
        emailPreview.transform.GetComponentInParent<Viewer>().mainViewer.transform.GetChild(0).GetComponent<Text>().text = sender.text;
        emailPreview.transform.GetComponentInParent<Viewer>().mainViewer.transform.GetChild(1).GetComponent<Text>().text = subject.text;
        emailPreview.transform.GetComponentInParent<Viewer>().mainViewer.transform.GetChild(2).GetComponent<Text>().text = message.text;
        childPos = emailPreview.transform.GetSiblingIndex();
        emailPreview.transform.GetComponentInParent<Viewer>().clientRef = clientRef;
        emailPreview.transform.GetComponentInParent<Viewer>().mail = mail;
        emailPreview.transform.GetComponentInParent<Viewer>().ButtonClicked(childPos, associatedScenario);

    }

    public void DestroyButton()
    {
        emailPreview.SetActive(false);
        GameObject.Destroy(emailPreview);
        GameObject.Destroy(emailPreviewBtn);
    }

}
