using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CheatsHandler : MonoBehaviour {

	void Start () {
		if(GameData.storage.cheats == true){
        Transform cheats = this.transform.Find("Cheats");
        this.transform.Find("Load").GetComponent<Button>().onClick.AddListener(delegate
        {
            InputField cheatText = cheats.GetComponent<InputField>();
            string cheat = cheatText.text.ToLower();
            cheatText.text = "";

            switch (cheat)
            {
                case "motherlode":
                    GameObject.Find("Money").GetComponent<economy>().SetMoney(999999);
                    break;
                case "nomoney":
                    GameObject.Find("Money").GetComponent<economy>().SetMoney(0);
                    break;
                case "rep":
                    GameObject.Find("Rep").GetComponent<reputation>().SetRep(100);
                    break;
								case "42":
										new Save().save(GameData.storage);
										SceneManager.LoadScene("BOOMTOWN");
										break;
            }

        });
			}

			else{
				this.gameObject.transform.localScale = new Vector3(0,0,0);
			}
	}
}
