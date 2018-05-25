using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExitButton : MonoBehaviour {

	void Start () {
        SceneManager.SetActiveScene(this.gameObject.scene);

        if (GameData.ui == null)
            GameData.ui = GameObject.Find("Server Placement UI");

        GameData.ui.SetActive(false);

        GameData.move = false;

        GetComponent<Button>().onClick.AddListener(delegate {
            SceneManager.UnloadSceneAsync(this.gameObject.scene);
            GameData.ui.SetActive(true);

            GameData.move = true;
            GameData.menuOpen = false;
        });
	}
	
	void Update () {
		
	}
}
