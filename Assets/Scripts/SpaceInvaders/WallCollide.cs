using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCollide : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.CompareTag("Invader"))
        //{
        Debug.Log("Wall Hit");
            
       // }
    }
}
