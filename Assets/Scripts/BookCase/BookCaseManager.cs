using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BookCaseManager : MonoBehaviour {

    //public GameObject pythonCanvas, BinCanvas, LogCanvas, EncCanvas, PhishCanvas, bookShelf;
    public GameObject BinCanvas, PhishCanvas, LogCanvas, bookShelf;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /*public void Python()
    {
        Debug.Log("Python");
        pythonCanvas.gameObject.SetActive(true);
        bookShelf.gameObject.SetActive(false);
    }*/

    public void Binary()
    {
        Debug.Log("Binary");
        BinCanvas.gameObject.SetActive(true);
        bookShelf.gameObject.SetActive(false);
    }

    public void Logic()
    {
        Debug.Log("Logic");
        LogCanvas.gameObject.SetActive(true);
        bookShelf.gameObject.SetActive(false);
    }

    /*public void Encryption()
    {
        Debug.Log("Encryption");
        EncCanvas.gameObject.SetActive(true);
        bookShelf.gameObject.SetActive(false);
    }*/

    public void Phishing()
    {
        Debug.Log("Phishing");
        PhishCanvas.gameObject.SetActive(true);
        bookShelf.gameObject.SetActive(false);
    }

    public void Backfunction()
    {
        //pythonCanvas.SetActive(false);
        BinCanvas.gameObject.SetActive(false);
        //LogCanvas.gameObject.SetActive(false);
        //EncCanvas.gameObject.SetActive(false);
        PhishCanvas.gameObject.SetActive(false);
        bookShelf.gameObject.SetActive(true);
    }
}
