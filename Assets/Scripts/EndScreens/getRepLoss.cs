using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class getRepLoss : MonoBehaviour {

	public TextMeshProUGUI parent;
	public int reploss = 10;

	void Start () {
		parent.text = "You have lost 10 reputation.";
	}
}
