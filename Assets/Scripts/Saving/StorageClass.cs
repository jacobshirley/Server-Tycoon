using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StorageClass {

    //Player
    public Vector3 position;

    //Emails
    public List<Mail> EmailList;

    //Money Systems
    public float money;
    public float profit;

    //Time+Reputation Systems
    public int rep;
    //Need to store the data for the dates, should be able to do that in the morning, won't be hard

    public ClientManager clients = new ClientManager();

    public TutorialManager tutManager = new TutorialManager();

    //Client Lists
    public List<GeneratedClient> GeneratedClients;

    //Server List
    public List<ServerData> servers;

    public int[] date;

    public bool GameBought = false;

    public List<string> fish = new List<string>();
    public List<string> unlockedFish = new List<string>();

    public List<string> unlockedGames = new List<string>();

    public bool cheats;
}
