using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class PauseMenuManager : MonoBehaviour {
    public Canvas pauseMenu;
    public GameObject pause;
    public GameObject options;
    public GameObject player;

    public AudioSource music;

    public GameSettings gameSettings;

    public Resolution[] resolutions;

    public Toggle fullScreenToggle;
    public Dropdown resDropDown;
    public Dropdown textureQualityDropDown;
    public Dropdown vsyncDropDown;
    public Slider musicVolumeSlider;
    public Slider soundVolumeSlider;

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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ResumeGame();
        }
    }
    public void ResumeGame()
    {
        pause.SetActive(true);
        options.SetActive(false);
        GameData.move = true;
        pauseMenu.gameObject.SetActive(false);
    }

    public void SaveGame()
    {
        new Save().save(GameData.storage);
        Debug.Log("Saved");
    }

    public void LoadGame()
    {
        Debug.Log("Game Loaded");
    }

    public void ToOptions()
    {
        options.SetActive(true);
        pause.SetActive(false);
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
        Destroy(GameObject.Find("Money"));
        Destroy(GameObject.Find("Rep"));
        Destroy(GameObject.Find("Time"));
        Destroy(GameObject.Find("Systems"));
    }

    public void QuitGame()
    {
        Application.Quit();
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
        pause.gameObject.SetActive(true);
    }
    public void ToPause()
    {
        options.gameObject.SetActive(false);
        pause.gameObject.SetActive(true);
        LoadSettings();
    }
}
