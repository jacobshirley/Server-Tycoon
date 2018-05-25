using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameLoader : MonoBehaviour {

	public string Location;
	private bool bought;
	public Transform Lock;

	void Start(){
		if(!GameData.storage.unlockedGames.Contains(this.gameObject.name)){
			this.gameObject.transform.Find("Image").GetComponent<Image>().color = new Color(85/255f, 85/255f, 85/255f);
			Transform l = Instantiate(Lock);
			l.SetParent(this.gameObject.transform, false);
		}
	}

	public void ChangeScene(){
		Debug.Log(this.gameObject.name);
		if(GameData.storage.unlockedGames.Contains(this.gameObject.name)){
			SceneManager.LoadScene(this.gameObject.name.Replace(" ", ""));
		}
		else{
			Debug.Log("Um?");
		}
	}
}
