using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Binary : MonoBehaviour {

	public GameObject self;
	public Text text;
	public int playerAns;

	private bool out1;
	private bool out2;
	private bool out3;
	private bool out4;
	private bool out5;
	private bool out6;

	public int ans;

	public void Start(){
		ans = Random.Range(1,64);
		text.text = ans.ToString();
	}

	public void check(){

		playerAns = 0;

		out1 = self.GetComponent<ButtonHandle>().out1;
		out2 = self.GetComponent<ButtonHandle>().out2;
		out3 = self.GetComponent<ButtonHandle>().out3;
		out4 = self.GetComponent<ButtonHandle>().out4;
		out5 = self.GetComponent<ButtonHandle>().out5;
		out6 = self.GetComponent<ButtonHandle>().out6;

		if(out1){
			playerAns += 32;
		}
		if(out2){
			playerAns += 16;
		}
		if(out3){
			playerAns += 8;
		}
		if(out4){
			playerAns += 4;
		}
		if(out5){
			playerAns += 2;
		}
		if(out6){
			playerAns += 1;
		}

		if(playerAns == ans){
			GameObject.Find("Canvas").GetComponent<Generation>().Dec();
			Destroy(self);
		}

	}
}
