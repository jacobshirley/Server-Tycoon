using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class OnLoad : MonoBehaviour {
    public GameSettings gameSettings;
    public AudioSource music;
    public Dropdown resDropDown;
    public Resolution[] res;
	// Use this for initialization
	void Awake() {
        res = Screen.resolutions;
        foreach (Resolution res in res)
        {
            resDropDown.options.Add(new Dropdown.OptionData(res.ToString()));
        }
        if (File.Exists(Application.persistentDataPath + "/gameSettings.json"))
        {
            LoadSettings();
        }
        else
        {
            LoadDefaultSettings();
            SaveSettings();
        }
    }
	
	void LoadSettings()
    {
        gameSettings = JsonUtility.FromJson<GameSettings>(File.ReadAllText(Application.persistentDataPath + "/gameSettings.json"));
        music.volume = gameSettings.musicVol;
        QualitySettings.vSyncCount = gameSettings.vsync;
        QualitySettings.masterTextureLimit = gameSettings.textureQuality;
        Screen.SetResolution(res[gameSettings.resolution].width, res[gameSettings.resolution].height, Screen.fullScreen);
        Screen.fullScreen = gameSettings.fullScreen;
    }

    public void LoadDefaultSettings()
    {
        gameSettings = new GameSettings();
        music.volume = 0.5f;
        gameSettings.musicVol = 0.5f;
        //soundVolumeSlider.value = 0.5f;
        //gameSettings set
        QualitySettings.vSyncCount = 0;
        gameSettings.vsync = 0;
        QualitySettings.masterTextureLimit = 0;
        gameSettings.textureQuality = 0;
        Screen.SetResolution(res[res.Length - 1].width, res[res.Length - 1].height, Screen.fullScreen);
        gameSettings.resolution = res.Length - 1;
        Screen.fullScreen = false;
        gameSettings.fullScreen = false;
        SaveSettings();
    }

    public void SaveSettings()
    {
        string jsonData = JsonUtility.ToJson(gameSettings, true);
        File.WriteAllText(Application.persistentDataPath + "/gameSettings.json", jsonData);
    }
}
