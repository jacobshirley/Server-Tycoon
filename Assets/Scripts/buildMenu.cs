using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buildMenu : MonoBehaviour {

	public GameObject open;
	public GameObject menu;

	public GameObject serverPlacement;

    public GameObject clientSelection;

	public bool trigger;

	void Start(){
		trigger = true;
		open.SetActive(true);
		menu.SetActive(false);
	}

	public void toggleMenu(){

		trigger = !trigger;

		//open.SetActive(trigger);
		menu.SetActive(!trigger);

		setActive();
	}

    void Update()
    {
        if (GameData.servers.Count > 0)
        {
            clientSelection.SetActive(true);
        } else
        {
            clientSelection.SetActive(false);
        }
    }

	public void setActive(){
		//serverPlacement.GetComponent<NewServerPlacement>().SetActive(!trigger);
	}
}
