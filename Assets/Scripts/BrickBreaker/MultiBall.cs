using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiBall : MonoBehaviour {
    private Rigidbody rb;
    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.AddForce(new Vector3(Random.Range(-600f, 600f), Random.Range(0, 600f), 0) / 100);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
