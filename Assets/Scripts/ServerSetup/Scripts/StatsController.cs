using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsController : MonoBehaviour {
    float lastUpdate = 0;

    // Use this for initialization
    void Start () {
        Display();
    }

    private static int FormatStorage(int storage)
    {
        if (storage > 1000)
            storage /= 1000;

        return (int)Math.Round((double)storage);
    }

    private void SetText(string child, string text)
    {
        this.transform.Find(child).GetChild(0).GetComponent<Text>().text = text;
    }

    private void SetColourWarning(string child, double value, Predicate<double> medium, Predicate<double> high)
    {
        Color colour = new Color();
        if (high.Invoke(value))
        {
            colour = Settings.RED_WARNING; //red
        }
        else if (medium.Invoke(value))
        {
            colour = Settings.ORANGE_WARNING; //orange
        }
        else
        {
            colour = Settings.GREEN_WARNING; //green
        }

        this.transform.Find(child).GetChild(0).GetComponent<Text>().color = colour;
    }

    private static float RANDOM_CPU_FLUCTUATION = (float)0.2;
    private static float RANDOM_RAM_FLUCTUATION = (float)0.3;
    private static float RANDOM_STORAGE_FLUCTUATION = (float)2.0;

    public float GetRandomFluctuation(float fluct, int decPlaces)
    {
        return (float)System.Math.Round(UnityEngine.Random.Range(-fluct, fluct), decPlaces);
    }

    void Display()
    {
        ServerPlacedScript server = GameData.CurrentServer;
        ServerDef def = server.data.def;

        float cpu = 0;
        float ram = 0;
        int storage = 0;

        if (server.data.clients.Count > 0)
        {
            cpu = System.Math.Min(System.Math.Max(server.data.cpuUsage + GetRandomFluctuation(RANDOM_CPU_FLUCTUATION, 1), 0), server.data.def.cpu);
            ram = System.Math.Min(System.Math.Max(server.data.ramUsage + GetRandomFluctuation(RANDOM_RAM_FLUCTUATION, 2), 0), server.data.def.ram);
            storage = (int)System.Math.Min(System.Math.Max(server.data.storageUsed + GetRandomFluctuation(RANDOM_STORAGE_FLUCTUATION, 1), 0), server.data.def.capacity);
        }

        SetText("Processor", cpu.ToString("N1") + " / " + server.data.overclockedCPU.ToString("N1") + " Ghz");
        SetColourWarning("Processor", server.data.cpuUsage / server.data.overclockedCPU, d => d >= 0.60, d => d >= 0.90);

        SetText("RAM", ram.ToString("N1") + " / " + def.ram + " Gb");
        SetColourWarning("RAM", server.data.ramUsage / def.ram, d => d >= 0.60, d => d >= 0.90);

        SetText("Storage", FormatStorage(storage) + " / " + def.capacity + " Gb");
        SetColourWarning("Storage", server.data.storageUsed / def.capacity, d => d >= 0.60, d => d >= 0.90);

        SetText("Security", server.data.securityLevel + "%");
        SetColourWarning("Security", server.data.securityLevel, d => d <= 50, d => d <= 35);

        SetText("Temperature", server.data.temperature + "\u2103");
        SetColourWarning("Temperature", server.data.temperature, d => d >= 55, d => d >= 70);

        SetText("Power Usage", server.data.powerUsage + " W");
        SetColourWarning("Power Usage", server.data.powerUsage, d => d >= 300, d => d >= 500);

        SetText("Cost Per Month", "\u00A3" + server.data.costPerMonth);
        this.transform.Find("Cost Per Month").GetChild(0).GetComponent<Text>().color = Settings.NEUTRAL_WARNING;
    }

    // Update is called once per frame
    void Update () {
        if (Time.time > lastUpdate + 2)
        {
            lastUpdate = Time.time;

            Display();
        }
    }
}
