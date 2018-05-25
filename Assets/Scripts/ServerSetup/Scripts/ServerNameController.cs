using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ServerNameController : MonoBehaviour {

	void Start () {
        ServerPlacedScript server = GameData.CurrentServer;
        InputField nameField = this.transform.Find("Server Name").GetComponent<InputField>();

        nameField.text = server.data.serverName;

        nameField.onValueChanged.AddListener(delegate
        {
            server.data.serverName = nameField.text;
        });
    }
	
	void Update () {
		
	}
}
