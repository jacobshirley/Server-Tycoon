using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerLight : MonoBehaviour {

    public bool on = true;
    private float timer;

    public bool flash = false;

    public void SetColor(Color color)
    {
        this.GetComponent<SpriteRenderer>().color = color;
    }
	
	void Update () {
        if (Time.time > timer)
        {
            timer = Time.time + (float)(0.5 + (Random.value * 0.5));
            on = flash ? !on : true;

            Light l = this.GetComponentInChildren<Light>();
            SpriteRenderer ren = this.GetComponent<SpriteRenderer>();

            l.enabled = on;
            ren.enabled = on;

            if (on) {
                l.color = ren.color;
            }
            
        }
        
	}
}
