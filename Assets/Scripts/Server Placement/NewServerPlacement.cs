using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class NewServerPlacement : MonoBehaviour {
    public Object hoverServerPrefab;
    public Object serverPrefab;

    private Sprite currentServerSprite;
    private GameObject hoverUI;

    public ServerDef CurrentServerDef;

    public void CreateNew()
    {
        hoverUI = (GameObject)Instantiate(hoverServerPrefab);
        SetHoverPos();
        if (GameData.ui == null)
            GameData.ui = GameObject.Find("Server Placement UI");
    }

    public void OnMouseOver()
    {
    }

    public void StartServerPlacement(Sprite sprite, ServerDef def)
    {
        Debug.Log(sprite);
        currentServerSprite = sprite;
        if (hoverUI == null)
            CreateNew();
        SetActive(true);
        //hoverUI.GetComponent<ServerScript>().SetSprite(sprite);
        CurrentServerDef = def;
    }

    public void OnMouseDown()
    {
        if (hoverUI != null && hoverUI.activeSelf && hoverUI.GetComponent<ServerScript>().InValidPosition())
        {
            Place();
            SetActive(false);
        }
    }

    public void Place()
    {
        GameObject newServer = (GameObject)Instantiate(serverPrefab);
        newServer.transform.position = hoverUI.transform.position;
        newServer.GetComponent<SpriteRenderer>().sprite = currentServerSprite;
        newServer.GetComponent<ServerPlacedScript>().Init();
        newServer.GetComponent<ServerPlacedScript>().data.def = CurrentServerDef;

        GameObject.Find("Money").GetComponent<economy>().Pay(CurrentServerDef.cost);

        GameData.servers.Add(newServer.GetComponent<ServerPlacedScript>());
    }

    public void SetActive(bool state)
    {
        hoverUI.SetActive(state);
        GameData.ui.SetActive(!state);
    }

    void SetHoverPos()
    {
        var v3 = Input.mousePosition;
        v3 = Camera.main.ScreenToWorldPoint(v3);

        hoverUI.transform.position = new Vector2(v3.x, v3.y);
        hoverUI.GetComponent<ServerScript>().SetSprite(currentServerSprite);
    }

    // Use this for initialization
    void Start () {
    }

	// Update is called once per frame
	void Update () {
        if (hoverUI != null)
        {
            SetHoverPos();
        }
    }
}
