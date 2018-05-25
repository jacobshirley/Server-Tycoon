using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHandle : MonoBehaviour {

	public GameObject self;

	public GameObject button1;
	public GameObject button2;
	public GameObject button3;
	public GameObject button4;
	public GameObject button5;
	public GameObject button6;

	public bool out1 = false;
	public bool out2 = false;
	public bool out3 = false;
	public bool out4 = false;
	public bool out5 = false;
	public bool out6 = false;

	void Start () {
		//Dark red 175, 1, 65
		//Orange 175, 79, 0
		button1.GetComponent<Image>().color = new Color(175/255f, 1/255f, 65/255f);
		button2.GetComponent<Image>().color = new Color(175/255f, 1/255f, 65/255f);
		button3.GetComponent<Image>().color = new Color(175/255f, 1/255f, 65/255f);
		button4.GetComponent<Image>().color = new Color(175/255f, 1/255f, 65/255f);
		button5.GetComponent<Image>().color = new Color(175/255f, 1/255f, 65/255f);
		button6.GetComponent<Image>().color = new Color(175/255f, 1/255f, 65/255f);
	}

	// Update is called once per frame
	void Update () {
	}

	public void switch1(){
		out1 = !out1;
		if(out1){
			button1.GetComponent<Image>().color = new Color(175/255f, 79/255f, 0/255f);
		}
		else{
			button1.GetComponent<Image>().color = new Color(175/255f, 1/255f, 65/255f);
		}
	}

	public void switch2(){
		out2 = !out2;
		if(out2){
			button2.GetComponent<Image>().color = new Color(175/255f, 79/255f, 0/255f);
		}
		else{
			button2.GetComponent<Image>().color = new Color(175/255f, 1/255f, 65/255f);
		}
	}

	public void switch3(){
		out3 = !out3;
		if(out3){
			button3.GetComponent<Image>().color = new Color(175/255f, 79/255f, 0/255f);
		}
		else{
			button3.GetComponent<Image>().color = new Color(175/255f, 1/255f, 65/255f);
		}
	}

	public void switch4(){
		out4 = !out4;
		if(out4){
			button4.GetComponent<Image>().color = new Color(175/255f, 79/255f, 0/255f);
		}
		else{
			button4.GetComponent<Image>().color = new Color(175/255f, 1/255f, 65/255f);
		}

	}
	public void switch5(){
		out5 = !out5;
		if(out5){
			button5.GetComponent<Image>().color = new Color(175/255f, 79/255f, 0/255f);
		}
		else{
			button5.GetComponent<Image>().color = new Color(175/255f, 1/255f, 65/255f);
		}
	}

	public void switch6(){
		out6 = !out6;
		if(out6){
			button6.GetComponent<Image>().color = new Color(175/255f, 79/255f, 0/255f);
		}
		else{
			button6.GetComponent<Image>().color = new Color(175/255f, 1/255f, 65/255f);
		}

	}

}
