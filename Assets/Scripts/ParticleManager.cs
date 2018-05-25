using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour {

    public GameObject minus10;
    public GameObject repObj;
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.P))
        {
            SpawnParticle();
        }
	}

    public void SpawnParticle()
    {
        GameObject newParticle = (GameObject)GameObject.Instantiate(minus10);
        newParticle.transform.SetParent(repObj.transform, false);
        //newParticle.transform.localPosition = repObj.transform.position;
        newParticle.transform.localScale = new Vector3(1, 1, 1);

    }
}
