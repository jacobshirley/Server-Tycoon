using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMovement : MonoBehaviour {

    public int moveSpeed = 140;  //per second
    public int moveSpeedY = 50;  //per second
    public int computerDirection;
    public Transform diamond;
    public GameObject fish;
    private Transform Back;
    private Transform Mid;
    private Transform Front;
    Vector3 moveDirection = new Vector3(-1, 0, 0);
    Vector3 moveDirectionY = new Vector3(0, 0.5f, 0);
    private int counter;
    private int maxMove = 50;
    bool movingLeft = false;
    bool normalLocation = true;

    private void Start()
    {

        Back = GameObject.Find("Back").transform;
        Mid = GameObject.Find("Mid").transform;
        Front = GameObject.Find("Front").transform;
    }

    void Update()
    {
        Vector3 eulerAngles = transform.rotation.eulerAngles;
        if (fish.transform.localEulerAngles == new Vector3(0, 0, 0))
        {
            movingLeft = true;
        }
        else
        {
            movingLeft = false;
        }
        if (movingLeft && transform.position.x <= -150)
        {
            fish.transform.Rotate(0, 180, 0);
            if (Random.Range(0, 100) == 50)
            {
                fish.transform.SetParent(Front);
                fish.transform.localScale = new Vector3(1.7f, 1.7f, 1.7f);
            }
            else if (fish.transform.parent.name.Equals("Front"))
            {
                fish.transform.SetParent(Back);
                fish.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
            }
            else if (fish.transform.parent.name.Equals("Back"))
            {
                fish.transform.SetParent(Mid);
                fish.transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                fish.transform.SetParent(Back);
                fish.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
            }
            movingLeft = false;
        }

        if (!movingLeft && transform.position.x >= Screen.width+150)
        {
            fish.transform.Rotate(0, -180, 0);
            if (Random.Range(0, 100) == 50)
            {
                fish.transform.SetParent(Front);
                fish.transform.localScale = new Vector3(1.7f, 1.7f, 1.7f);
            }
            else if (fish.transform.parent.name.Equals("Front"))
            {
                fish.transform.SetParent(Back);
                fish.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
            }
            else if (fish.transform.parent.name.Equals("Back"))
            {
                fish.transform.SetParent(Mid);
                fish.transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                fish.transform.SetParent(Back);
                fish.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
            }
            movingLeft = true;
        }

        transform.Translate(moveSpeed * Time.deltaTime * moveDirection);
        transform.Translate(moveSpeedY * Time.deltaTime * moveDirectionY);
        
        if (normalLocation)
        {
            counter++;
            if (counter == maxMove)
            {
                 moveDirectionY.y *= -1;
                 maxMove = Random.Range(40, 150);
                 counter = 0;

            }
        }
       
        if (fish.transform.position.y < Screen.height*0.25)
        {
            moveDirectionY.y = 1;
            normalLocation = false;
        }
        else if (fish.transform.position.y > Screen.height*0.8)
        {
            moveDirectionY.y = -1;
            normalLocation = false;
        }
        else
        {
            normalLocation = true;
        }
    }
}

