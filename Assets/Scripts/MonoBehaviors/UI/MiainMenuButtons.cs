using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MiainMenuButtons : MonoBehaviour {

    private void Start()
    {
        Cursor.visible = true;
    }

    public void PlayButton()
    {
        SceneManager.LoadSceneAsync("01-Level1");
    }

    public void CreditsButton()
    {
        SceneManager.LoadSceneAsync("Credits");
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}