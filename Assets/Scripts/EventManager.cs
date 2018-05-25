using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour {

    public delegate void EventListener(System.Object message);

    public Dictionary<string, List<EventListener>> listeners = new Dictionary<string, List<EventListener>>();

	// Use this for initialization
	void Start () {
		
	}

    public void Listen(string eventName, EventListener listener)
    {
        List<EventListener> ls;
        if (listeners.TryGetValue(eventName, out ls))
        {
            ls.Add(listener);
        } else
        {
            List<EventListener> list = new List<EventListener>();
            list.Add(listener);
            listeners.Add(eventName, list);
        }
    }

    public void Unlisten(string eventName, EventListener listener)
    {
        List<EventListener> ls;
        if (listeners.TryGetValue(eventName, out ls))
        {
            ls.Remove(listener);
        }
    }

    public void Trigger(string eventName, System.Object message) {
        List<EventListener> ls;
        if (listeners.TryGetValue(eventName, out ls)) {
            foreach (EventListener l in ls)
            {
                l.Invoke(message);
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
