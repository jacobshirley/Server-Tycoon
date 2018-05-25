using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Caller : MonoBehaviour {

	public Button button;

	void Start () {
		button.onClick.AddListener(loadMain);
	}

	void loadMain(){
		SceneManager.LoadScene("ComputerMenu");
	}
}
