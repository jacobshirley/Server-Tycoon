using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour
{

    public float ballInitialVelocity = 600f;


    private Rigidbody rb;
    private bool ballInPlay;
    private Vector3 speed;
    private bool stop = false;
    private bool getSpeed = true;

    void Awake()
    {

        rb = GetComponent<Rigidbody>();

    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && ballInPlay == false && !GM.instance.paused)
        {
            transform.parent = null;
            ballInPlay = true;
            rb.isKinematic = false;
            GM.instance.instructions.SetActive(false);
            rb.AddForce(new Vector3(Random.Range(-600f, 600f), ballInitialVelocity, 0));
        }

        if (GM.instance.paused)
        {
            stop = true;
            if (getSpeed)
            {
                speed = rb.velocity;
                getSpeed = false;
            }
            rb.isKinematic = true;
            Debug.Log(speed);

        }
        else if (!GM.instance.paused && stop)
        {
            Invoke("restart", 1f);
            Debug.Log("Adding Force");
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MultiBall"))
        {
            Debug.Log("mmmmMultiBall");
            GM.instance.StartMultiBall();
            Destroy(other.gameObject);
        }
    }

    void restart()
    {
        rb.isKinematic = false;
        rb.AddForce(speed);
        stop = false;
        getSpeed = true;
    }
}