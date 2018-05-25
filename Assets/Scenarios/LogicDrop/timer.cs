using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class timer : MonoBehaviour {

	private float time = 30;
	public TextMeshProUGUI textbox;

    void Start()
    {
        textbox.text = time.ToString("#.00");
    }

	void Update () {
        if (GameData.gamePaused)
            return;

        time -= Time.deltaTime;
		if(time < 0){
			SceneManager.LoadScene("Passed");
		}
		textbox.text = time.ToString("#.00");
	}
}
