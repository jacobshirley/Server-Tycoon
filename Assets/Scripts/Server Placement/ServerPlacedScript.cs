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

    // Use this for initialization
    void Start () {
        light = this.GetComponentInChildren<ServerLight>();
    }

    public void Init()
    {
        data.serverName = WordsGenerator.GetJoinedWord(1);
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

    // Update is called once per frame
    void Update () {
        if (GameData.gamePaused)
            return;

        float cpuInc = (float)(this.data.def.cpu * CPU_OVERCLOCK_MAX * this.data.overclockedPercent);
        this.data.overclockedCPU = (float)System.Math.Round(this.data.def.cpu + cpuInc, 1);

        if (Time.time > lastUpdate + UPDATE_RATE)
        {
            lastUpdate = Time.time;

            UpdateClients();

            this.data.temperature = Settings.MIN_CPU_TEMP + (int)(System.Math.Exp(this.data.cpuUsage * Settings.CPU_TEMP_CURVE_MOD));
            this.data.temperature -= (int)((this.data.coolingUpgrades / (double)Settings.MAX_COOLING_UPGRADES) * Settings.MAX_TEMPERATURE_DECREASE);
            this.data.temperature += (int)(this.data.overclockedPercent * 20);
            if (this.data.temperature >= Settings.SERVER_WARNING_TEMP)
            {
                light.SetColor(Color.red);
                light.flash = false;
            } else
            {
                light.SetColor(Color.green);
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
