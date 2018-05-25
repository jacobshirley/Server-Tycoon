using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{

    private float speed = -7f;
    private float xPos, yPos;
    // Use this for initialization
    void Start()
    {


        transform.position = new Vector3(xPos, yPos, 0);

    }

    // Update is called once per frame
    void Update()
    {
        float yPos = transform.position.y + (speed * Time.deltaTime);
        transform.position = new Vector2(xPos, yPos);
    }

    public void SetPos(Vector3 enemyPos)
    {
        xPos = enemyPos.x;
        yPos = enemyPos.y;
    }
    

    


}
