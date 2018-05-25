using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class clientGen : MonoBehaviour {

	//Left these as generic names, these could be specialised, but that would require specialising storage, ping, etc
	//Could also make different levels of difficulty to host the client based on reputation (proabably a good idea)..
	//This could easily be done by copying+pasting the function and just making a few new functions with different ranges

	public Text nameT;
	public Text typeT;
	public Text ppT;
	public Text portsT;
	public Text pingT;
	public Text storageT;
	public Text payT;

	string[] types = {"game", "business", "personal", "medical", "booking"};
	string[] names = {"C1", "C2", "C3", "C4", "C5"}; //Remember to randomly generate these names
	int[] ports = {20, 80, 25, 10};
	int[] storage = {50, 100, 250, 500, 1000, 2000, 5000, 10000};

	void Start(){
		Client temp = genClient();
		nameT.text = temp.reqName;
		typeT.text = "Type - " + temp.reqType;
		ppT.text = "Processing Power - " + temp.reqPPower.ToString();
		portsT.text = "Insert ports";
		pingT.text = "Ping - " + temp.reqPing.ToString();
		storageT.text = "Storage - " + temp.reqStorage.ToString();
		payT.text = "£" + temp.reqPay;
	}

	public Client genClient(){
 		string reqType = types[Random.Range(0,types.Length)];
 		string reqName = names[Random.Range(0,names.Length)];
 		int size = Random.Range(0,ports.Length);
 		int[] reqPorts = new int[size];
 		List<int> list = new List<int>(ports);
 		for(int i = 0; i < size; i++){
 			int a = Random.Range(0,list.Count);
 			reqPorts[i] = list[a];
 			list.RemoveAt(a);
 		}
 		int reqPing = Random.Range(10, 99);
		float reqPPower = 2.13f;

		int reqStorage = storage[Random.Range(0, storage.Length)];
		string reqPay = getCost(reqType, reqName, reqPPower, reqPorts, reqPing, reqStorage);

		Client client = new Client();

		client.reqType = reqType;
		client.reqName = reqName;
		client.reqPPower = reqPPower;
		client.reqPorts = reqPorts;
		client.reqPing = reqPing;
		client.reqStorage = reqStorage;

		client.reqPay = reqPay;

		return client;
 	}

	private string getCost(string type, string name, float power, int[] ports, int ping, int storage){
		float cost = 50;

		//Every ms below 40 costs 50p
		if(ping<40){cost += (ping-40)*0.5f;}
		//Every .25 Ghz Power costs £25
		cost += (power/4)*25;
		//Every 50GB storage costs £1
		cost += storage/50;
		//Every open port costs £10 - Extra security + setup
		cost += ports.Length * 10;

		return cost.ToString("N2");
	}
}
