using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Generation : MonoBehaviour {

	public float time;
	public GameObject prefab;
	public GameObject canv;
	public int number;

	void Start(){
		time = 0;
		number = 0;
	}

	void Update () {
        if (GameData.gamePaused)
            return;

        time -= Time.deltaTime;
		if(time <= 0){
			GameObject a = Instantiate(prefab);
			a.transform.SetParent(canv.transform);
			a.transform.position = new Vector3(transform.position.x,transform.position.y + 150, transform.position.z);
			a.transform.localScale = new Vector3(1,1,1); 
			time = 5;
			number++;
			checkNum();
		}
	}

	void checkNum(){
		if(number > 4){
			Debug.Log("You Failed");
			SceneManager.LoadScene("Game Over");
		}
	}

	public void Dec(){
		number--;
	}
}
