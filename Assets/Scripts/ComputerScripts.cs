using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ComputerScripts : MonoBehaviour {

	public void loadEmail(){
		SceneManager.LoadScene("EmailClient");
	}

	public void loadStore(){
		SceneManager.LoadScene("Shop");
	}

	public void loadGames(){
		SceneManager.LoadScene("GameManager");
	}

	public void logout(){
		new Save().save(GameData.storage);
		SceneManager.LoadScene("game");
	}

}
