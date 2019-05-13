﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
  [SerializeField]
  private PlayerInputManager inputMan;
  [SerializeField]
  private GameObject pauseCanvas;
  [SerializeField]
  private GameObject levelSelect;
  [SerializeField]
  private GameObject pauseMenu;
  [SerializeField]
  private bool IsPaused = false;

  private void Awake()
  {
    inputMan.OnStartPressedDown += TogglePause;
  }

  private void TogglePause()
  {
    if (IsPaused) _UnpauseGame();
    else PauseGame();
  }

  private void PauseGame()
  {
    pauseMenu.SetActive(true);
    levelSelect.SetActive(false);

    Debug.Log("Pausing game");
    Time.timeScale = 0;
    pauseCanvas.SetActive(true);
    IsPaused = true;
    Cursor.lockState = CursorLockMode.Confined;
  }

  public void _UnpauseGame()
  {
    Debug.Log("Unpausing");

    Time.timeScale = 1;
    pauseCanvas.SetActive(false);
    Cursor.lockState = CursorLockMode.Locked;
    IsPaused = false;
  }

  public void _LoadLevel(string sceneName)
  {
    SceneManager.LoadScene(sceneName);
  }

  private void OnDestroy()
  {
    inputMan.OnStartPressedDown -= TogglePause;
  }
}
