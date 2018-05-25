using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class EmailManager : MonoBehaviour {

    public GameObject prefab;
    public Transform previewPanel;

    public EmailTemplate template;
    EmailTemplates templates = JsonUtility.FromJson<EmailTemplates>(File.ReadAllText("./Assets/EmailTemplates/Emails.json"));

    // Use this for initialization
    void Start () {
        List<Mail> e = GameData.storage.EmailList;

        for(int i = e.Count - 1; i >= 0; i--){
            Mail m = e[i];
            Client cl = GameData.storage.clients.GetClient(m.sender);
            MakeButton(cl, m);
        }
    }

    void OnEnable()
    {
        GameObject.Find("Time").GetComponent<EventManager>().Listen("NewEmail", NewEmail);
    }

    void OnDisable()
    {
        GameObject.Find("Time").GetComponent<EventManager>().Unlisten("NewEmail", NewEmail);
    }

    void NewEmail(System.Object data)
    {
        Debug.Log("got new email");
        SendEmail(GameData.storage.clients.GetClient(((Mail)data).sender));
    }

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.P))
        {
            SendEmail(new ClientGen().GenClient(ClientGen.EASY));
        }
    }

    void MakeButton(Client client, Mail m)
    {
        GameObject newButton = (GameObject)GameObject.Instantiate(prefab);
        newButton.transform.SetParent(previewPanel);
        newButton.transform.SetSiblingIndex(0);
        newButton.transform.localScale = new Vector3(1, 1, 1);
        ButtonManager buttonManager = newButton.GetComponent<ButtonManager>();
        buttonManager.SetVals(client, m);
    }

    public void SendEmail(Client client)
    {
        template = templates.templates[Random.Range(0,templates.templates.Count)];

        Mail m = new Mail();
        m.sender = client.id;
        m.fromName = client.reqName;
        m.subject = template.subject;
        m.body = template.body;
        m.scenario = template.scenario;
        GameData.storage.EmailList.Insert(0, m);

        MakeButton(client, m);
    }
}
