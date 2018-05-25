using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GMSnake : MonoBehaviour {

    public static GMSnake instance = null;

    private int scoreCount, snakeLength, maxSnakeLength;
    private int worldMultiplier;
    private float foodTimer, maxFoodTimer = 5;
    private float moveTimer;
    private float turnTimer;
    private Vector2 newPos;
    private int dir;
    private int xBound, yBound;

    public GameObject curFood;
    private GameObject snakeHead;
    public GameObject snakePart, food, gameOverPnl, pausePnl;

    public Snake head, tail;
    public bool paused = false, gameOver = false;
    public Text scoreTxt;


    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        Setup();

    }

    void Setup()
    {
        scoreCount = 0;
        maxSnakeLength = 4;
        snakeLength = 1;
        foodTimer = maxFoodTimer;
        moveTimer = 0.3f;
        dir = 0;
        xBound = 23;
        yBound = 12;
        SpawnFood();
        turnTimer = 0.2f;
    }
	
	// Update is called once per frame
	void Update () {
        if (paused)
        {
            if (Input.GetKeyDown(KeyCode.Escape) && !gameOver)
            {
                paused = false;
                pausePnl.SetActive(false);
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene("Snake");
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                //return to game
                Debug.Log("Quitting");
                SceneManager.LoadScene("GameManager");
            }
        }
        else
        {
            moveTimer -= Time.deltaTime;
            if (moveTimer <= 0)
            {
                moveTimer = 0.3f;
                Movement();
                StartCoroutine(CheckVisible());
                if (snakeLength >= maxSnakeLength)
                {
                    TailFunc();
                }
                else
                {
                    snakeLength++;
                }
            }

            turnTimer -= Time.deltaTime;
            if(turnTimer <= 0)
            {
                
                ChangeDir(); // check for key in to change direction
            }
            

            if (Input.GetKeyDown(KeyCode.Escape)) // pause check
            {
                Debug.Log("pause");
                paused = true;
                pausePnl.SetActive(true);
            }
        }
        


	}

    void ChangeDir()
    {
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && dir != 2)
        {
            dir = 0;
            turnTimer = 0.2f;
        }
        if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && dir != 1)
        {
            dir = 3;
            turnTimer = 0.2f;
        }
        if ((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) && dir != 0)
        {
            dir = 2;
            turnTimer = 0.2f;
        }
        if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && dir != 3)
        {
            dir = 1;
            turnTimer = 0.2f;
        }

    }

    void SpawnFood()
    {
        int xPos = Random.Range(-xBound, yBound);
        int yPos = Random.Range(-yBound, yBound);
        curFood = (GameObject)GameObject.Instantiate(food, new Vector2(xPos, yPos), transform.rotation);
        StartCoroutine(CheckRender(curFood));
    }

    IEnumerator CheckRender(GameObject IN)
    {
        yield return new WaitForEndOfFrame();
        if (!IN.GetComponent<Renderer>().isVisible)
        {
            if (IN.CompareTag("SnakeFood"))
            {
                Destroy(IN);
                SpawnFood();
            }
        }
    }

    void SpawnSnake()
    {
        snakeHead = (GameObject)GameObject.Instantiate(snakePart, new Vector3(0, 0, 0), Quaternion.identity);
        snakeHead.name = "SnakeHead";
        Debug.Log(snakeHead.transform.position);
        //Instantiate(snakePart, new Vector3(snakeHead.transform.position.x, snakeHead.transform.position.y-worldMultiplier, 0), Quaternion.identity);
        AddSnake();
    }

    void AddSnake()
    {
        GameObject newSnake = (GameObject)GameObject.Instantiate(snakePart);
        newSnake.transform.parent = snakeHead.transform;
        newSnake.transform.position -= new Vector3(0.04f, 1, 0);
    }

    void Movement()
    {
        GameObject temp;
        newPos = head.transform.position;

        switch (dir)
        {
            case 0:
                newPos = new Vector2(newPos.x, newPos.y + 1);
                break;
            case 1:
                newPos = new Vector2(newPos.x + 1, newPos.y);
                break;
            case 2:
                newPos = new Vector2(newPos.x, newPos.y - 1);
                break;
            case 3:
                newPos = new Vector2(newPos.x - 1, newPos.y);
                break;
        }

        temp = (GameObject)GameObject.Instantiate(snakePart, newPos, transform.rotation);
        head.SetNext(temp.GetComponent<Snake>());
        head = temp.GetComponent<Snake>();
        return;
    }

    void TailFunc()
    {
        Snake tempSnake = tail;
        tail = tail.GetNext();
        tempSnake.RemoveTail();
    }


    public void EatFood()
    {
        Destroy(curFood);
        scoreCount++;
        maxSnakeLength++;
        Invoke("SpawnFood", 1f);
        scoreTxt.text = "Score: " + scoreCount;
    }

    public void GameOver()
    {
        Debug.Log("DIE");
        paused = true;
        gameOver = true;
        Destroy(GameObject.Find("Snake(Clone)"));
        gameOverPnl.transform.GetChild(1).GetComponent<Text>().text = "Your final score was: " + scoreCount;
        gameOverPnl.SetActive(true);
    }

    void wrap()
    {
        if (dir == 0)
        {
            head.transform.position = new Vector2(head.transform.position.x, -(head.transform.position.y - 1));
        }
        else if (dir == 1)
        {
            head.transform.position = new Vector2(-(head.transform.position.x-1), head.transform.position.y);
        }
        else if(dir == 2)
        {
            head.transform.position = new Vector2(head.transform.position.x, -(head.transform.position.y + 1));
        }
        else if(dir == 3)
        {
            head.transform.position = new Vector2(-(head.transform.position.x + 1), head.transform.position.y);
        }
    }

    IEnumerator CheckVisible()
    {
        yield return new WaitForEndOfFrame();
        if (!head.GetComponent<Renderer>().isVisible)
        {
            wrap();
        }
    }
   
}
