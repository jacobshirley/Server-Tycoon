using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class economy : MonoBehaviour {

	public int money;
	public int profit;

	public Text moneyDisplay;
	public Text profitDisplay;

	public GameObject moneyIncrease;
	public GameObject moneyDecrease;

	// Use this for initialization
	void Start () {
		money = 10;
		profit = 0;
		InvokeRepeating("updateMoney", 0.1f, 0.5f);
	}

	// Update is called once per frame
	void Update () {
		moneyDisplay.text = "Money : " + money;
		profitDisplay.text = "Profit/Loss : " + profit;
	}

	public void increase(){
		profit += 10;
	}

	public void decrease(){
		profit -= 10;
	}

	public void updateMoney(){
		money += profit;
	}
}
