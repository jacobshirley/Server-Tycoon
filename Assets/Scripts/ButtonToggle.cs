using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonToggle : MonoBehaviour {
    public bool isDown = false;
    private ColorBlock colors;
    private ColorBlock downColors;

    void Awake()
    {
        colors = this.GetComponent<Button>().colors;
        downColors = colors;
    }

    // Use this for initialization
    void Start()
    {
        downColors.normalColor = colors.pressedColor;
        downColors.highlightedColor = colors.pressedColor;

        this.GetComponent<Button>().onClick.AddListener(delegate
        {
            isDown = !isDown;
        });
    }

    private void UpdateState()
    {

        if (isDown)
        {
            GetComponent<Button>().colors = downColors;
        }
        else
        {
            GetComponent<Button>().colors = colors;
        }
    }

    public void Up()
    {
        isDown = false;
        //UpdateState();
    }

    public void Down()
    {
        isDown = true;
        //UpdateState();
    }
	
	// Update is called once per frame
	void Update () {
        UpdateState();
    }
}
