using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PortsController : MonoBehaviour {

    delegate void Listener();

    private List<Listener> listeners = new List<Listener>();

    void Start() {
        ServerPlacedScript server = GameData.CurrentServer;

        foreach (int port in server.data.portsOpen)
        {
            EnablePort(port);
        }

        PortHandler(server, "OpenFTP", new int[] { 20, 21 });
        PortHandler(server, "OpenSSH", new int[] { 22 });
        PortHandler(server, "OpenSMTP", new int[] { 25 });
        PortHandler(server, "OpenHTTP", new int[] { 80 });
    }

    private void PortHandler(ServerPlacedScript server, string portName, int[] portNums) {
        Transform obj = this.transform.Find(portName);

        ButtonToggle toggle = obj.GetComponent<ButtonToggle>();

        obj.GetComponent<Button>().onClick.AddListener(delegate
        {
            listeners.Add(delegate
            {
                
                if (toggle.isDown)
                {
                    server.data.portsOpen.AddRange(portNums);
                }
                else
                {
                    foreach (int clientId in server.data.clients)
                    {
                        Client client = GameData.storage.clients.GetClient(clientId);
                        if (Array.Exists(client.reqPorts, i => Array.Exists(portNums, j => j == i)))
                        {
                            this.transform.Find("Do not turn off ports").gameObject.SetActive(true);
                            toggle.Down();
                            return;
                        }
                    }

                    server.data.portsOpen.RemoveAll(delegate (int i)
                    {
                        foreach (int p in portNums)
                            if (p == i)
                                return true;
                        return false;
                    });
                }
            });
        });
    }


    private void EnablePort(int port)
    {
        Transform buttonTrans = null;
        switch (port)
        {
            case 20:
                buttonTrans = this.transform.Find("OpenFTP");
                break;
            case 21:
                buttonTrans = this.transform.Find("OpenFTP");
                break;
            case 22:
                buttonTrans = this.transform.Find("OpenSSH");
                break;
            case 25:
                buttonTrans = this.transform.Find("OpenSMTP");
                break;
            case 80:
                buttonTrans = this.transform.Find("OpenHTTP");
                break;
        }

        buttonTrans.GetComponent<ButtonToggle>().Down();
    }
	
	void Update () {
        foreach (Listener l in listeners)
            l.Invoke();

        listeners.Clear();
	}
}
