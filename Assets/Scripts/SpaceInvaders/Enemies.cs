using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour {

    private float speed = 0.5f;
	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        if (!GMSpaceInvaders.instance.paused)
        {
            if (speed > 0)
            {
                speed = GMSpaceInvaders.instance.GetEnemySpeed();
            }
            else
            {
                speed = GMSpaceInvaders.instance.GetEnemySpeed() * -1;
            }

            float xPos = transform.position.x + (speed * Time.deltaTime);
            transform.position = new Vector2(xPos, transform.position.y);

           // Debug.Log(GameObject.Find("Enemies").transform.GetChild(0).transform.GetChild(4).transform.position.y);
           if (GMSpaceInvaders.instance.getEnemies().transform.childCount > 0)
            {
                EdgeCollision();
            }
            
        }
        
    }

    void EdgeCollision()
    {
        if (GMSpaceInvaders.instance.getEnemies().transform.GetChild(0).transform.GetChild(0).transform.position.x < -7.93 && speed < 0)
        {
            speed *= -1;
            transform.position = new Vector2(transform.position.x, transform.position.y - 0.5f);
            GMSpaceInvaders.instance.CheckLevel();
        }
        else if (GMSpaceInvaders.instance.getEnemies().transform.GetChild(GMSpaceInvaders.instance.getEnemies().transform.childCount - 1).transform.GetChild(0).transform.position.x > 7.93f && speed > 0)
        {
            speed *= -1;
            transform.position = new Vector2(transform.position.x, transform.position.y - 0.5f);
            GMSpaceInvaders.instance.CheckLevel();
        }
    }


    

   
}
