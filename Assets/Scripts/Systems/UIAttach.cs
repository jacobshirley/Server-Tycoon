using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAttach : MonoBehaviour {

	public GameObject TimeManager;
	public GameObject EconomyManager;
	public GameObject RepManager;

	public Text date;
	public Text money;
	public Text profit;
	public Slider reputation;

	void Start () {
		TimeManager = GameObject.Find("Time");
		EconomyManager = GameObject.Find("Money");
		RepManager = GameObject.Find("Rep");
	}

	void Update () {
		date.GetComponent<Text>().text = TimeManager.GetComponent<timeManager>().GetDate();
		money.GetComponent<Text>().text = "Money: £" + EconomyManager.GetComponent<economy>().GetMoney().ToString();
		profit.GetComponent<Text>().text = "Profit: £" + EconomyManager.GetComponent<economy>().GetProfit().ToString();
		reputation.GetComponent<Slider>().value = RepManager.GetComponent<reputation>().GetRep();
	}
}
