using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClientServerController : MonoBehaviour {

    public UnityEngine.Object clientPrefab;
    public Transform list;

    private Transform clientObj;
    private ButtonToggleList toggleList;
    private Client client;

    public RemoveClient removeClientWindow;

    private Dictionary<Client, GameObject> clientList = new Dictionary<Client, GameObject>();

    // Use this for initialization
    void Start () {
        ServerPlacedScript server = GameData.CurrentServer;

        clientObj = this.transform.Find("Client Info");
        toggleList = this.transform.Find("Clients List").Find("List").GetComponent<ButtonToggleList>();

        clientObj.Find("Remove Client").GetComponent<Button>().onClick.AddListener(delegate
        {
            if (client != null)
            {
                removeClientWindow.gameObject.SetActive(true);
                removeClientWindow.client = client;
            }
        });

        int i = 0;
        foreach (int clientId in server.data.clients)
        {
            Client client = GameData.storage.clients.GetClient(clientId);
            GameObject clientItem = (GameObject)Instantiate(clientPrefab);

            clientItem.GetComponentInChildren<Text>().text = client.reqName;
            clientItem.transform.SetParent(list, false);

            clientItem.GetComponent<Button>().onClick.AddListener(delegate
            {
                this.client = client;
            });

            clientList.Add(client, clientItem);

            i++;
        }
    }

    public void OnEnable()
    {
        GameObject.Find("Time").GetComponent<EventManager>().Listen("RemoveClient", ClientRemoved);
    }

    public void OnDisable()
    {
        GameObject.Find("Time").GetComponent<EventManager>().Unlisten("RemoveClient", ClientRemoved);
    }

    void ClientRemoved(System.Object client)
    {
        GameObject o;
        if (clientList.TryGetValue((Client)client, out o))
        {
            clientList.Remove((Client)client);
            Destroy(o);
        }
    }

    private void SetText(Transform clientObj, string child, string data)
    {
        clientObj.Find(child).GetChild(0).GetComponent<Text>().text = data;
    }

    private void SetColourWarning(Transform parent, string child, double value, Predicate<double> medium, Predicate<double> high)
    {
        Color colour = new Color();
        if (high.Invoke(value))
        {
            colour = Settings.RED_WARNING; //red
        }
        else if (medium.Invoke(value))
        {
            colour = Settings.ORANGE_WARNING; //orange
        }
        else
        {
            colour = Settings.GREEN_WARNING; //green
        }

        parent.Find(child).GetChild(0).GetComponent<Text>().color = colour;
    }

    // Update is called once per frame
    void Update () {
        if (client == null)
            return;

        if (toggleList.selected == null)
        {
            clientObj.gameObject.SetActive(false);
            return;
        } else
        {
            clientObj.gameObject.SetActive(true);
        }

        ServerPlacedScript server = GameData.CurrentServer;
        ServerDef def = server.data.def;

        SetText(clientObj, "Ports Open", string.Join(", ", client.reqPorts));
        clientObj.Find("Ports Open").GetChild(0).GetComponent<Text>().color = Settings.NEUTRAL_WARNING;

        double perc = client.clientCPU / server.data.overclockedCPU;
        SetText(clientObj, "CPU Utilisation", client.clientCPU.ToString("N1") + " Ghz (" + (perc * 100).ToString("N1") + "%)");
        SetColourWarning(clientObj, "CPU Utilisation", perc, d => d >= 0.60, d => d >= 0.90);

        perc = client.clientRAM / def.ram;
        SetText(clientObj, "RAM", client.clientRAM.ToString("N1") + " Gb (" + (perc * 100).ToString("N1") + "%)");
        SetColourWarning(clientObj, "RAM", perc, d => d >= 0.60, d => d >= 0.90);

        perc = client.clientStorage / def.capacity;
        SetText(clientObj, "Storage", client.clientStorage + " Gb (" + (perc * 100).ToString("N1") + "%)");
        SetColourWarning(clientObj, "Storage", perc, d => d >= 0.60, d => d >= 0.90);
    }
}
