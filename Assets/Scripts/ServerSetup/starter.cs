using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class starter : MonoBehaviour {

	public Client client;
	public string location;

	void Start(){
		loadClient();
	}

	void loadClient(){
		client = JsonUtility.FromJson<Client>(File.ReadAllText("./gameSettings.json"));

	}

	void exit(){
		saveClient();
		
	}

	void saveClient(){

	}
}
