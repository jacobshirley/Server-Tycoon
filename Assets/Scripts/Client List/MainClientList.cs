using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainClientList : MonoBehaviour {
    public Object prefab;

	void Start () {
		foreach (Client client in GameData.storage.clients.GetClients())
        {
            AddClient(client);
        }
    }

    void NewClient(System.Object client)
    {
        Client cl = (Client)client;
        AddClient(cl);
    }

    void RemoveClient(System.Object client)
    {
        Client cl = (Client)client;
        RemoveClient(cl);
    }

    void OnEnable()
    {
        GameObject.Find("Time").GetComponent<EventManager>().Listen("NewClient", NewClient);
    }

    void OnDisable()
    {
        if (GameObject.Find("Time") != null)
            GameObject.Find("Time").GetComponent<EventManager>().Unlisten("NewClient", NewClient);
    }

    void AddClient(Client client)
    {
        GameObject item = (GameObject)Instantiate(prefab);

        item.GetComponent<ClientItemController>().client = client;
        item.transform.SetParent(this.transform, false);
    }

    void RemoveClient(Client client)
    {
        foreach (ClientItemController child in this.transform.GetComponentsInChildren<ClientItemController>())
        {
            if (child.client == client)
            {
                Destroy(child.gameObject);
                break;
            }
        }
    }

    void Update () {
		
	}
}
