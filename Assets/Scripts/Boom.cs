using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boom : MonoBehaviour {

	void Start () {
		Invoke("menu", 8);
		Destroy(GameObject.Find("Time"));
		Destroy(GameObject.Find("Load"));
		Destroy(GameObject.Find("Money"));
		Destroy(GameObject.Find("Rep"));
	}

	void menu(){
		SceneManager.LoadScene("Main Menu");
	}

}
