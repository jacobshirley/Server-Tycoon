using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GM : MonoBehaviour
{

    public int lives = 3;
    public int bricks = 20;
    public float resetDelay = 1f;
    public Text livesText, levelText;
    public GameObject gameOver, pauseMenu;
    public GameObject youWon;
    public GameObject l1, l2, l3;
    public GameObject paddle;
    public GameObject multiBallball;
    public GameObject deathParticles;
    public static GM instance = null;
    public GameObject instructions;

    private GameObject clonePaddle;
    private GameObject lvl;
    private int curLevel;
    
    public bool paused = false, gameOverBool = false;

    // Use this for initialization
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        Setup();

    }

    public void Setup()
    {
        clonePaddle = Instantiate(paddle, transform.position, Quaternion.identity) as GameObject;
        livesText.text = "Lives: " + lives;
        instructions.SetActive(true);
        LoadOne();
    }

    private void Update()
    {
        if (paused || gameOverBool)
        {
            if (Input.GetKeyDown(KeyCode.Escape) && !gameOverBool)
            {
                paused = false;
                pauseMenu.SetActive(false);
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                SceneManager.LoadScene("GameManager");
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene("BrickBreaker");
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                paused = true;
                pauseMenu.SetActive(true);
            }
        }
    }

    void CheckGameOver()
    {
        if (bricks < 1)
        {
            youWon.SetActive(true);
            Time.timeScale = .5f;
            Destroy(lvl);
            Destroy(GameObject.Find("Ball"));
            Destroy(clonePaddle);
            switch (curLevel)
            {
                case 1:
                    
                    Invoke("LoadTwo", resetDelay);
                    Debug.Log("Load 2");
                    break;
                case 2:
                   
                    Invoke("LoadThree", resetDelay);
                    break;
                case 3:
                    
                    Invoke("LoadOne", resetDelay);
                    break;
            }
        }

        if (lives < 1)
        {
            gameOver.SetActive(true);
            paused = true;
            gameOverBool = true;
            Time.timeScale = .5f;
        }

    }

    void Reset()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("BrickBreaker");
    }

    public void LoseLife()
    {
        lives--;
        livesText.text = "Lives: " + lives;
        Instantiate(deathParticles, clonePaddle.transform.position, Quaternion.identity);
        Destroy(clonePaddle);
        CheckGameOver();
        Invoke("SetupPaddle", resetDelay);
        instructions.SetActive(true);
        GameObject.Destroy(GameObject.Find("Ball"));
    }

    void SetupPaddle()
    {
        if (!gameOverBool)
        {
            clonePaddle = Instantiate(paddle, transform.position, Quaternion.identity) as GameObject;
        }
        
    }

    public void DestroyBrick()
    {
        bricks--;
        CheckGameOver();
        Debug.Log(bricks);
    }

    void LoadOne()
    {
        Destroy(GameObject.Find("Ball"));
        Destroy(clonePaddle);
        Time.timeScale = 1f;
        lives = 3;
        curLevel = 1;
        bricks = 20;
        youWon.SetActive(false);
        Debug.Log(bricks);
        SetupPaddle();
        lvl = Instantiate(l1, transform.position, Quaternion.identity);
        levelText.text = "Level: " + curLevel;
    }

    void LoadTwo()
    {
        Destroy(GameObject.Find("Ball"));
        Destroy(clonePaddle);
        Time.timeScale = 1f;
        lives = 5;
        curLevel = 2;
        bricks = 12;
        youWon.SetActive(false);
        SetupPaddle();
        lvl = Instantiate(l2, transform.position, Quaternion.identity);
        levelText.text = "Level: " + curLevel;
    }

    void LoadThree()
    {
        Destroy(GameObject.Find("Ball"));
        Destroy(clonePaddle);
        Time.timeScale = 1f;
        lives = 10;
        curLevel = 3;
        bricks = 22;
        youWon.SetActive(false);
        SetupPaddle();
        lvl = Instantiate(l3, transform.position, Quaternion.identity);
        levelText.text = "Level: " + curLevel;
    }

    public void StartMultiBall()
    {
        for (int i = 0; i < 5; i++)
        {
            StartCoroutine(SpawnMultiBall());
            StartCoroutine(SpawnMultiBall());
            StartCoroutine(SpawnMultiBall());
            StartCoroutine(SpawnMultiBall());

        }
    }

   

    IEnumerator SpawnMultiBall()
    {
            yield return new WaitForSecondsRealtime(0.5f);
            Instantiate(multiBallball);
    }

}
