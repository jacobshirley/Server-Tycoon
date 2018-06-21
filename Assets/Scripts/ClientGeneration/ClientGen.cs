using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class ClientGen {

    public const int EASY = 0;
    public const int MEDIUM = 1;
    public const int HARD = 2;

    public Client GenClient(int difficulty) {
        Debug.Log(difficulty);

 		string reqType = Settings.CLIENT_TYPES[Random.Range(0, Settings.CLIENT_TYPES.Length)];
        string reqName = WordsGenerator.ToTitleCase(WordsGenerator.GetInterestingWord(1)) + Settings.CLIENT_NAME_SUFFIXES[(int)(Random.value * Settings.CLIENT_NAME_SUFFIXES.Length)];

        int[] reqPorts = null;
        int reqStorage = 0;
        float reqRam = 0;
        float reqPPower = 2.13f;

        if (difficulty == EASY)
        {
            reqPPower = Random.Range((float)0.3, (float)1.2);
            reqPorts = RandomPorts(1);
            reqRam = Random.Range((float) 0.5, 2);
            reqStorage = Settings.CLIENT_EASY_STORAGE[(int)(Random.value * Settings.CLIENT_EASY_STORAGE.Length)];
        } else if (difficulty == MEDIUM)
        {
            reqPPower = Random.Range((float)1.2, (float)2.5);
            reqPorts = RandomPorts(Random.Range(2, Settings.CLIENT_PORTS.Length));
            reqRam = Random.Range((float)2, 8);
            reqStorage = Settings.CLIENT_MEDIUM_STORAGE[(int)(Random.value * Settings.CLIENT_MEDIUM_STORAGE.Length)];
        } else if (difficulty == HARD)
        {
            reqPPower = Random.Range((float)2.5, (float)5.0);
            reqPorts = RandomPorts(Random.Range(3, Settings.CLIENT_PORTS.Length));
            reqRam = Random.Range((float)6, 16);
            reqStorage = Settings.CLIENT_HARD_STORAGE[(int)(Random.value * Settings.CLIENT_HARD_STORAGE.Length)];
        }

		Client client = new Client();

        client.difficulty = difficulty;
        client.satisfaction = 50;
		client.reqType = reqType;
		client.reqName = reqName;
		client.reqPPower = (float)System.Math.Round((double)reqPPower, 1);
        client.reqRam = (float)System.Math.Round((double)reqRam, 1);
        client.reqPorts = reqPorts;
        client.reqStorage = reqStorage;

        double reqPay = getCost(client);

        client.reqPay = (((int)System.Math.Ceiling(reqPay / 100.0)) * 100) + 200;

        client.EmailFreq = Random.Range(Settings.MIN_EMAIL_FREQ, Settings.MAX_EMAIL_FREQ);

		return client;
 	}

    private int[] RandomPorts(int size)
    {
        int[] randPorts = new int[size];
        List<int> options = new List<int>();
        for (int i = 0; i < Settings.CLIENT_PORTS.Length; i++)
        {
            options.Add(i);
        }

        for (int i = 0; i < size; i++)
        {
            int val = (int)(Random.value * (options.Count - 1));
            randPorts[i] = Settings.CLIENT_PORTS[options[val]];
            options.Remove(val);
        }

        return randPorts;
    }

	private double getCost(Client client){
        double cost = 0;

        cost += Settings.CLIENT_PRICE_PER_HERT * (client.reqPPower / 0.01);

        cost += Settings.CLIENT_PRICE_PER_GB_STORAGE * client.reqStorage;

        cost += Settings.CLIENT_PRICE_PER_PORT * client.reqPorts.Length;

        cost += Settings.CLIENT_PRICE_PER_GB_RAM * client.reqRam;

        return System.Math.Round((double)cost, 2);
    }
}
