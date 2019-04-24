using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
  [SerializeField]
  private PlayerInputManager inputMan;
  [SerializeField]
  private GameObject pauseCanvas;

  private void Awake()
  {
    inputMan.OnStartPressedDown += TogglePause;
  }

  private void TogglePause()
  {
    pauseCanvas.SetActive(!pauseCanvas.activeSelf);
  }

  private void OnDestroy()
  {
    inputMan.OnStartPressedDown -= TogglePause;
  }
}
