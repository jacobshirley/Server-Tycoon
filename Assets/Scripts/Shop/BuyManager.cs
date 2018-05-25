using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyManager : MonoBehaviour {
	public void Purchase(){
		string PriceString = this.gameObject.transform.Find("Price").GetComponent<Text>().text.Split('£')[1];
		float price = float.Parse(PriceString);
		if(GameData.storage.money >= price){
			this.gameObject.GetComponent<ItemManager>().SetButton();
			GameObject.Find("Money").GetComponent<economy>().Pay(price);
			if(this.gameObject.GetComponent<UnlockManager>() != null){
				this.gameObject.GetComponent<UnlockManager>().EnableNext();
			}
			else{
				Destroy(this.gameObject);
			}
		}
		else{
			GameObject.Find("Error").GetComponent<Text>().text = "You don't have the money!";
			Invoke("clear",2);
		}
	}

	void clear(){
		GameObject.Find("Error").GetComponent<Text>().text = "";
	}
}
