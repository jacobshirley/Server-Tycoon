using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class selectClient : MonoBehaviour {
	public Button self;

	void Start(){
		self.onClick.AddListener(GameObject.Find("Systems").GetComponent<scenes>().removeClient);
	}

}
