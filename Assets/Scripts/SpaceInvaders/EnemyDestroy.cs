using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDestroy : MonoBehaviour {

    private void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            if (gameObject.transform.parent.childCount == 1)
            {
                Destroy(gameObject.transform.parent.gameObject);
                GMSpaceInvaders.instance.DestroyCol();
            }
            Destroy(gameObject);
            Destroy(collision.gameObject);
            GMSpaceInvaders.instance.DestroyEnemy();
        }
        
    }

    public void Shoot()
    {

    }
}
