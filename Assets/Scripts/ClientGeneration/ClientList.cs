using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClientList : MonoBehaviour {

    private const int MEDIUM_REP = 30;
    private const int HARD_REP = 75;

    public UnityEngine.Object clientPrefab;

    private ClientGen clientGenerator = new ClientGen();
    private List<GameObject> clientObjs = new List<GameObject>();

    private float timer = 0;

    void Start()
    {
        if (GameData.generatedClients.Count == 0)
        {
            for (int i = 0; i < 3; i++)
            {
                GameData.generatedClients.Add(new GeneratedClient());
            }
        }

        clientObjs.Clear();
        for (int i = 0; i < 3; i++)
        {
            clientObjs.Add(null);
        }

        CreateClient(ClientGen.EASY, true);
        CreateClient(ClientGen.MEDIUM, true);
        CreateClient(ClientGen.HARD, true);
    }

    long CurrentTime()
    {
        return new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
    }

    public GeneratedClient GenClient(int difficulty)
    {
        long timeGen = CurrentTime();
        Client client = clientGenerator.GenClient(difficulty);
        GeneratedClient gen = new GeneratedClient(timeGen, client);

        return gen;
    }

    void CreateClient(int difficulty, bool first)
    {
        GeneratedClient gen = GameData.generatedClients[difficulty];
        Client client = gen.client;

        if (client == null)
        {
            long timeGen = first ? 0 : CurrentTime();
            client = clientGenerator.GenClient(difficulty);
            gen = new GeneratedClient(timeGen, client);

            GameData.generatedClients[difficulty] = gen;
        }

        GameObject clientObj = (GameObject)Instantiate(clientPrefab, Vector3.zero, Quaternion.identity);

        clientObj.transform.SetParent(this.transform.Find("Clients"), false);
        clientObj.transform.SetSiblingIndex(difficulty);

        Text tex = clientObj.transform.Find("Difficulty").GetComponent<Text>();

        switch (difficulty)
        {
            case ClientGen.EASY:
                tex.text = "Easy";
                break;
            case ClientGen.MEDIUM:
                tex.text = "Medium";
                break;
            case ClientGen.HARD:
                tex.text = "Hard";
                break;
        }

        UpdateClient(clientObj, difficulty);

        clientObjs[difficulty] = clientObj;
    }

    void SetReputationTooLow(GameObject clientObj)
    {
        clientObj.transform.Find("Error Reputation Too Low").gameObject.SetActive(true);
        clientObj.transform.Find("Data").gameObject.SetActive(false);
        clientObj.transform.Find("Please Wait").gameObject.SetActive(false);
    }

    void SetTimeLeft(GameObject clientObj, long timeLeft)
    {
        clientObj.transform.Find("Error Reputation Too Low").gameObject.SetActive(false);
        clientObj.transform.Find("Data").gameObject.SetActive(false);
        clientObj.transform.Find("Please Wait").gameObject.SetActive(true);

        Text t = clientObj.transform.Find("Please Wait").GetComponentInChildren<Text>();
        t.text = "We've run out of clients.\n\nPlease wait " + timeLeft + " seconds while we find more.";
    }

    void SetValues(GameObject clientObj, int difficulty, Client client)
    {
        clientObj.transform.Find("Data").gameObject.SetActive(true);
        clientObj.transform.Find("Error Reputation Too Low").gameObject.SetActive(false);
        clientObj.transform.Find("Please Wait").gameObject.SetActive(false);

        Transform trans = clientObj.transform.Find("Data");

        bool green = false;

        foreach (ServerPlacedScript server in GameData.servers)
        {
            if (server.CPULeft() > client.reqPPower &&
                server.RAMLeft() > client.reqRam &&
                server.StorageLeft() > client.reqStorage &&
                server.HasPortsOpen(client.reqPorts))
            {
                green = true;
            }
        }

        if (!green)
        {
            string s = "No servers have ALL the required values.";

            clientObj.transform.Find("Details").gameObject.SetActive(true);
            clientObj.transform.Find("Details").GetComponent<Text>().text = s;
            trans.Find("Accept").GetComponent<Button>().interactable = false;
        }
        else
        {
            clientObj.transform.Find("Details").gameObject.SetActive(false);
        }

        trans.Find("Title").GetComponent<Text>().text = client.reqName;

        SetText(trans, "Type", client.reqType);
        trans.Find("Type").GetChild(0).GetComponent<Text>().color = Settings.NEUTRAL_WARNING;

        SetText(trans, "Processing Power", client.reqPPower + " Ghz");
        RedOrGreenText(trans, "Processing Power", !green);

        SetText(trans, "RAM", client.reqRam + " Gb");
        RedOrGreenText(trans, "RAM", !green);

        SetText(trans, "Storage", client.reqStorage + " Gb");
        RedOrGreenText(trans, "Storage", !green);

        if (client.reqPorts.Length == 0)
            SetText(trans, "Ports", "None");
        else
            SetText(trans, "Ports", string.Join(", ", client.reqPorts));

        RedOrGreenText(trans, "Ports", !green);

        SetText(trans, "Pays", "\u00A3" + client.reqPay); //\u00A3 = £
        trans.Find("Pays").GetChild(0).GetComponent<Text>().color = Settings.NEUTRAL_WARNING;

        trans.Find("Accept").GetComponent<Button>().onClick.AddListener(delegate
        {
            Transform chooseHosting = this.transform.Find("Clients").Find("Choose Hosting");
            chooseHosting.GetComponent<ChooseHosting>().currentClient = client;
            chooseHosting.GetComponent<ChooseHosting>().clientObj = clientObj;
            chooseHosting.GetComponent<ChooseHosting>().clientList = this;
            chooseHosting.gameObject.SetActive(true);
        });

        trans.Find("Decline").GetComponent<Button>().onClick.AddListener(delegate
        {
            GameData.generatedClients[difficulty].client = null;
        });
    }

    void HideDelayScreen(int id)
    {
        this.transform.GetChild(id).Find("Please Wait").gameObject.SetActive(false);
    }

    void SetText(Transform transform, string child, string text)
    {
        transform.Find(child).GetChild(0).GetComponent<Text>().text = text;
    }

    void RedOrGreenText(Transform transform, string child, bool red)
    {
        transform.Find(child).GetChild(0).GetComponent<Text>().color = red ? Settings.RED_WARNING : Settings.GREEN_WARNING;
    }

    void UpdateClient(GameObject clientObj, int difficulty)
    {
        int rep = GameObject.Find("Rep").GetComponent<reputation>().GetRep();
        Client client = GameData.generatedClients[difficulty].client;
        long timeAdded = GameData.generatedClients[difficulty].timeGenerated;

        if ((difficulty == ClientGen.MEDIUM && rep < 50) || (difficulty == ClientGen.HARD && rep < 75))
        {
            SetReputationTooLow(clientObj);
        } else
        {
            long diff = CurrentTime() - timeAdded;
            if (diff < Settings.CLIENT_LOAD_DELAY)
            {
                SetTimeLeft(clientObj, Settings.CLIENT_LOAD_DELAY - diff);
            } else
            {
                SetValues(clientObj, difficulty, client);
            }
        }
    }

    void Update()
    {
        if (Time.time > timer)
        {
            int i = 0;
            timer = Time.time + (float)0.1;
            lock (GameData.generatedClients)
            {
                foreach (GameObject clientObj in clientObjs)
                {
                    if (GameData.generatedClients[i].client == null)
                    {
                        Destroy(clientObj);
                        clientObjs[i] = null;

                        CreateClient(i++, false);
                        break;
                    }
                    else
                    {
                        UpdateClient(clientObj, i++);
                    }
                }
            }
        }
    }
}
