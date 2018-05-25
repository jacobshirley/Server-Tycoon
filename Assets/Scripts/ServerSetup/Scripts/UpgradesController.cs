using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradesController : MonoBehaviour {

    Text securityLvlText = null;
    Text coolingLvlText = null;

    Button securityUpgradeBtn = null;
    Button coolingUpgradeBtn = null;

    // Use this for initialization
    void Start () {
        ServerPlacedScript server = GameData.CurrentServer;
        ServerDef def = server.data.def;
        Slider overclockingSlider = this.transform.Find("Overclocking Slider").GetComponent<Slider>();

        overclockingSlider.value = server.data.overclockedPercent;
        overclockingSlider.onValueChanged.AddListener(delegate
        {
            server.data.overclockedPercent = overclockingSlider.value;
        });

        securityLvlText = this.transform.Find("Security Lvl").GetChild(0).GetComponent<Text>();
        coolingLvlText = this.transform.Find("Cooling Lvl").GetChild(0).GetComponent<Text>();

        securityUpgradeBtn = this.transform.Find("Security Upgrade").GetComponent<Button>();
        coolingUpgradeBtn = this.transform.Find("Cooling Upgrade").GetComponent<Button>();

        SetSecurityLvlValue();
        SetCoolingLvlValue();

        securityUpgradeBtn.onClick.AddListener(delegate
        {
            
            if (server.data.securityUpgrades < Settings.MAX_SECURITY_UPGRADES)
            {
                GameObject.Find("Money").GetComponent<economy>().DecreaseProfit(Settings.SECURITY_UPGRADE_COST);
                server.data.securityUpgrades++;
                SetSecurityLvlValue();
            }
        });

        coolingUpgradeBtn.GetComponent<Button>().onClick.AddListener(delegate
        {
            
            if (server.data.coolingUpgrades < Settings.MAX_COOLING_UPGRADES)
            {
                GameObject.Find("Money").GetComponent<economy>().DecreaseProfit(Settings.COOLING_UPGRADE_COST);
                server.data.coolingUpgrades++;
                SetCoolingLvlValue();
            }
        });
    }

    void SetSecurityLvlValue()
    {
        ServerPlacedScript server = GameData.CurrentServer;
        securityLvlText.text = server.data.securityUpgrades + " / " + Settings.MAX_SECURITY_UPGRADES;

        if (server.data.securityUpgrades < Settings.MAX_SECURITY_UPGRADES) {
            securityUpgradeBtn.GetComponentInChildren<Text>().text = "$" + Settings.SECURITY_UPGRADE_COST + " Security Upgrade";
        } else
        {
            securityUpgradeBtn.GetComponentInChildren<Text>().text = "Maximum Upgrades";
            securityUpgradeBtn.interactable = false;
        }
    }

    void SetCoolingLvlValue()
    {
        ServerPlacedScript server = GameData.CurrentServer;
        this.transform.Find("Cooling Lvl").GetChild(0).GetComponent<Text>().text = server.data.coolingUpgrades + " / " + Settings.MAX_SECURITY_UPGRADES;

        if (server.data.coolingUpgrades < Settings.MAX_COOLING_UPGRADES)
        {
            coolingUpgradeBtn.GetComponentInChildren<Text>().text = "$" + Settings.COOLING_UPGRADE_COST + " Cooling Upgrade";
        }
        else
        {
            coolingUpgradeBtn.GetComponentInChildren<Text>().text = "Maximum Upgrades";
            coolingUpgradeBtn.interactable = false;
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
