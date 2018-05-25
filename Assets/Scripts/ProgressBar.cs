using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class ProgressBar : MonoBehaviour {
    public float value = 0;
    public Color progressColour;

    private Image progressImage;
    private RectTransform progressSize;

    void Start () {
        progressSize = this.transform.GetChild(0).GetComponent<RectTransform>();
        progressImage = this.transform.GetChild(0).GetComponent<Image>();
    }
	
	void Update () {
        float thisWidth = this.GetComponent<RectTransform>().sizeDelta.x;
        RectTransform trans = progressSize;
        trans.sizeDelta = new Vector2((float)(thisWidth * (value / 100.0)), trans.sizeDelta.y);

        progressImage.color = progressColour;
	}
}
