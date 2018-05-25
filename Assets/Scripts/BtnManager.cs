using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BtnManager : MonoBehaviour {

	public void NewGame()
    {
        SceneManager.LoadScene("Office");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
