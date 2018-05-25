using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShopManager : MonoBehaviour {


    public GameObject blue, clown, orange, red, tankUpgrade, brickBreaker, gravBall;
    public Transform fish, games, other;
    public Button backBtn;

    public Text eco;

	// Use this for initialization
	void Start () {
    backBtn.onClick.AddListener(ReturnToComp);
    foreach(string f in GameData.storage.unlockedFish){
      GameObject.Find(f).transform.Find("Disabled").GetComponent<DisableManager>().Enable();
    }

    Debug.Log(GameData.storage.unlockedGames.Count);
    foreach(string g in GameData.storage.unlockedGames){
      Destroy(GameObject.Find(g.Replace(" ", "")));
    }
	}

	// Update is called once per frame
	void Update () {
	    if (Input.GetKeyDown(KeyCode.P))
        {
            Enable(orange);
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            Disable(orange);
        }

      eco.text = "£" + GameData.storage.money.ToString();
    }

    public void Enable(GameObject x)
    {
        x.transform.GetComponentInChildren<DisableManager>().Enable();
    }

    public void Disable(GameObject x)
    {
        x.transform.GetComponentInChildren<DisableManager>().Disable();
    }


    public void ReturnToComp(){
      List<string> fishlist = this.gameObject.GetComponent<PurchaseManager>().fish;
      foreach (string f in fishlist){
        GameData.storage.fish.Add(f);
      }
      Debug.Log(GameData.storage.fish.Count);
      new Save().save(GameData.storage);
      SceneManager.LoadScene("ComputerMenu");
    }
}
