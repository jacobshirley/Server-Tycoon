using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour {

    private Snake next;

    public Snake GetNext()
    {
        return next;
    }

    public void SetNext(Snake snake)
    {
        next = snake;
    }

    public void RemoveTail()
    {
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SnakeFood"))
        {
            GMSnake.instance.EatFood();
        }
        else if (other.CompareTag("Snake"))
        {
            GMSnake.instance.GameOver();
        }
    }

}
