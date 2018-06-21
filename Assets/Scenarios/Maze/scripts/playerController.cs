using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class playerController : MonoBehaviour {

    public float speed = 4f;
    public GameObject player;
    Rigidbody2D rb2d;
    public Text promptTxt;

    public GameObject scripts;
    private int serverTriggers = 0;

    private bool collisionWithPC = false;
    private bool collisionWithTank = false;
    private bool collisionWithBookcase = false;
    private GameObject ServerCollision = null;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.freezeRotation = true;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("workstation"))
        {
            promptTxt.text = "Press 'e' to log on";
            collisionWithPC = true;
        }

        if (collision.gameObject.CompareTag("server"))
        {
            serverTriggers++;
            promptTxt.text = "Press 'e' to configure server";
            ServerCollision = collision.gameObject;
        }

        if (collision.gameObject.CompareTag("FishTank"))
        {
            promptTxt.text = "Press 'e' to view the fish tank";
            collisionWithTank = true;
        }

        if (collision.CompareTag("BookCase"))
        {
            promptTxt.text = "Press 'e' to view your books";
            collisionWithBookcase = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("server"))
        {
            serverTriggers--;
            if (serverTriggers == 0)
            {
                ServerCollision = null;
            }
        }

        if (collision.gameObject.CompareTag("workstation"))
        {
            collisionWithPC = false;
        }

        if (collision.gameObject.CompareTag("FishTank"))
        {
            collisionWithTank = false;
        }

        if (collision.gameObject.CompareTag("BookCase"))
        {
            collisionWithBookcase = false;
        }

        if (!collisionWithPC && !collisionWithTank && !collisionWithBookcase && ServerCollision == null)
            promptTxt.text = "";
    }

    void Update()
    {
        if (GameData.gamePaused)
            return;

        if (!GameData.menuOpen)
        {
            if (collisionWithPC && Input.GetKeyDown("e"))
            {
                Debug.Log("Logged on");
                GameData.storage.position = GameObject.Find("manBlue_stand").transform.position;
                new Save().save(GameData.storage);
                SceneManager.LoadScene("ComputerMenu");
                GameData.move = false;
                GameData.menuOpen = true;
            }
            else if (ServerCollision != null && Input.GetKeyDown("e"))
            {
                Debug.Log("Open server config");
                GameData.CurrentServer = ServerCollision.GetComponent<ServerPlacedScript>();
                GameData.storage.position = GameObject.Find("manBlue_stand").transform.position;
                new Save().save(GameData.storage);
                SceneManager.LoadScene("Server Setup", LoadSceneMode.Additive);
                GameData.move = false;
                GameData.menuOpen = true;
            }
            else if (collisionWithTank && Input.GetKeyDown("e"))
            {
                Debug.Log("Fish tank :)");
                GameData.storage.position = GameObject.Find("manBlue_stand").transform.position;
                new Save().save(GameData.storage);
                SceneManager.LoadScene("FishTank");
                GameData.move = false;
                GameData.menuOpen = true;
            }
            else if (collisionWithBookcase && Input.GetKeyDown("e"))
            {
                Debug.Log("Bookcase :)");
                GameData.storage.position = GameObject.Find("bookcase").transform.position;
                new Save().save(GameData.storage);
                SceneManager.LoadScene("BookCase", LoadSceneMode.Additive);
                GameData.move = false;
                GameData.menuOpen = true;
            }
        }
        if (GameData.move)
        {
            Vector2 targetVelocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            rb2d.velocity = targetVelocity * speed;

            if (rb2d.velocity.x > 0 && rb2d.velocity.y > 0) //top right
            {
                player.transform.rotation = Quaternion.Euler(0, 0, -315);
            }
            else if (rb2d.velocity.x > 0 && rb2d.velocity.y < 0) //bottom right
            {
                player.transform.rotation = Quaternion.Euler(0, 0, -45);
            }
            else if (rb2d.velocity.x < 0 && rb2d.velocity.y > 0) //top left
            {
                player.transform.rotation = Quaternion.Euler(0, 0, -225);
            }
            else if (rb2d.velocity.x < 0 && rb2d.velocity.y < 0) //bottom left
            {
                player.transform.rotation = Quaternion.Euler(0, 0, -135);
            } else if (rb2d.velocity.y > 0)
            {
                player.transform.rotation = Quaternion.Euler(0, 0, -270);
            }
            else if (rb2d.velocity.y < 0)
            {
                player.transform.rotation = Quaternion.Euler(0, 0, -90);
            }
            else if (rb2d.velocity.x < 0)
            {
                player.transform.rotation = Quaternion.Euler(0, 0, -180);
            }
            else if (rb2d.velocity.x > 0)
            {
                player.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
    }
}
