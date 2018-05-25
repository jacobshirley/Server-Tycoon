using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;

public class OptionsMenuManager : MonoBehaviour {
    public GameObject main;
    public GameObject options;
    public AudioSource music;

    public Toggle fullScreenToggle;
    public Dropdown resDropDown;
    public Dropdown textureQualityDropDown;
    public Dropdown vsyncDropDown;
    public Slider musicVolumeSlider;
    public Slider soundVolumeSlider;

    public Resolution[] resolutions;

    public GameSettings gameSettings;

    private float time = 0.5f;
    public TextMeshProUGUI title;
    public float initTime = 0.1f;
    private bool init = true;
    private string[] titleArray = { "O", "p", "t", "i", "o", "n", "s"};
    private string titleString = "";
    private int titleCount = 0;

    void Update()
    {
        if (init)
        {

            initTime -= Time.deltaTime;
            if (initTime <= 0)
            {
                initTime = 0.1f;
                titleString += titleArray[titleCount];
                title.text = titleString + "_";
                titleCount++;
            }
            if (title.text.Equals("Options_"))
            {
                init = false;
            }
        }
        else
        {
            time -= Time.deltaTime;
            if (time <= 0)
            {
                time = 0.5f;
                if (title.text.Equals("Options"))
                {
                    title.text = "Options_";
                }
                else
                {
                    title.text = "Options";
                }
            }
        }
    }

    private void OnEnable()
    {
        

        fullScreenToggle.onValueChanged.AddListener(delegate { OnFullScreenToggle(); });
        resDropDown.onValueChanged.AddListener(delegate { OnResolutionChange(); });
        textureQualityDropDown.onValueChanged.AddListener(delegate { OnTextureQualityChange(); });
        vsyncDropDown.onValueChanged.AddListener(delegate { OnVSyncChange(); });
        musicVolumeSlider.onValueChanged.AddListener(delegate { OnMusicVolumeChange(); });
        soundVolumeSlider.onValueChanged.AddListener(delegate { OnSoundVolumeChange(); });

        resolutions = Screen.resolutions;
        resDropDown.ClearOptions();
        foreach (Resolution res in resolutions)
        {
            resDropDown.options.Add(new Dropdown.OptionData(res.ToString()));
        }

        LoadSettings();
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
        vsyncDropDown.value = gameSettings.vsync;
        textureQualityDropDown.value = gameSettings.textureQuality;
        resDropDown.value = gameSettings.resolution;
        fullScreenToggle.isOn = gameSettings.fullScreen;

    }

    

    public void Apply()
    {
        SaveSettings();
        options.gameObject.SetActive(false);
        main.gameObject.SetActive(true);
        GameObject.Find("OptionsBtn").GetComponent<Button>().Select();
    }
    public void Back()
    {
        LoadSettings();
        options.gameObject.SetActive(false);
        main.gameObject.SetActive(true);
        GameObject.Find("OptionsBtn").GetComponent<Button>().Select();
    }
}
