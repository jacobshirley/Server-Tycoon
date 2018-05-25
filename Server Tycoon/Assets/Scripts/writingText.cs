using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class writingText : MonoBehaviour {

	public Text text;
	public string message;
	public int location;
	private bool write;

	public Text cont;

	// Use this for initialization
	void Start () {
		location = 0;
		text.text = "";
		message = "This is just a test to see if the scrolling text shit works + also to see if the user can actually see the text on a given background."
							+ "\n\nI don't really know wtf I'm saying lol";
		write = true;
		cont.enabled = false;
	}

	// Update is called once per frame
	void Update () {
		if(location < message.Length && write == true){
			text.text += message[location];
			location++;
		}
		if(location == message.Length){

			Invoke("showCont",1);
		}
	}

	void showCont(){
		cont.enabled = true;
	}
}
