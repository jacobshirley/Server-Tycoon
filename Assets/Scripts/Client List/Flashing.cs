using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flashing : MonoBehaviour {

    public float flashingIntervalSeconds = (float)0.5;

    private float lastUpdate = 0;
    private Image img;

	void Start () {
        img = this.GetComponent<Image>();
    }

	void Update () {
		if (Time.time > lastUpdate + flashingIntervalSeconds)
        {
            lastUpdate = Time.time;

            img.enabled = !img.enabled;
        }
	}
}
