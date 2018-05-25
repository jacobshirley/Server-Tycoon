using UnityEngine;

[System.Serializable]
public class Client{
    public int id;
    public int difficulty;

	public string reqType;
	public string reqName;
	public float reqPPower;
	public int[] reqPorts;
	public float reqRam;
	public int reqStorage;
	public float reqPay;

    //Client server utilisation
    public float clientCPU;
    public float clientRAM;
    public int clientStorage;

    public float satisfaction;

    public int EmailFreq;
    public bool hasMail;
}
