using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class gameOverSwitch : MonoBehaviour {

	private float time = 5;
	public TextMeshProUGUI parent;
  public GameObject minus10;

	void Start(){
		if(this.gameObject.name.Equals("Fail")){
			GameObject.Find("Rep").GetComponent<reputation>().removeRep(Settings.REP_LOSS);
			parent.text = "You have lost " + Settings.REP_LOSS + " reputation.";
      GameData.ScenarioClient.satisfaction -= Settings.CLIENT_SATISFACTION_DECREASE;
		}
		else{
			GameObject.Find("Rep").GetComponent<reputation>().addRep(Settings.REP_GAIN);
			parent.text = "You have gained " + Settings.REP_GAIN + " reputation.";
      GameData.ScenarioClient.satisfaction += Settings.CLIENT_SATISFACTION_INCREASE;
    }
    GameData.storage.EmailList.Remove(GameData.ScenarioEmail);
}

	void Update () {
		time = time - Time.deltaTime;

		if(time <= 0){
			new Save().save(GameData.storage);
			GameObject.Find("Time").GetComponent<timeManager>().enabled = true;
			SceneManager.LoadScene("game");
		}
	}
}
