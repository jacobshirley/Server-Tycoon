using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class reputation : MonoBehaviour {

	public int rep;
	public Slider bar;

	// Use this for initialization
	void Start () {
		rep = 500;

		//InvokeRepeating("setRandom", 0.1f, 0.5f); //This is for testing purposes only
	}

	// Update is called once per frame
	void Update () {
		bar.value = rep;
	}

	void addRep(int a){
		rep += a;
	}

	void removeRep(int a){
		rep += a;
	}

	void setRep(int a){
		rep = a;
	}

	//Used for testing
	void setRandom(){
		rep = Random.Range(1,100);
	}
}
