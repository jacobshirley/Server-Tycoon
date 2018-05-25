using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ClientItemController : MonoBehaviour {

    public Client client;

    public Text nameField;
    public ProgressBar progressBar;
    public GameObject mailIcon;

	void Start () {
        nameField.text = client.reqName;
    }

    void OnEnable()
    {
        GameObject.Find("Time").GetComponent<EventManager>().Listen("RemoveClient", ClientRemoved);
    }

    void OnDisable()
    {
        GameObject.Find("Time").GetComponent<EventManager>().Unlisten("RemoveClient", ClientRemoved);
    }

    void ClientRemoved(System.Object client)
    {
        Client c = (Client)client;
        if (c == this.client)
        {
            GameData.storage.clients.RemoveClient(c);

            foreach (ServerPlacedScript server in GameData.servers)
            {
                server.data.clients.Remove(c.id);
            }

            GameData.storage.EmailList.RemoveAll((Mail m) => m.sender == c.id); //delete all emails to do with client

            this.client = null;
        }
    }

	void Update () {
        if (client == null)
        {
            Destroy(this.gameObject);
            return;
        }

        if (client.satisfaction < 5)
        {
            GameObject.Find("Time").GetComponent<EventManager>().Trigger("RemoveClient", client);
            SceneManager.LoadScene("ClientLeft", LoadSceneMode.Additive);

            return;
        }

        progressBar.value = client.satisfaction;

        progressBar.progressColour = Settings.GREEN_WARNING;

        if (client.satisfaction < 30)
        {
            progressBar.progressColour = Settings.ORANGE_WARNING;
        }

        if (client.satisfaction < 10)
        {
            progressBar.progressColour = Settings.RED_WARNING;
        }

        mailIcon.SetActive(client.hasMail);
    }
}
