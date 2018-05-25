using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TutorialIsland : MonoBehaviour {
    private TutorialPart Current;
    private Stack<TutorialPart> stack = new Stack<TutorialPart>();
    private GameObject CurrentObj = null;

    private bool paused = false;

    public Image outline;
    public Image overlay;

    

    // Use this for initialization
    void Start () {
        Current = JsonUtility.FromJson<TutorialPart>(Resources.Load<TextAsset>("tutorial").text);

        stack.Push(Current);
    }

    bool AllChildrenSeen()
    {
        foreach (TutorialPart child in Current.Children)
            if (!child.Seen)
                return false;

        return true;
    }
	
	// Update is called once per frame
	void Update () {
        if (stack.Count == 0)
        {
            Destroy(CurrentObj.GetComponent<Canvas>());
            Destroy(CurrentObj.GetComponent<Outline>());

            overlay.enabled = false;

            Destroy(this);
            return;
        }

        if (paused)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                paused = false;

                foreach (TutorialPart part in Current.Children)
                {
                    if (!part.Seen)
                    {
                        Current = part;
                        stack.Push(Current);
                        return;
                    }
                }

                Current.Seen = true;
            }
        } else {
            if (!Current.Seen)
            {
                string[] objectPath = Current.Path.Split('>');
                GameObject gO = CurrentObj == null ? GameObject.Find(objectPath[0]) : CurrentObj.transform.Find(objectPath[0]).gameObject;

                for (int i = 1; i < objectPath.Length; i++)
                {
                    gO = gO.transform.Find(objectPath[i]).gameObject;
                }

                if (gO.activeSelf)
                {
                    paused = true;

                    if (CurrentObj != null)
                    {
                        Destroy(CurrentObj.GetComponent<Canvas>());
                        Destroy(CurrentObj.GetComponent<Outline>());
                    }

                    this.transform.Find("Tutorial Text").GetComponent<Text>().text = Current.Text;

                    EventSystem.current.SetSelectedGameObject(this.gameObject);

                    Canvas can = gO.AddComponent<Canvas>();
                    can.overrideSorting = true;
                    can.sortingOrder = 31;
                    overlay.enabled = true;
                    Vector2 size = gO.GetComponent<RectTransform>().sizeDelta;

                    outline.transform.SetParent(gO.transform, false);
                    outline.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
                    outline.transform.GetComponent<RectTransform>().sizeDelta = size;

                    Outline o2 = gO.AddComponent<Outline>();
                    o2.useGraphicAlpha = false;
                    o2.effectColor = Color.red;
                    o2.effectDistance = new Vector2(5, 5);

                    CurrentObj = gO;
                }
            } else
            {
                Current = stack.Pop();
            }
        }
    }
}
