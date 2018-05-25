using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class EmailManager : MonoBehaviour {

    public GameObject prefab;
    public Transform previewPanel;

    public EmailTemplate template;

    
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.P))
        {
            SendEmail();
            Debug.Log("CompanyA");
        }
    }

    void MakeButton(string x, string y, string z, string scenario)
    {
        GameObject newButton = (GameObject)GameObject.Instantiate(prefab);
        newButton.transform.SetParent(previewPanel);
        newButton.transform.SetSiblingIndex(0);
        ButtonManager buttonManager = newButton.GetComponent<ButtonManager>();
        buttonManager.SetVals(x, y, z, scenario);
        
    }

    public void SendEmail()
    {
        template = JsonUtility.FromJson<EmailTemplate>(File.ReadAllText("Assets/EmailTemplates/email"+ Random.Range(1,4).ToString() + ".json"));
        MakeButton(template.sender, template.subject, template.body, template.scenario);
        
    }
}
