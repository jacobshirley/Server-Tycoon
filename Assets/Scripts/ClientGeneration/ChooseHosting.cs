
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChooseHosting : MonoBehaviour {
    public Object serverItemPrefab;
    public Transform list;
    public Client currentClient;
    public GameObject clientObj;

    public ClientList clientList;

    ButtonToggleList serverList;

    void Start () {
        Transform scrollContent = this.transform.Find("Content");

        serverList = this.transform.Find("Server List").GetComponent<ButtonToggleList>();

        int i = 0;
		foreach (ServerPlacedScript server in GameData.servers)
        {
            if (server.CPULeft() > currentClient.reqPPower &&
                server.RAMLeft() > currentClient.reqRam &&
                server.StorageLeft() > currentClient.reqStorage &&
                server.HasPortsOpen(currentClient.reqPorts))
            {
                GameObject serverItem = (GameObject)Instantiate(serverItemPrefab);
                serverItem.GetComponentInChildren<Text>().text = server.data.serverName;
                serverItem.GetComponent<ServerItem>().server = server;
                serverItem.transform.SetParent(list, false);
            }

            i++;
        }

        this.transform.Find("OK").GetComponent<Button>().onClick.AddListener(delegate
        {
            if (serverList.selected == null)
            {
                this.transform.Find("Error Select Server").gameObject.SetActive(true);
            } else
            {
                currentClient.id = GameData.storage.clients.AddClient(currentClient);
                serverList.selected.GetComponent<ServerItem>().server.data.clients.Add(currentClient.id);

                GameObject.Find("Time").GetComponent<EventManager>().Trigger("NewClient", currentClient);

                GameData.generatedClients[currentClient.difficulty].client = clientList.GenClient(currentClient.difficulty).client;
            }
        });
	}
	
	void Update () {
		if (serverList.selected == null)
        {
            this.transform.Find("OK").GetComponent<Button>().interactable = false;
        } else
        {
            this.transform.Find("OK").GetComponent<Button>().interactable = true;
        }
	}
}
