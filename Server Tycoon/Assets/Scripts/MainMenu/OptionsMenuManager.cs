using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class OptionsMenuManager : MonoBehaviour {
    public GameObject main;
    public GameObject options;
    public AudioSource music;

    public Toggle fullScreenToggle;
    public Dropdown resDropDown;
    public Dropdown textureQualityDropDown;
    public Dropdown aaDropDown;
    public Dropdown vsyncDropDown;
    public Slider musicVolumeSlider;
    public Slider soundVolumeSlider;

    public Resolution[] resolutions;

    public GameSettings gameSettings;

    private void OnEnable()
    {
        gameSettings = new GameSettings();

        fullScreenToggle.onValueChanged.AddListener(delegate { OnFullScreenToggle(); });
        resDropDown.onValueChanged.AddListener(delegate { OnResolutionChange(); });
        textureQualityDropDown.onValueChanged.AddListener(delegate { OnTextureQualityChange(); });
        aaDropDown.onValueChanged.AddListener(delegate { OnAAChange(); });
        vsyncDropDown.onValueChanged.AddListener(delegate { OnVSyncChange(); });
        musicVolumeSlider.onValueChanged.AddListener(delegate { OnMusicVolumeChange(); });
        soundVolumeSlider.onValueChanged.AddListener(delegate { OnSoundVolumeChange(); });

        resolutions = Screen.resolutions;
        
        foreach (Resolution res in resolutions)
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
        }
    }
    void Start()
    {
        if (File.Exists(Application.persistentDataPath + "/gameSettings.json"))
        {
            LoadSettings();
        }
        else
        {
            LoadDefaultSettings();
        }
     
    }

    void Awake()
    {
        if (File.Exists(Application.persistentDataPath + "/gameSettings.json"))
        {
            LoadSettings();
        }
        else
        {
            LoadDefaultSettings();
        }

    }
    void FixedUpdate()
    {
       
    }

    public void OnFullScreenToggle()
    {
        gameSettings.fullScreen = fullScreenToggle.isOn;
        Screen.fullScreen = fullScreenToggle.isOn;
        Debug.Log("FullScreen Toggled");
    }

    public void OnResolutionChange()
    {
        Screen.SetResolution(resolutions[resDropDown.value].width, resolutions[resDropDown.value].height, Screen.fullScreen);
        gameSettings.resolution = resDropDown.value;
        Debug.Log("Res Changed");
    }

    public void OnTextureQualityChange()
    {
        gameSettings.textureQuality = QualitySettings.masterTextureLimit = textureQualityDropDown.value;
        Debug.Log("Textures Changed");
    }

    public void OnAAChange()
    {
        gameSettings.aa = QualitySettings.antiAliasing = (int)Mathf.Pow(2, aaDropDown.value);
    }

    public void OnVSyncChange()
    {
        gameSettings.vsync = QualitySettings.vSyncCount = vsyncDropDown.value;
    }

    public void OnMusicVolumeChange()
    {
        gameSettings.musicVol = music.volume = musicVolumeSlider.value;
    }

    public void OnSoundVolumeChange()
    {
        gameSettings.soundVol = soundVolumeSlider.value;
    }

    public void SaveSettings()
    {
        string jsonData = JsonUtility.ToJson(gameSettings, true);
        File.WriteAllText(Application.persistentDataPath + "/gameSettings.json", jsonData);
    }

    public void LoadSettings()
    {
        gameSettings = JsonUtility.FromJson<GameSettings>(File.ReadAllText(Application.persistentDataPath + "/gameSettings.json"));
        musicVolumeSlider.value = gameSettings.musicVol;
        soundVolumeSlider.value = gameSettings.soundVol;
        aaDropDown.value = gameSettings.aa;
        vsyncDropDown.value = gameSettings.vsync;
        textureQualityDropDown.value = gameSettings.textureQuality;
        resDropDown.value = gameSettings.resolution;
        fullScreenToggle.isOn = gameSettings.fullScreen;

    }

    public void LoadDefaultSettings()
    {
        musicVolumeSlider.value = 0.5f;
        soundVolumeSlider.value = 0.5f;
        aaDropDown.value = 1;
        vsyncDropDown.value = 0;
        textureQualityDropDown.value = 1;
        Resolution[] res = Screen.resolutions;
        resDropDown.value = res.Length - 1;
        fullScreenToggle.isOn = true;
    }

    public void Apply()
    {
        SaveSettings();
        options.gameObject.SetActive(false);
        main.gameObject.SetActive(true);
    }
    public void Back()
    {
        options.gameObject.SetActive(false);
        main.gameObject.SetActive(true);
        LoadSettings();
    }
}
