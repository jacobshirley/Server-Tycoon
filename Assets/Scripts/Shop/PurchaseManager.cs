using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchaseManager : MonoBehaviour {

    public List<string> fish;
    public List<string> games;

	  void Update () {
  		if (Input.GetKeyDown(KeyCode.M))
          {
              foreach (string f in fish)
              {
                  Debug.Log(f);
              }
          }
          if (Input.GetKeyDown(KeyCode.N))
          {
              foreach (string g in games)
              {
                  Debug.Log(g);
              }
          }
    }

    public void AddFish(string type)
    {
      Debug.Log("Added fish");
        fish.Add(type);
    }

    public void AddGame(string game)
    {
        games.Add(game);
    }
}
