using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class finish : MonoBehaviour {

	private float timeLeft = 30;
	public int time;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update(){
     timeLeft -= Time.deltaTime;
     if (timeLeft < 0){
			 GameOver();
     }

		 time = (int)timeLeft;
 }

	void OnTriggerEnter2D(Collider2D collision){
			if (collision.gameObject.CompareTag("Finish")){
					timeLeft = 0;
					time = 0;
			}
	}

	void GameOver(){
		Debug.Log("Game Over.");
	}
}
