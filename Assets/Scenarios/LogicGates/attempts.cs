using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

//TextMeshProUGUI

public class attempts : MonoBehaviour {

	private int att = 1;	//Number of attempts taken - I've set the maximum to be three

	public void Attempted(){
		att++;
		if(att == 4){
			Debug.Log("Failed");
			SceneManager.LoadScene("Game Over");
		}
		this.gameObject.GetComponent<TextMeshProUGUI>().text = "Attempt " + att + "/3";
	}
}
