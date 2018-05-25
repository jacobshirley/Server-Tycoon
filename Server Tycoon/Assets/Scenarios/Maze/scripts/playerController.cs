using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerController : MonoBehaviour {

    public float speed = 4f;
    public GameObject player;
    Rigidbody2D rb2d;
    public Text promptTxt;

    public GameObject scripts;
    private bool move;
    private int serverTriggers = 0;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        move = true;
        rb2d.freezeRotation = true;
    }

    void FixedUpdate(){
      if(move){
        Vector2 targetVelocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        rb2d.velocity = targetVelocity * speed;
        if(Input.GetKeyDown(KeyCode.W)){
          player.transform.rotation = Quaternion.Euler(0,0,-270);
        }
        else if(Input.GetKeyDown(KeyCode.S)){
          player.transform.rotation = Quaternion.Euler(0,0,-90);
        }
        else if(Input.GetKeyDown(KeyCode.A)){
          player.transform.rotation = Quaternion.Euler(0,0,-180);
        }
        else if(Input.GetKeyDown(KeyCode.D)){
          player.transform.rotation = Quaternion.Euler(0,0,0);
        }
      }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("workstation"))
        {
            promptTxt.text = "Press 'e' to log on";
        }

        if (collision.gameObject.CompareTag("server"))
        {
            serverTriggers++;
            promptTxt.text = "Press 'e' to configure server";
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("workstation"))
        {
            promptTxt.text = "";
        }

        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.CompareTag("server"))
        {
            serverTriggers--;
            if (serverTriggers == 0)
                promptTxt.text = "";
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("workstation") && Input.GetKeyDown("e")){
            Debug.Log("Logged on");
            scripts.GetComponent<scenes>().loadEmail();
            move = false;
        }
        if (collision.gameObject.CompareTag("server") && Input.GetKeyDown("e")){
            Debug.Log("Open server config");
            scripts.GetComponent<scenes>().loadServer();
            move = false;
        }
    }

    public void movementActivate(){
      move = true;
    }

    public void movementDisable(){
      move = false;
    }
}
