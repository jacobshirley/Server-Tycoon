using UnityEngine;
using System.Collections;

public class DeadZone : MonoBehaviour
{

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Ball"))
        {
            GM.instance.LoseLife();
        }
        else
        {
            Destroy(col.gameObject);
        }
        
    }
}