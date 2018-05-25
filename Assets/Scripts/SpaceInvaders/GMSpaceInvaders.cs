using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GMSpaceInvaders : MonoBehaviour {

    private int enemyCount = 55, score = 0, waveCount, lifeCount = 3;
    public static GMSpaceInvaders instance = null;
    public GameObject enemies, playerPrefab;
    public GameObject scoreTxt, waveTxt, pausePnl;
    public Text waveCleared;
    public GameObject gameOver;
    public GameObject life1, life2, life3;
    public bool paused = false;
    public GameObject enemyBullet;


    private float enemyShootTimer = 5f, maxEnemyShootTimer = 5f;
    private int cols;
    private GameObject Player, enemyObject;
    private int chooseCol, chooseEnemy;
    private float enemySpeed = 0.5f;


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
        enemyObject = (GameObject)GameObject.Instantiate(enemies, transform.position, Quaternion.identity);
        enemyObject.name = "Enemies";
        Player = (GameObject)GameObject.Instantiate(playerPrefab, transform.position, Quaternion.identity);
        Player.name = "Player";
        score = 0;
        waveCount = 1;
        cols = enemies.transform.childCount;
        Debug.Log("Cols: " + cols + " enemies: " + enemyObject.transform.GetChild(0).childCount);
    }

    // Update is called once per frame
    void Update () {
        int fr = (int)(1 / Time.deltaTime);
        if (paused)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene("SpaceInvaders");
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                paused = false;
                pausePnl.SetActive(false);
            }
            else if (Input.GetKeyDown(KeyCode.Q))
            {
                SceneManager.LoadScene("GameManager");
            }
        }
        else // game not paused
        {
            enemyShootTimer -= Time.deltaTime;
            if (enemyShootTimer <= 0 && enemyObject.transform.childCount > 0)
            {
                chooseCol = Random.Range(0, enemyObject.transform.childCount);
                chooseEnemy = Random.Range(0, enemyObject.transform.GetChild(chooseCol).transform.childCount);
                enemyShootTimer = maxEnemyShootTimer;
                enemyObject.transform.GetChild(chooseCol).transform.GetChild(chooseEnemy).GetComponent<SpriteRenderer>().color = Color.red;

                Invoke("EnemyShoot", 0.2f);

            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                paused = true;
                pausePnl.SetActive(true);
                Debug.Log("Paused");
            }
            else if (Input.GetKeyDown(KeyCode.P))
            {
                takeDamage();
            }

        }
    }

    private void EnemyShoot()
    {
        if (enemyObject.transform.childCount >= chooseCol && enemyObject.transform.GetChild(chooseCol).childCount > chooseEnemy)
        {
            GameObject newEnemyBullet = (GameObject)GameObject.Instantiate(enemyBullet);
            newEnemyBullet.GetComponent<EnemyBullet>().SetPos(enemyObject.transform.GetChild(chooseCol).transform.GetChild(chooseEnemy).transform.position);
            Invoke("ResetColour", 0.2f);
        }
        else
        {
            enemyShootTimer = 0.2f;
        }
    }

    private void ResetColour()
    {
        enemyObject.transform.GetChild(chooseCol).transform.GetChild(chooseEnemy).GetComponent<SpriteRenderer>().color = Color.white;
    }

    public void DestroyEnemy()
    {
        enemyCount--;
        score += 20;
        scoreTxt.GetComponent<TextMesh>().text = "Score: " + score.ToString();
        Debug.Log("Destroyed, " + enemyCount + " remain");
        checkWaveWin();

    }

    public void DestroyCol()
    {
        cols--;
    }

    public void takeDamage()
    {
        Destroy(Player);
        switch (lifeCount)
        {
            case 3:
                Destroy(life3);
                Invoke("Respawn", 1f);
                break;
            case 2:
                Destroy(life2);
                Invoke("Respawn", 1f);
                break;
            case 1:
                Destroy(life1);
                GameOver();
                break;
        }
        lifeCount--;
    }

    void Respawn()
    {
        Player = (GameObject)GameObject.Instantiate(playerPrefab);
        Player.name = "Player";
    }

    void GameOver()
    {
        gameOver.gameObject.SetActive(true);
        paused = true;
    }

    void checkWaveWin()
    {
        if (enemyCount < 1 || cols < 1)
        {
            
            waveCleared.text = "Wave " + waveCount + " Cleared!";
            waveCleared.gameObject.SetActive(true);
            waveCount++;
            Invoke("Reset", 1f);
        }
    }

    private void Reset()
    {
        Destroy(enemyObject);
        waveTxt.GetComponent<TextMesh>().text = "Wave " + waveCount;
        waveCleared.gameObject.SetActive(false);
        cols = enemies.transform.childCount;
        enemyObject = (GameObject)GameObject.Instantiate(enemies, transform.position, Quaternion.identity);
        enemyObject.name = "Enemies";
        Debug.Log("Cols: " + enemyObject.transform.childCount + " Enemies: " + enemyObject.transform.GetChild(0).childCount);
        enemyCount = 55;
        enemySpeed += 0.5f;
        if (maxEnemyShootTimer > 0)
        {
            maxEnemyShootTimer--;
        }
        enemyShootTimer = maxEnemyShootTimer;
    }

    public GameObject getEnemies()
    {
        return enemyObject;
    }

    public void CheckLevel()
    {
        int biggestCol = 0, max = 0;
        for (int i = 0; i < cols; i++)
        {
            if (enemyObject.transform.GetChild(i).childCount > max)
            {
                biggestCol = i;
                max = enemyObject.transform.GetChild(i).childCount;
            }
        }
        Debug.Log(enemyObject.transform.GetChild(biggestCol).transform.GetChild(max-1).transform.position.y);
        if (enemyObject.transform.GetChild(biggestCol).transform.GetChild(max-1).transform.position.y <= -4f)
        {
            takeDamage();
            Destroy(enemyObject);
            enemyObject = (GameObject)GameObject.Instantiate(enemies, transform.position, Quaternion.identity);
            enemyObject.name = "Enemies";
            enemyCount = 55;
        }
    }

    public float GetEnemySpeed()
    {
        return enemySpeed;
    }
}
