using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class economy : MonoBehaviour {

	public float money;
	public float profit;

	void Awake(){
		DontDestroyOnLoad(this.gameObject);
	}

	void Start(){
		money = GameData.storage.money;
		profit = GameData.storage.profit;
	}

	void Update(){
		GameData.storage.money = money;
		GameData.storage.profit = profit;
	}

	public void IncreaseProfit(float a){
		profit += a;
	}

	public void DecreaseProfit(float a){
		profit -= a;
	}

	public void updateMoney(){
		money += profit;
		GameData.storage.money = money;
	}

	public void SetMoney(float a){
		money = a;
	}

	public void SetProfit(float a){
		profit = a;
	}

	public float GetMoney(){
		return money;
	}

	public float GetProfit(){
		return profit;
	}

	public void Pay(float a){
		money -= a;
	}
}
