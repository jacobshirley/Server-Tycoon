using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonToggleList : MonoBehaviour {

    public Button selected = null;
    private List<Button> updated = new List<Button>();

	void Start () {
    }

    void OnTransformChildrenChanged()
    {
        
        
    }

    void Update()
    {
        if (selected != null && !selected.GetComponent<ButtonToggle>().isDown)
            selected = null;

        foreach (ButtonToggle btn in this.transform.GetComponentsInChildren<ButtonToggle>())
        {
            if (btn.isDown && selected == null)
            {
                selected = btn.GetComponent<Button>();
            }

            if (btn.isDown && selected != btn.GetComponent<Button>() && selected != null)
            {
                selected.GetComponent<ButtonToggle>().Up();
                selected = btn.GetComponent<Button>();
            }
        }
    }
}
