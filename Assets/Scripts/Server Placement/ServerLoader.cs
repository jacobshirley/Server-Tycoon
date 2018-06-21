using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ServerLoader : MonoBehaviour {

    [Serializable]
    private class ServerList
    {
        public ServerDef[] servers;
    }

    private ServerList serverList;
    public UnityEngine.Object serverListItem;
    public Transform content;
    public NewServerPlacement serverPlacement;
    private List<GameObject> serverItems = new List<GameObject>();
    private List<ServerDef> defs = new List<ServerDef>();
    private AtlasLoader loader;

    private static Color ENABLED_COLOUR = new Color(170f / 255f, 1f, 171f / 255f, 225f / 255f);
    private static Color DISABLED_COLOUR = Color.red;

    void Awake()
    {
        loader = new AtlasLoader("Server Prefabs/Server Rack");
    }

    // Use this for initialization
    void Start () {
        serverList = JsonUtility.FromJson<ServerList>(Resources.Load<TextAsset>("JSON/server-definitions").text);
        foreach (ServerDef def in serverList.servers)
        {
            GameObject server = (GameObject)Instantiate(serverListItem);

            server.transform.SetParent(content, false);

            server.transform.Find("Name").GetComponent<Text>().text = def.signature;
            server.transform.Find("CPU").GetComponent<Text>().text = "CPU: " + def.cpu + "GHz";
            server.transform.Find("RAM").GetComponent<Text>().text = "RAM: " + def.ram + "Gb";
            server.transform.Find("Storage").GetComponent<Text>().text = "Storage: " + def.capacity + "GB";
            server.transform.Find("Price").GetComponent<Text>().text = "£" + def.cost;

            server.GetComponent<Button>().onClick.AddListener(delegate
            {
                serverPlacement.StartServerPlacement(loader.getAtlas("Server Rack_" + def.id), def);
            });

            serverItems.Add(server);
            defs.Add(def);
        }
    }

	// Update is called once per frame
	void Update () {
        float money = GameObject.Find("Money").GetComponent<economy>().GetMoney();

        int i = 0;
        foreach (GameObject serverItem in serverItems)
        {
            ServerDef def = defs[i];
            if (money >= def.cost)
            {
                serverItem.GetComponent<Image>().color = ENABLED_COLOUR;
                serverItem.GetComponent<Button>().interactable = true;
            }
            else
            {
                serverItem.GetComponent<Image>().color = DISABLED_COLOUR;
                serverItem.GetComponent<Button>().interactable = false;
            }

            i++;
        }
    }
}
