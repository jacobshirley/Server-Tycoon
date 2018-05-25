using UnityEngine;
using System.Collections;

public class Shooter : MonoBehaviour
{

    private float Speed = 5f;
    public GameObject bullet;

    private Vector2 playerPos = new Vector2(0, -5f);

    private bool canShoot = true;
    private float shootDelay = 0.3f;

    void Update()
    {
        if (!GMSpaceInvaders.instance.paused)
        {
            float xPos = transform.position.x + (Input.GetAxis("Horizontal") * Speed * Time.deltaTime);
            playerPos = new Vector2(Mathf.Clamp(xPos, -8f, 8f), -5f);
            transform.position = playerPos;

            if (shootDelay < 0)
            {
                canShoot = true;
            }
            else
            {
                shootDelay -= Time.deltaTime;
            }
            if (canShoot)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    GameObject newBullet = Instantiate(bullet);
                    canShoot = false;
                    shootDelay = 0.3f;
                }
            }
        }


    }

   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyBullet"))
        {
            GMSpaceInvaders.instance.takeDamage();
        }
    }
}