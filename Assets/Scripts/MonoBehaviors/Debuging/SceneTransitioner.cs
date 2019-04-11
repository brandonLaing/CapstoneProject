using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneTransitioner : MonoBehaviour
{
  public static SceneTransitioner main;

  public string[] scenes = { "01-Level1", "scene_MiniBlocks", "scene_NoPlatformZone", "scene_TransitionTest" };

  private void Start()
  {
    DontDestroyOnLoad(this.gameObject);

    if (main != null)
    {
      Destroy(this.gameObject);
    }
    else
    {
      main = this;
      DontDestroyOnLoad(this.gameObject);
    }
  }

  private void Update()
  {
    for (int i = 257; i <= 265; i++)
    {
      if (Input.GetKeyDown((KeyCode)i))
      {
        int sceneNumber = i - 257;
        if (sceneNumber < scenes.Length && scenes[sceneNumber] != null)
        {
          SceneManager.LoadScene(scenes[sceneNumber]);
        }
      }
    }
  }
}
