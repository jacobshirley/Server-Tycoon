using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ReadTextFromFile : MonoBehaviour {
	public TutorialPart LoadFile(int Location, string FileName){
		TutorialObject tester = JsonUtility.FromJson<TutorialObject>(File.ReadAllText(FileName));
		//Debug.Log(tester.Parts.Count);
		return tester.Parts[Location];
	}
}
