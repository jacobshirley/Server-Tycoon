using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class showTime : MonoBehaviour {

	public Text text;
	public GameObject time;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		text.text = "" + time.GetComponent<finish>().time;
	}
}
