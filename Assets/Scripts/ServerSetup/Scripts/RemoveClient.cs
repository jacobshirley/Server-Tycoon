using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RemoveClient : MonoBehaviour {
    public Client client;

	// Use this for initialization
	void Start () {
        this.transform.Find("Remove").GetComponent<Button>().onClick.AddListener(delegate
        {
            GameData.storage.clients.RemoveClient(client.id);
            GameObject.Find("Time").GetComponent<EventManager>().Trigger("RemoveClient", client);

            GameObject.Find("Rep").GetComponent<reputation>().removeRep(10);

            this.gameObject.SetActive(false);
        });
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
