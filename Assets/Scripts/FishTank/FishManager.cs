using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FishManager : MonoBehaviour {

    public Transform Back;
    public Transform Mid;
    public Transform Front;
    public GameObject OrangeFish, BlueFish, ClownFish, RedFish;
    public GameObject info;
    private float time = 2;

    void Start () {
        ShowInfo();
        List<string> fish = GameData.storage.fish;
        for(int i = 0; i < fish.Count; i++){
          fish[i] = fish[i].Replace(" ", "");
          Debug.Log(fish[i]);
        }
        foreach(string f in fish){
          if(f.Equals(OrangeFish.name)){MakeFish(OrangeFish);}
          if(f.Equals(BlueFish.name)){MakeFish(BlueFish);}
          if(f.Equals(ClownFish.name)){MakeFish(ClownFish);}
          if(f.Equals(RedFish.name)){MakeFish(RedFish);}
        }
	   }

	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
          new Save().save(GameData.storage);
            SceneManager.LoadScene("game");
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            MakeFish(OrangeFish);
            GameData.storage.fish.Add("OrangeFish");
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            MakeFish(BlueFish);
        }
        else if (Input.GetKeyDown(KeyCode.Semicolon))
        {
            MakeFish(ClownFish);
        }
        else if (Input.GetKeyDown(KeyCode.LeftBracket))
        {
            MakeFish(RedFish);
        }
        else if (Input.GetKeyDown(KeyCode.M))
        {
            Debug.Log(Screen.width);
        }
        time -= Time.deltaTime;
        if (time <= 0)
        {
            HideInfo();
        }
    }

    void MakeFish(GameObject fish)
    {
        GameObject newFish = (GameObject)GameObject.Instantiate(fish);
        newFish.transform.SetParent(Mid);
        newFish.transform.localScale = new Vector3(1, 1, 1);
        newFish.transform.position = new Vector3(Random.Range(Screen.width*0.15f, Screen.width*0.85f), Random.Range(Screen.height*0.25f, Screen.height*0.85f), 0);
        newFish.transform.GetComponent<FishMovement>().moveSpeed = Random.Range(80, 150);
        int rotation = Random.Range(0, 2);
        if (rotation == 1)
        {
            newFish.transform.Rotate(0, 180, 0);
        }
    }

    void ShowInfo()
    {
        info.SetActive(true);
    }

    void HideInfo()
    {
        info.SetActive(false);
    }
}
