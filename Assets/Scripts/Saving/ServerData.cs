using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ServerData{
    public Vector3 position;

    public string serverName;
    public float cpuUsage;
    public float ramUsage;
    public List<int> portsOpen;
    public int securityLevel;
    public int storageUsed;
    public int temperature;
    public int powerUsage;
    public float overclockedPercent;
    public int costPerMonth;

    public float health;

    public List<int> clients = new List<int>();

    //Upgrades
    public float overclockedCPU; // between 0 and 1 (0% to 100%)
    public int securityUpgrades; // between 0 and 3
    public int coolingUpgrades; // between 0 and 3

    //Original server definition
    public ServerDef def;
}
