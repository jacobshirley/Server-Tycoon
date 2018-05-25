using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour, System.IComparable
{

    [TextArea(3, 10)]
    public string Text;

    public bool Immediate = true;

    public bool Seen = false;

    public bool top;
    public bool left;

    public int order = 0;

    private string[] TextParts;

	void Awake () {
        if (!Immediate && !Seen)
        {
            GameObject.Find("Tutorial").GetComponent<TutorialController>().RunTutorial(this);
        }
    }

    public void CheckSeen()
    {
        Seen = GameData.storage.tutManager.HasSeenTutorial(GetPath());
    }

    public string GetPath()
    {
        string s = this.gameObject.name;

        GameObject gO = this.gameObject;
        while (gO.transform.parent != null)
        {
            s = gO.name + s;
            gO = gO.transform.parent.gameObject;
        }

        return this.gameObject.scene.name + s;
    }
	
	void Update () {
		
	}

    public string[] GetTextParts()
    {
        if (TextParts == null)
        {
            TextParts = Text.Split(new[] { "\n\n" }, System.StringSplitOptions.None);
            for (int i = 0; i < TextParts.Length; i++)
            {
                TextParts[i] = TextParts[i].Trim();
            }

            if (TextParts.Length == 0)
            {
                TextParts = new string[] { Text };
            }
        }

        return TextParts;
    }

    public List<Tutorial> TutorialChildren()
    {
        List<Tutorial> children = new List<Tutorial>();
        List<string> Contains = new List<string>();

        Debug.Log(this.Text);

        foreach (Tutorial tut in GetComponentsInChildren<Tutorial>())
        {
            if (tut != this && !Contains.Contains(tut.Text))
            {
                Tutorial[] tuts = tut.GetComponentsInParent<Tutorial>();
                if (tuts[tuts.Length - 1] == this) //mean that tut is the only parent with it
                {
                    children.Add(tut);
                    Contains.Add(tut.Text);
                }
            }
        }

        return children;
    }

    private void OnEnable()
    {
        
    }

    void OnDisable()
    {
        if (!Immediate)
        {
            Debug.Log("TRYING TO CANCEL: " + this.gameObject.name);
            //if (GameObject.Find("Tutorial") != null)
              //  GameObject.Find("Tutorial").GetComponent<TutorialController>().CancelTutorial(this);
        }
    }

    public int CompareTo(System.Object other)
    {
        return this.order - ((Tutorial)other).order;
    }
}
