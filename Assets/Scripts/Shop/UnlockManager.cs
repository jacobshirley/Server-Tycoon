using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockManager : MonoBehaviour {

	public GameObject NextFish;

	public void EnableNext(){
		NextFish.gameObject.transform.Find("Disabled").GetComponent<DisableManager>().Enable();
		if(!GameData.storage.unlockedFish.Contains(NextFish.name)){
			GameData.storage.unlockedFish.Add(NextFish.name);
		}
	}

}
