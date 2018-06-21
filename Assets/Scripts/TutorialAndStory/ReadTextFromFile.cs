using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class ReadTextFromFile : MonoBehaviour
{
    [Serializable]
    public class TutorialPart
    {
        public string Text;
        public string ObjectName;
        public int ID;
        public int NextID;
    }

    [Serializable]
    class TutorialObject
    {
        public TutorialPart[] Parts;
    }

    public TutorialPart LoadFile(int Location, string FileName)
    {
        TutorialObject tester = JsonUtility.FromJson<TutorialObject>(Resources.Load<TextAsset>(FileName).text);
        return tester.Parts[Location];
    }
}