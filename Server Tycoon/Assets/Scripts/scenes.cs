using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scenes : MonoBehaviour {
	void Start(){
		DontDestroyOnLoad(this.gameObject);
		SceneManager.SetActiveScene(SceneManager.GetSceneByName("game"));
	}

	public void loadEmail(){
		SceneManager.LoadScene("EmailClient", LoadSceneMode.Additive);
		GameObject.Find("manBlue_stand").GetComponent<playerController>().movementDisable();
	}

	public void returnMain(){
		SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName("EmailClient"));
		GameObject.Find("manBlue_stand").GetComponent<playerController>().movementActivate();
	}

	public void loadServer(){
		SceneManager.LoadScene("Server Setup", LoadSceneMode.Additive);
		GameObject.Find("manBlue_stand").GetComponent<playerController>().movementDisable();
	}

	public void loadConfig(){
		SceneManager.LoadScene("Configure", LoadSceneMode.Additive);
	}

	public void removeConfig(){
		SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName("Configure"));
	}

	public void removeServer(){
		SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName("Server Setup"));
		GameObject.Find("manBlue_stand").GetComponent<playerController>().movementActivate();
	}

	public void loadClient(){
		SceneManager.LoadScene("ClientSelect", LoadSceneMode.Additive);
		GameObject.Find("manBlue_stand").GetComponent<playerController>().movementDisable();
	}

	public void removeClient(){
		SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName("ClientSelect"));
		GameObject.Find("manBlue_stand").GetComponent<playerController>().movementActivate();
	}

}
