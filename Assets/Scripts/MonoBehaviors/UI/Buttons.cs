using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
  public void _LoadScene(string sceneName)
  {
    SceneManager.LoadScene(sceneName);
  }

  public void _QuitGame()
  {
    Application.Quit();
  }
}
