using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class Save {

	public GameObject obj;

	public void save(StorageClass s){

		s.position = getPos();
		s.date = getDate();
		s.money = GameObject.Find("Money").GetComponent<economy>().GetMoney();
		s.profit = GameObject.Find("Money").GetComponent<economy>().GetProfit();
		s.rep = GameObject.Find("Rep").GetComponent<reputation>().GetRep();

        s.GeneratedClients = GameData.generatedClients;
        //s.clients = GameData.clients;
        s.servers = new List<ServerData>();

		Debug.Log("Number of servers22 - " + GameData.servers.Count);

        foreach (ServerPlacedScript server in GameData.servers)
        {
            s.servers.Add(server.data);
        }

		string jsonData = JsonUtility.ToJson(s, true);
		File.WriteAllText(Application.persistentDataPath + "/Save.json", "");
		//File.WriteAllText("./Assets/Saves/Save.json", "");
		File.WriteAllText(Application.persistentDataPath + "/Save.json", jsonData);
		//File.WriteAllText("./Assets/Saves/Save.json", jsonData);
	}

	private Vector3 getPos(){
		if(SceneManager.GetActiveScene().name.Equals("game")){
			return GameObject.Find("manBlue_stand").transform.position;
		}
		else{
			return GameData.storage.position;
		}
	}

	private int[] getDate(){
		return GameObject.Find("Time").GetComponent<timeManager>().GetDateArray();
	}

}
