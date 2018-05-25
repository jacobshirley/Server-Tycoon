using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mazeController : MonoBehaviour {

    public float speed = 4f;
    public GameObject player;
    Rigidbody2D rb2d;

    private bool collected;

    void Start(){
        rb2d = GetComponent<Rigidbody2D>();
        collected = false;
        GameObject.Find("key_found").GetComponent<Image>().color = Color.red;
    }

    void FixedUpdate(){
        if (GameData.gamePaused)
            return;

        Vector2 targetVelocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        rb2d.velocity = targetVelocity * speed;
    }

    void OnTriggerEnter2D(Collider2D collision){
        if (collision.gameObject.CompareTag("key")){
            Destroy(GameObject.Find("secret_key"));
            Destroy(GameObject.Find("lightning"));
            collected = true;
            GameObject.Find("key_found").GetComponent<Image>().color = Color.green;
        }
        if(collision.gameObject.CompareTag("gate")){
          if(collected){
            Debug.Log("Collected.");
          }
          else{
            player.SetActive(false);
            this.gameObject.GetComponent<finish>().GameOver();
          }
        }
        if(collision.gameObject.CompareTag("Finish")){
          this.gameObject.GetComponent<finish>().Win();
        }
    }
}
