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
            rep.removeRep((int)(rep.GetRep() * Settings.SELL_SERVER_REP_LOSS_PERCENT));

            //add money to economy
            float money = GameObject.Find("Money").GetComponent<economy>().GetMoney();
            GameObject.Find("Money").GetComponent<economy>().SetMoney(money + ValueServer());

            GameData.servers.Remove(GameData.CurrentServer);
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
