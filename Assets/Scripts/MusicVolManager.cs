using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MusicVolManager : MonoBehaviour {
    public AudioSource music;
    public GameSettings gameSettings;
	// Use this for initialization
	void Start () {
        gameSettings = JsonUtility.FromJson<GameSettings>(File.ReadAllText(Application.persistentDataPath + "/gameSettings.json"));
        music.volume = gameSettings.musicVol;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
