using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Start : MonoBehaviour {

    public Object serverPrefab;

	//This is the loading script, should load all the saved data into GameData.storage
	void Awake(){
    GameData.storage = null;
    GameData.servers = new List<ServerPlacedScript>();
    GameData.generatedClients = new List<GeneratedClient>();
        //GameData.clients = new List<Client>();
        GameData.storage = JsonUtility.FromJson<StorageClass>(File.ReadAllText(Application.persistentDataPath + "/Save.json"));
    Debug.Log(GameData.storage);
		//If the GameData is nothing, ie. it hasn't been used before, then I'll need to instantiate variables
		if(GameData.storage == null){
			GameData.storage = new StorageClass();
			GameData.storage.position = new Vector3(0,0,0);
			GameData.storage.EmailList = new List<Mail>();
			GameData.storage.money = 1000f; //Probably shouldn't be set to 0 at the start, but we can change it
			GameData.storage.profit = 0f;
			GameData.storage.rep = 20;
			GameData.storage.GeneratedClients = new List<GeneratedClient>();

            GameData.storage.tutManager = new TutorialManager();
            GameData.storage.clients = new ClientManager();
            GameData.storage.servers = new List<ServerData>();

      GameData.storage.date = new int[3];
      GameData.storage.date[0] = 1;
      GameData.storage.date[1] = 1;
      GameData.storage.date[2] = 2000;

      GameData.storage.fish = new List<string>();
      GameData.storage.unlockedFish = new List<string>();

      GameData.storage.cheats = false;

			Debug.Log("Created data");

		}
        GameData.generatedClients = GameData.storage.GeneratedClients;

		GameObject.Find("manBlue_stand").transform.position = GameData.storage.position;
		GameData.move = true;
		GameObject mc = GameObject.Find("Main Camera");
		mc.transform.position = GameData.storage.position;
		mc.transform.position = new Vector3(mc.transform.position.x, mc.transform.position.y, -10);
    GameObject.Find("Money").GetComponent<economy>().SetMoney(GameData.storage.money);
		GameObject.Find("Money").GetComponent<economy>().SetProfit(GameData.storage.profit);
		GameObject.Find("Rep").GetComponent<reputation>().SetRep(GameData.storage.rep);
		GameObject.Find("Time").GetComponent<timeManager>().SetDate(GameData.storage.date);

        AtlasLoader loader = new AtlasLoader("Server Prefabs/Server Rack");
        foreach (ServerData server in GameData.storage.servers)
    {
        GameObject serverObj = (GameObject)Instantiate(serverPrefab);
        serverObj.transform.position = server.position;
            serverObj.GetComponent<SpriteRenderer>().sprite = loader.getAtlas("Server Rack_" + server.def.id);
        serverObj.GetComponent<ServerPlacedScript>().data = server;

        GameData.servers.Add(serverObj.GetComponent<ServerPlacedScript>());
    }
    GameData.menuOpen = false;
    new Save().save(GameData.storage);
		Debug.Log("Loaded");
	}
}
