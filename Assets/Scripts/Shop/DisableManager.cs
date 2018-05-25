using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisableManager : MonoBehaviour {
    public GameObject panel;
    public void Enable()
    {
        panel.GetComponent<Image>().color = new Color(1, 1, 1, 0);
        panel.transform.SetAsFirstSibling();
    }

    public void Disable()
    {
        panel.GetComponent<Image>().color = new Color(0.8396f, 0.8396f, 0.8396f, 0.8156f);
        panel.transform.SetAsLastSibling();
    }
}
