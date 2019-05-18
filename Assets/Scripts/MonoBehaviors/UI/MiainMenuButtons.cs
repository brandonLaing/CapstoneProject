using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MiainMenuButtons : MonoBehaviour {

    public GameObject controls;
    private bool controlsVisible;

    private void Start()
    {
        Cursor.visible = true;
    }

    private void Awake()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private void Update()
    {
        if (controlsVisible == true)
        {
            if (Input.anyKey)
            {
                controlsVisible = false;
            }
        }
        controls.SetActive(controlsVisible);
    }

    public void PlayButton()
    {
        SceneManager.LoadSceneAsync("01-Level1");
    }

    public void ControlsButton()
    {
        controlsVisible = true;
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