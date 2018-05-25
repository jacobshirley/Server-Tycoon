using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour {

    public Image image;
    public Text title;
    public Text price;
    public Button buttonBtn;
    public GameObject button;
    private string type;
    private Transform itemParent;

    void Start () {
        type = title.GetComponent<Text>().text;
        itemParent = transform.parent.gameObject.transform;
    }

    public void SetButton()
    {
        switch (itemParent.name)
        {
            case "Fish":
                GameObject.Find("ShopManager").GetComponent<PurchaseManager>().AddFish(type);
                break;
            case "Games":
                GameData.storage.unlockedGames.Add(type);
                break;
            default:
                break;
        }



    }

}
