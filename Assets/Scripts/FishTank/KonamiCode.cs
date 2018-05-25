using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KonamiCode : MonoBehaviour {
	private string[] KONAMI = {"UpArrow", "UpArrow", "DownArrow", "DownArrow", "LeftArrow", "RightArrow", "LeftArrow", "RightArrow", "B", "A", "Space"};
	private int pos;

	void Start(){
		pos = 0;
	}

	void Update() {
		//Input.GetKeyDown(KeyCode.UpArrow)
		if(Input.GetKeyDown(KeyCode.UpArrow)){
			if(KONAMI[pos].Equals("UpArrow")){
				pos++;
			}
			else{
				pos = 0;
			}
		}
		else if(Input.GetKeyDown(KeyCode.DownArrow)){
			if(Input.GetKeyDown(KeyCode.DownArrow)){
				if(KONAMI[pos].Equals("DownArrow")){
					pos++;
				}
				else{
					pos = 0;
				}
			}
		}
		else if(Input.GetKeyDown(KeyCode.LeftArrow)){
			if(Input.GetKeyDown(KeyCode.LeftArrow)){
				if(KONAMI[pos].Equals("LeftArrow")){
					pos++;
				}
				else{
					pos = 0;
				}
			}
		}
		else if(Input.GetKeyDown(KeyCode.RightArrow)){
			if(Input.GetKeyDown(KeyCode.RightArrow)){
				if(KONAMI[pos].Equals("RightArrow")){
					pos++;
				}
				else{
					pos = 0;
				}
			}
		}
		else if(Input.GetKeyDown(KeyCode.A)){
			if(Input.GetKeyDown(KeyCode.A)){
				if(KONAMI[pos].Equals("A")){
					pos++;
				}
				else{
					pos = 0;
				}
			}
		}
		else if(Input.GetKeyDown(KeyCode.B)){
			if(Input.GetKeyDown(KeyCode.B)){
				if(KONAMI[pos].Equals("B")){
					pos++;
				}
				else{
					pos = 0;
				}
			}
		}
		else if(Input.GetKeyDown(KeyCode.Space)){
			if(Input.GetKeyDown(KeyCode.Space)){
				if(KONAMI[pos].Equals("Space")){
					pos++;
				}
				else{
					pos = 0;
				}
			}
		}

		if(pos == 11){
			GameData.storage.cheats = true;
		}
	}
}
