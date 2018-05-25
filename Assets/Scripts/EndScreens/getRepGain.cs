using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class getRepGain : MonoBehaviour {

	public TextMeshProUGUI parent;
	public int repgain = 10;

	void Start () {
		parent.text = "You have gained " + (repgain) + " reputation.";
	}
}
