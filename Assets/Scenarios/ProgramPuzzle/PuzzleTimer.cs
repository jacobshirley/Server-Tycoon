using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PuzzleTimer : MonoBehaviour {

	public Text timer;
	public int attempts = 0;
	public float time = 15;

	public bool run = false;

	void Update(){
		if(time < 0){
			SceneManager.LoadScene("Game Over");
		}
		if(run){
			timer.text = time.ToString("#");
			time -= Time.deltaTime;
		}
		else{timer.text = "";}
	}

	public void Attempted(){

	}

	public void Toggle(){
		run = !run;
	}
}
