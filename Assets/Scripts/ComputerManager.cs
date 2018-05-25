using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComputerManager : MonoBehaviour {
    public Canvas canvas;
    public GameObject player;
	public void LogOff()
    {
        canvas.gameObject.SetActive(false);
        player.SetActive(true);
    }
}
