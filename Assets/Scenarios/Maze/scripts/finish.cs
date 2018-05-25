using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class finish : MonoBehaviour {

	private float timeLeft = 45;
	public int time;

	void Update(){
        if (GameData.gamePaused)
            return;

     timeLeft -= Time.deltaTime;
     if (timeLeft < 0){
			 GameOver();
     }

		 time = (int)timeLeft;
 }

	void OnTriggerEnter2D(Collider2D collision){
			if (collision.gameObject.CompareTag("Finish")){
					SceneManager.LoadScene("Passed");
			}
	}

	public void GameOver(){
		SceneManager.LoadScene("Game Over");
	}

	public void Win(){
		SceneManager.LoadScene("Passed");
	}
}
