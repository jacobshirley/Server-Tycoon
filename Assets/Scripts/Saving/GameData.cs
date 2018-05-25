using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour {
    public static ServerPlacedScript CurrentServer = null;

    public static GameObject player = null;

    public static List<ServerPlacedScript> servers = new List<ServerPlacedScript>();

    public static StorageClass storage = new StorageClass();
    public static bool move = true;

    public static bool menuOpen = false;

    public static bool gamePaused = false;

    // public static List<Client> clients = new List<Client>();

    public static Client ScenarioClient = null;
    public static Mail ScenarioEmail = null;

    public static List<GeneratedClient> generatedClients = new List<GeneratedClient>();
    public static GameObject ui;
}
