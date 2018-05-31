using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour {

    private Tutorial Current;
    private int CurrentTextIndex = 0;
    private Stack<Tutorial> stack = new Stack<Tutorial>();
    private GameObject CurrentObj = null;

    private bool paused = false;
    private bool inTutorial = false;

    public RectTransform panel;
    public RectTransform content;

    public Image outline;
    public Image overlay;

    public Text textArea;

    private PriorityQueue<Tutorial> tutorialQueue = new PriorityQueue<Tutorial>();

    void Start () {
        //this.panel.gameObject.SetActive(false);

    }

    public void SetPosition(RectTransform rect, bool left, bool top)
    {
        Vector2 min = panel.anchorMin;
        Vector2 max = panel.anchorMax;
        Vector2 pivot = panel.pivot;

        if (left)
        {
            min.x = max.x = pivot.x = 0;
        }
        else
        {
            min.x = max.x = pivot.x = 1;
        }

        if (top)
        {
            min.y = max.y = pivot.y = 1;
        } else
        {
            min.y = max.y = pivot.y = 0;
        }

        rect.anchorMin = min;
        rect.anchorMax = max;
        rect.pivot = pivot;
    }

    public void Highlight(GameObject gO)
    {
        if (gO.GetComponent<Canvas>() == null)
        {
            Canvas can = gO.AddComponent<Canvas>();
            can.overrideSorting = true;
            can.sortingOrder = 31;
        }

        overlay.enabled = true;

        EventSystem.current.SetSelectedGameObject(this.gameObject);
    }

    public void SetText(string text)
    {
        textArea.text = text;
    }

    public void RunTutorial(Tutorial tutorial)
    {
        if (!tutorial.Seen)
        {

            tutorialQueue.Enqueue(tutorial);
        }
    }

    public void CancelTutorial(Tutorial tutorial)
    {
        if (Current == tutorial)
        {
            inTutorial = false;
        
        } else
        {
            tutorialQueue.Remove(tutorial);
        }

        StartTutorial();
    }

    void StartTutorial()
    {
        if (tutorialQueue.Count > 0)
        {
            Tutorial tutorial =  tutorialQueue.Dequeue();

            paused = false;

            inTutorial = true;

            Current = tutorial;
            stack.Clear();
            stack.Push(tutorial);
        } else
        {
            panel.gameObject.SetActive(false);
            overlay.enabled = false;
            Current = null;
            CurrentObj = null;
            inTutorial = false;
            stack.Clear();

            GameData.gamePaused = false;
        }
    }

	void Update () {

        if (paused)
        {
            if (inTutorial && (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)))
            {
                if (++CurrentTextIndex < Current.GetTextParts().Length)
                {
                    SetText(Current.GetTextParts()[CurrentTextIndex]);
                }
                else
                {
                    paused = false;

                    while (stack.Count >= 0)
                    {
                        

                        foreach (Tutorial part in Current.TutorialChildren())
                        {
                            if (!part.Seen)
                            {
                                Debug.Log(part.Text);
                                stack.Push(Current);
                                Current = part;
                                //stack.Push(part);

                                return;
                            }
                        }

                        if (!Current.Seen)
                        {
                            GameData.storage.tutManager.Seen(Current.GetPath());
                        }

                        Current.Seen = true;
                        Current = stack.Count == 0 ? null : stack.Pop();

                        

                        if (Current == null)
                            break;
                    }

                    if (Current == null)
                    {
                        Destroy(CurrentObj.GetComponent<Canvas>());
                        overlay.enabled = false;
                        panel.gameObject.SetActive(false);

                        inTutorial = false;
                        GameData.gamePaused = false;
                        return;
                    }
                }
            }
        }
        else
        {
            if (!inTutorial)
            {
                StartTutorial();

                if (Current != null)
                {
                    Current.CheckSeen();
                    if (Current.Seen)
                    {
                        inTutorial = false;
                        Current = null;
                    } else
                    {
                        panel.gameObject.SetActive(true);
                    }
                }
            }

            if (inTutorial && !Current.Seen)
            {
                if (Current == null || !Current.isActiveAndEnabled)
                {
                    inTutorial = false;
                    Current = null;
                    return;
                }

                GameObject gO = Current.gameObject;
                paused = true;

                if (CurrentObj != null)
                {
                    Destroy(CurrentObj.GetComponent<Canvas>());
                }

                panel.gameObject.SetActive(true);
                CurrentTextIndex = 0;
                SetText(Current.GetTextParts()[CurrentTextIndex]);
                EventSystem.current.SetSelectedGameObject(this.gameObject);
                Highlight(gO);
                SetPosition(panel, Current.left, Current.top);
                SetPosition(content, Current.left, Current.top);

                GameData.gamePaused = true;

                CurrentObj = gO;
            }
        }
    }
}
