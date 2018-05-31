using System;
using System.Collections.Generic;

[Serializable]
public class TutorialManager {
    public List<string> TutorialPaths = new List<string>();
    public List<bool> TutorialsSeen = new List<bool>();

    public bool HasSeenTutorial(string path)
    {
        int id = TutorialPaths.IndexOf(path);
        if (id < 0)
            return false;

        return TutorialsSeen[id];
    }

    public void Seen(string path)
    {
        int id = TutorialPaths.IndexOf(path);
        if (id < 0) {
            TutorialsSeen.Add(true);
            TutorialPaths.Add(path);
            return;
        } else
        {
            TutorialsSeen[id] = true;
        }
    }
}
