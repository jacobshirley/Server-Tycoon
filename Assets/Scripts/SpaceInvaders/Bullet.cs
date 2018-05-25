using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    private float speed = 7f;
    private float pos;
	// Use this for initialization
	void Start () {
        
   
        transform.position = GameObject.Find("Player").transform.position;
        pos = GameObject.Find("Player").transform.position.x;

    }
	
	// Update is called once per frame
	void Update () {
        float yPos = transform.position.y + (speed * Time.deltaTime);
        transform.position = new Vector2(pos, yPos);
    }

    
}
