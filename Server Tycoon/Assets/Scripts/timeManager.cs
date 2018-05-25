using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timeManager : MonoBehaviour {

	public int cycle;
	public float time;
	public int[] date;

	void Start () {
		cycle = 2;
		date = new int[] {01,01,2012};
	}

	void Update () {
		time -= Time.deltaTime;
		if(time < 0){
			day();
			time = cycle;
		}
	}

	void day(){
		updateDate();
		Debug.Log(date[0] + " - " + date[1] + " - " + date[2]);
	}

	//Could re-write this so it looks much cleaner with a switch statement
	void updateDate(){
		if(date[1] == 1 || date[1] == 3 || date[1] == 5 || date[1] == 7 || date[1] == 8 || date[1] == 10){
			if(date[0] == 31){
				date[0] = 1;
				date[1]++;
				this.GetComponent<economy>().updateMoney();
			}
			else{date[0]++;}
		}
		else if(date[1] == 4 || date[1] == 6 || date[1] == 9 || date[1] == 11){
			if(date[0] == 30){
				date[0] = 1;
				date[1]++;
				this.GetComponent<economy>().updateMoney();
			}
			else{date[0]++;}
		}
		else if (date[1] == 12){
			if(date[0] == 31){
				date[0] = 1;
				date[1] = 1;
				date[2]++;
				this.GetComponent<economy>().updateMoney();
			}
			else{date[0]++;}
		}
		else{
			if(date[0] == 28){
				date[0] = 1;
				date[1]++;
				this.GetComponent<economy>().updateMoney();
			}
			else{date[0]++;}
		}
	}
}
