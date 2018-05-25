using System;
[Serializable]
public class TutorialPart{
	public string Text;
    public string Trigger;
    public string Path;
	public int ID;
	public int NextID;
    public bool Seen;

    public TutorialPart[] Children;
}
