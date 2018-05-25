using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClientMenu : MonoBehaviour {

    public void open()
    {
        SceneManager.LoadScene("ClientSelect", LoadSceneMode.Additive);
    }
}
