using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class reputation : MonoBehaviour {

	private int rep;

	void Awake(){
		DontDestroyOnLoad(this.gameObject);
	}

	void Start(){
		rep = GameData.storage.rep;
	}

	public void addRep(int a){
		rep += a;
	}

	public void removeRep(int a){
		rep -= a;
	}

	public void SetRep(int a){
		rep = a;
	}

	public int GetRep(){
		return rep;
	}
}
