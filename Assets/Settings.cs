using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Settings {

    //----Emails----

    public const int MIN_EMAIL_FREQ = 20;
    public const int MAX_EMAIL_FREQ = 30;

    //----UI----

    //UI warning colours
    public static Color RED_WARNING = new Color(204f / 255f, 27f / 255f, 1f / 255f);
    public static Color ORANGE_WARNING = new Color(226f / 255f, 112f / 255f, 14f / 255f);
    public static Color GREEN_WARNING = new Color(39f / 255f, 202f / 255f, 54f / 255f);
    public static Color NEUTRAL_WARNING = new Color(32f / 255f, 103f / 255f, 217f / 255f);

    //UI client loading
    public const int CLIENT_LOAD_DELAY = 10; //in seconds

    //----Clients----

    public const int CLIENT_SATISFACTION_INCREASE = 20;
    public const int CLIENT_SATISFACTION_DECREASE = 20;

    //Costs
    public const int CLIENT_PRICE_PER_HERT = 1 / 10;
    public const int CLIENT_PRICE_PER_GB_STORAGE = 1 / 5;
    public const int CLIENT_PRICE_PER_GB_RAM = 100;
    public const int CLIENT_PRICE_PER_PORT = 100;

    //Generation
    public static string[] CLIENT_TYPES = { "Game", "Business", "Personal", "Medical", "Booking" };
    public static string[] CLIENT_NAME_SUFFIXES = { ", Inc.", ", Corp.", ", Ltd.", ", NGO.", ", LLC.", ", Co.", ", & Sons" }; //Remember to randomly generate these names
    public static int[] CLIENT_PORTS = { 20, 21, 22, 25, 80 };
    public static int[] CLIENT_EASY_STORAGE = { 50, 100, 200 };
    public static int[] CLIENT_MEDIUM_STORAGE = { 300, 550, 700 };
    public static int[] CLIENT_HARD_STORAGE = { 800, 950, 1150, 1300, 1500, 2000 };

    //----Reputation----

    public const int REP_GAIN = 10;
    public const int REP_LOSS = 10;

    //Server Config

    //-Selling
    public const float SELL_SERVER_CLIENT_REP_LOSS = (float)0.2;

    //-Security
    public const int STARTING_SECURITY_LEVEL = 50; //in percent
    public const int MAX_SECURITY_UPGRADES = 3;
    public const int MAX_SECURITY_INCREASE = 40; //in percent. Since we start at 50% security, the maximum security level can be 90% (if all ports are closed).
    public const int SECURITY_DECREASE_PER_PORT = 5; //in percent. Each port open decreases the server security by 5%
    public const int SECURITY_UPGRADE_COST = 200;

    //-Cooling
    public const int SERVER_MEDIUM_TEMP = 62;
    public const int SERVER_HIGH_TEMP = 70;
    public const int MAX_COOLING_UPGRADES = 3;
    public const int OVERCLOCK_MAX_TEMP_INCREASE = 30;
    public const int MAX_TEMPERATURE_DECREASE = 10; //in degrees. So if cooling upgrades are at maximum (3), 10 degrees celsius is taken off the server temperature
    public const int COOLING_UPGRADE_COST = 100;
    public const int MIN_CPU_TEMP = 42;
    public const float CPU_TEMP_CURVE_MOD = (float)0.73;

    //-Health
    public const float SERVER_HEALTH_DECREASE = (float)1;
}
