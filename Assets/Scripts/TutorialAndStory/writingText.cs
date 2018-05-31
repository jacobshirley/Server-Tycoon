using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class writingText : MonoBehaviour {

	public TextMeshProUGUI text;
	public string message;
	public int location;
	private bool write;

	private int Timer = 8;
	private int count;

	public Text cont;

	private int ArrayPoint = 0;
	public string Location;

	void Start () {
		location = 0;
		text.text = "";
		//message = this.gameObject.GetComponent<ReadTextFromFile>().LoadFile(ArrayPoint, Location).Text;
		write = false;
		if(cont != null){cont.enabled = false;}
		Invoke("RunMe", 2);
	}

	void Update () {
		if(location < message.Length && write == true && count == 0){
			text.text += message[location];
			location++;
			count = Timer;
		}
		if(location == message.Length && write == true){
			write = false;
			Invoke("showCont",2f);
		}
		count--;
	}

	void showCont(){
		if(cont != null){cont.enabled = true;}
		else{
			ArrayPoint++;
			if(ArrayPoint == 6){
				SceneManager.LoadScene("Base");
			}
			Reset();
		}
	}

	void RunMe(){
		write = true;
		count = Timer;
	}

	void Reset(){
		location = 0;
		text.text = "";
		Debug.Log(ArrayPoint);
		//message = this.gameObject.GetComponent<ReadTextFromFile>().LoadFile(ArrayPoint, Location).Text;
		write = false;
		if(cont != null){cont.enabled = false;}
		Invoke("RunMe", 0.5f);
	}
}
