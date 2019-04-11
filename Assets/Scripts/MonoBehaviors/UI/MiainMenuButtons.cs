using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MiainMenuButtons : MonoBehaviour {

    private void Start()
    {
        Cursor.visible = true;
    }

    private void Update()
    {
        if (Input.GetButtonDown("aButton"))
        {
            PlayButton();
        }
    }

    public void PlayButton()
    {
        SceneManager.LoadSceneAsync("03_Level01_JD");
    }

    public void CreditsButton()
    {
        SceneManager.LoadSceneAsync("02_Credits");
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
