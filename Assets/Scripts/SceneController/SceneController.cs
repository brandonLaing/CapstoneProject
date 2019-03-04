using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
  public string nextScene;
  public string previousScene;

  private void Start()
  {
    if (previousScene == string.Empty)
    {
      LoadNextScene();
    }
  }

  /// <summary>
  /// starts to load next scene
  /// </summary>
  public void LoadNextScene()
  {
    StartCoroutine(LoadSceneAsync());
  }

  /// <summary>
  /// Does the async loading
  /// </summary>
  /// <returns></returns>
  private IEnumerator LoadSceneAsync()
  {
    yield return new WaitForSeconds(2F);

    Debug.Log("Starting load new scene");

    AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(nextScene, LoadSceneMode.Additive);

    while (!asyncLoad.isDone)
    {
      yield return null;
    }
  }

  /// <summary>
  /// Sets the next scene to active
  /// </summary>
  public void SetNextSceneActive()
  {

  }

  /// <summary>
  /// Moves player to next scene
  /// </summary>
  public void MovePlayerOver()
  {

  }

  /// <summary>
  /// Closes door to the previous scene
  /// </summary>
  public void ClosePreviousScene()
  {

  }

  /// <summary>
  /// Unloads the previous scene
  /// </summary>
  public void UnloadPreviousScene()
  {

  }
}
