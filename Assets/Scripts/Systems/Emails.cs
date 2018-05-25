using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.EventSystems;

public class Emails: MonoBehaviour {

    public EmailTemplate template;
    EmailTemplates templates;

    void Awake(){
        templates = JsonUtility.FromJson<EmailTemplates>(File.ReadAllText("./Assets/EmailTemplates/Emails.json"));
        DontDestroyOnLoad(this.gameObject);
	}

    public void SendEmail(string name, Client client)
    {
        template = templates.templates[Random.Range(0, templates.templates.Count)];

        Mail m = new Mail();
        m.sender = client.id;
        m.fromName = name;
        m.subject = template.subject;
        m.body = template.body;
        m.scenario = template.scenario;

        GameData.storage.EmailList.Insert(0, m);

        GetComponent<EventManager>().Trigger("NewEmail", m);
        Debug.Log("Sent mail - " + client);
    }

    public void SendEmail(Client client)
    {
        SendEmail(client.reqName, client);
    }

    void Update()
    {
        foreach (Client client in GameData.storage.clients.GetClients())
        {
            client.hasMail = false;
            foreach (Mail m in GameData.storage.EmailList)
            {
                if (m.sender == client.id)
                {
                    client.hasMail = true;
                    break;
                }
            }
        }
    }
}
