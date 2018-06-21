using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SellServer : MonoBehaviour {

	void Start () {
        this.transform.Find("Value").GetComponent<Text>().text = "Value: £" + ValueServer();

        this.transform.Find("Sell").GetComponent<Button>().onClick.AddListener(delegate
        {
            reputation rep = GameObject.Find("Rep").GetComponent<reputation>();
            rep.removeRep((int)(GameData.CurrentServer.data.clients.Count * Settings.SELL_SERVER_CLIENT_REP_LOSS));

            //add money to economy
            float money = GameObject.Find("Money").GetComponent<economy>().GetMoney();
            GameObject.Find("Money").GetComponent<economy>().SetMoney(money + ValueServer());

            GameData.servers.Remove(GameData.CurrentServer);

            EventManager evManager = GameObject.Find("Time").GetComponent<EventManager>();

            foreach (int client in GameData.CurrentServer.data.clients)
            {
                Client cl = GameData.storage.clients.RemoveClient(client);
                evManager.Trigger("RemoveClient", cl);
            }

            Destroy(GameData.CurrentServer.gameObject);
        });
	}

    int ValueServer()
    {
        return GameData.CurrentServer.data.def.cost;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
