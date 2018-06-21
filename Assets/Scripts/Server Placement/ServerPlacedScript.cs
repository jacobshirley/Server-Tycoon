using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ServerPlacedScript : MonoBehaviour {

    private static double UPDATE_RATE = 2; //in seconds

    private const float CPU_OVERCLOCK_MAX = (float)0.6;

    private double lastUpdate = 0;

    public ServerData data;
    private new ServerLight light;
    public ParticleSystem fire;
    public ParticleSystem smoke;

    public ProgressBar healthBar;

    // Use this for initialization
    void Start () {
        RectTransform rect = healthBar.transform.parent.gameObject.GetComponent<RectTransform>();
        rect.anchoredPosition = new Vector2(0, (float) 0.7);

        light = this.GetComponentInChildren<ServerLight>();
        //fire = this.transform.Find("Fire").GetComponent<ParticleSystem>();
        //smoke = this.transform.Find("Smoke").GetComponent<ParticleSystem>();
    }

    public void Init()
    {
        data.serverName = WordsGenerator.GetJoinedWord(1);
        data.health = (float) 100.0;
    }

    public void UpdateClients()
    {
        if (GameData.gamePaused)
            return;

        this.data.cpuUsage = 0;
        this.data.ramUsage = 0;
        this.data.storageUsed = 0;

        foreach (int clId in data.clients)
        {
            Client cl = GameData.storage.clients.GetClient(clId);
            
            cl.clientCPU = cl.reqPPower;
            cl.clientRAM = cl.reqRam;
            cl.clientStorage = cl.reqStorage;

            this.data.cpuUsage += cl.clientCPU;
            this.data.ramUsage += cl.clientRAM;
            this.data.storageUsed += cl.clientStorage;
        }
    }

    public float CPULeft()
    {
        return data.def.cpu - this.data.cpuUsage;
    }

    public float RAMLeft()
    {
        return data.def.ram - this.data.ramUsage;
    }

    public float StorageLeft()
    {
        return data.def.capacity - this.data.storageUsed;
    }

    public bool HasPortsOpen(int[] ports)
    {
        if (ports == null)
            return false;

        foreach (int port in ports)
            if (!data.portsOpen.Contains(port))
                return false;

        return true;   
    }

    private void SetSmoke(Color color, float alpha)
    {
        smoke.gameObject.SetActive(true);

        var main = smoke.main;
        var startColor = main.startColor;

        startColor.color = new Color(color.r, color.g, color.b, alpha);

        main.startColor = startColor;
        healthBar.gameObject.SetActive(true);
    }

    private void HideSmoke()
    {
        smoke.gameObject.SetActive(false);
        healthBar.gameObject.SetActive(false);
    }

    private void ShowFire()
    {
        fire.gameObject.SetActive(true);
    }

    private void HideFire()
    {
        fire.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update () {
        if (GameData.gamePaused)
            return;

        float cpuInc = (float)(this.data.def.cpu * CPU_OVERCLOCK_MAX * this.data.overclockedPercent);
        this.data.overclockedCPU = (float)System.Math.Round(this.data.def.cpu + cpuInc, 1);

        healthBar.value = data.health;

        if (data.health >= 50)
        {
            healthBar.progressColour = Settings.GREEN_WARNING;
        } else if (data.health >= 15)
        {
            healthBar.progressColour = Settings.ORANGE_WARNING;
        } else
        {
            healthBar.progressColour = Settings.RED_WARNING;
        }

        if (data.health <= 0)
        {
            GameData.servers.Remove(this);

            EventManager evManager = GameObject.Find("Time").GetComponent<EventManager>();

            evManager.Trigger("RemoveServer", this);

            foreach (int client in this.data.clients)
            {
                Client cl = GameData.storage.clients.RemoveClient(client);
                evManager.Trigger("RemoveClient", cl);
            }

            Destroy(this.gameObject);
            return;
        }

        if (Time.time > lastUpdate + UPDATE_RATE)
        {
            lastUpdate = Time.time;

            UpdateClients();

            this.data.temperature = Settings.MIN_CPU_TEMP + (int)(System.Math.Exp(this.data.cpuUsage * Settings.CPU_TEMP_CURVE_MOD));
            this.data.temperature -= (int)((this.data.coolingUpgrades / (double)Settings.MAX_COOLING_UPGRADES) * Settings.MAX_TEMPERATURE_DECREASE);
            this.data.temperature += (int)(this.data.overclockedPercent * Settings.OVERCLOCK_MAX_TEMP_INCREASE);

            if (this.data.temperature >= Settings.SERVER_HIGH_TEMP)
            {
                data.health -= (float)Settings.SERVER_HEALTH_DECREASE * 2;

                SetSmoke(new Color(0, 0, 0), (float)0.1);
                ShowFire();

                light.SetColor(Settings.RED_WARNING);
                light.flash = false;
            } else if (this.data.temperature >= Settings.SERVER_MEDIUM_TEMP)
            {
                data.health -= (float)Settings.SERVER_HEALTH_DECREASE;

                SetSmoke(new Color((float)0.5, (float)0.5, (float)0.5), (float)0.04);
                HideFire();

                light.SetColor(Settings.ORANGE_WARNING);
                light.flash = true;
            } else
            {
                HideFire();
                HideSmoke();

                light.SetColor(Settings.GREEN_WARNING);
                light.flash = true;
            }

            this.data.securityLevel = Settings.STARTING_SECURITY_LEVEL;
            this.data.securityLevel += (int)((this.data.securityUpgrades / (double)Settings.MAX_SECURITY_UPGRADES) * Settings.MAX_SECURITY_INCREASE);
            this.data.securityLevel -= this.data.portsOpen.Count * 5; //take off the ports security decrease

            this.data.powerUsage = (int)(this.data.temperature * 4.5);
            this.data.costPerMonth = (int)(this.data.powerUsage * 1.5);

            this.data.position = this.transform.position;
        }
	}
}
