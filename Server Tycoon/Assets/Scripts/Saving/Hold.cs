using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hold : MonoBehaviour {

	public ClientList clients;
	public Systems systems;
	public Emails emails;

	/*
		Clients
		Money/Reputation/Profit-Loss
		Server Placement map
		Server setups
		Player position
		Emails
		Tutorial position (later)
	*/

	void Start () {
		clients = new ClientList();
		systems = new Systems();
		emails = new Emails();
	}

	public ClientList getClients(){
		return clients;
	}
	public Systems getSystems(){
		return systems;
	}
	public Emails getEmails(){
		return emails;
	}
}
