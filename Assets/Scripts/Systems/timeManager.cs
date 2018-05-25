using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class timeManager : MonoBehaviour {

	public int cycle;
	public float time;
	public int[] date;
	public Text output;

	public bool enabled;

	private static timeManager tm;
	public int repDecrease;
	private int repCount;

	private int phish;

	private string alphabet = "abcdefghijklmnopqrstuvwxyz";

	void Start () {
		date = new int[] {01,01,2012};
		DontDestroyOnLoad(this.gameObject);
		enabled = true;
		repCount = repDecrease;
		phish = 2;
	}

	void Update () {
        if (GameData.gamePaused)
            return;

        if (enabled){
			time -= Time.deltaTime;
		}
		if(time < 0){
			day();
			time = cycle;
		}

        economy econ = GameObject.Find("Money").GetComponent<economy>();
        econ.SetProfit(0);

        foreach (Client client in GameData.storage.clients.GetClients())
        {
            econ.IncreaseProfit(client.reqPay);
        }

        foreach (ServerPlacedScript server in GameData.servers)
        {
            econ.DecreaseProfit(server.data.costPerMonth);
        }
    }

	void day(){
		updateDate();
		updateEmails();
		updateRep();

        foreach (Client client in GameData.storage.clients.GetClients())
        {
            client.satisfaction -= 0.5f;
        }
        //checkPhish();
    }

	void updateRep(){
		repCount--;
		if(repCount == 0){
			repCount = repDecrease;
			GameObject.Find("Rep").GetComponent<reputation>().removeRep(1);
		}
	}

	void updateDate(){
		if(date[1] == 1 || date[1] == 3 || date[1] == 5 || date[1] == 7 || date[1] == 8 || date[1] == 10){
			if(date[0] == 31){
				date[0] = 1;
				date[1]++;
				GameObject.Find("Money").GetComponent<economy>().updateMoney();
			}
			else{date[0]++;}
		}
		else if(date[1] == 4 || date[1] == 6 || date[1] == 9 || date[1] == 11){
			if(date[0] == 30){
				date[0] = 1;
				date[1]++;
				GameObject.Find("Money").GetComponent<economy>().updateMoney();
			}
			else{date[0]++;}
		}
		else if (date[1] == 12){
			if(date[0] == 31){
				date[0] = 1;
				date[1] = 1;
				date[2]++;
				GameObject.Find("Money").GetComponent<economy>().updateMoney();
			}
			else{date[0]++;}
		}
		else{
			if(date[0] == 28){
				date[0] = 1;
				date[1]++;
				GameObject.Find("Money").GetComponent<economy>().updateMoney();
			}
			else{date[0]++;}
		}
	}

	public string GetDate(){
		return string.Join("-",date);
	}

	public int[] GetDateArray(){
		return date;
	}

	public void SetDate(int[] a){
		date = a;
	}

	public void updateEmails(){
		List<ServerPlacedScript> servers = GameData.servers;
		if(servers != null){
			foreach (Client cl in GameData.storage.clients.GetClients()) {
        cl.EmailFreq--;
				if(cl.EmailFreq == 0){
					Debug.Log("Server count - " + servers.Count);
					//Debug.Log("Client count - " + servers[i].data.clients.Count);
					GetComponent<Emails>().SendEmail(cl);
          cl.EmailFreq = Random.Range(Settings.MIN_EMAIL_FREQ, Settings.MAX_EMAIL_FREQ);

					if(SceneManager.GetActiveScene().name.Equals("game")){
						Invoke("hide", 3);
					}
				}
			}
		}
	}

	void hide(){
	}


	void checkPhish(){

		bool run = false;

		if(GameData.servers.Count != 0){
			foreach (ServerPlacedScript s in GameData.servers){
				if(s.data.clients.Count != 0){
					run = true;
				}
			}
		}

		if(run == true){
				phish--;
				if(phish == 0){
					Debug.Log("Phishing Email");

                    List<Client> clients = GameData.storage.clients.GetClients();

                    Client c = clients[(int)(Random.value * (clients.Count - 1))];
					this.gameObject.GetComponent<Emails>().SendEmail(genRandom(c), c);
				}
			}
		}

	string genRandom(Client c){
		Debug.Log(c.reqName);
		char ch = alphabet[Random.Range(0, alphabet.Length)];
		int pos = Random.Range(0, c.reqName.Length - 3);
		string returnable = "";

		for(int i = 0; i < c.reqName.Length; i++){
			if(i == pos){
				returnable += ch;
			}
			else{
				returnable += c.reqName[i];
			}
		}

		return returnable;
	}
}
