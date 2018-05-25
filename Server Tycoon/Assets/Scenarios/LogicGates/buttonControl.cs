using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buttonControl : MonoBehaviour {

	public GameObject button1;
	public GameObject button2;
	public GameObject button3;
	public GameObject button4;

	public bool out1 = false;
	public bool out2 = false;
	public bool out3 = false;
	public bool out4 = false;

	void Start () {
		button1.GetComponent<Image>().color = Color.red;
		button2.GetComponent<Image>().color = Color.red;
		button3.GetComponent<Image>().color = Color.red;
		button4.GetComponent<Image>().color = Color.red;
	}

	// Update is called once per frame
	void Update () {
	}

	public void switch1(){
		out1 = !out1;
		if(out1){
			button1.GetComponent<Image>().color = Color.green;
		}
		else{
			button1.GetComponent<Image>().color = Color.red;
		}
	}

	public void switch2(){
		out2 = !out2;
		if(out2){
			button2.GetComponent<Image>().color = Color.green;
		}
		else{
			button2.GetComponent<Image>().color = Color.red;
		}
	}

	public void switch3(){
		out3 = !out3;
		if(out3){
			button3.GetComponent<Image>().color = Color.green;
		}
		else{
			button3.GetComponent<Image>().color = Color.red;
		}
	}

	public void switch4(){
		out4 = !out4;
		if(out4){
			button4.GetComponent<Image>().color = Color.green;
		}
		else{
			button4.GetComponent<Image>().color = Color.red;
		}
	}

}
