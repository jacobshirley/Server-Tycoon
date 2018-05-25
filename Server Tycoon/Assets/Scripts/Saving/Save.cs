using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save : MonoBehaviour {

	/*
		Clients
		Money/Reputation/Profit-Loss
		Server Placement map
		Server setups
		Player position
		Emails
		Tutorial position (later)
	*/

	public GameObject obj;

	void save(){

		Emails emails;
		Systems systems;
		ClientList clients;

		clientData(obj.GetComponent<Hold>().getClients());
		//moneyData();
		//repData();
		//placementData();
		//setupData();
		//positionData();
		//emailData();
	}

	void clientData(ClientList clients){

	}

}
