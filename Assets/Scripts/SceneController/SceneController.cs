using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
  public string previousScene;
  public string currentScene;
  public string nextScene;

  public GameObject entranceDoor;
  public GameObject collisionChecker;

  private void Start()
  {
    if (previousScene == string.Empty)
    {
      LoadNextScene();
    }
  }

  /// <summary>
  /// Moves player to next scene
  /// </summary>
  public void MovePlayerOver()
  {
    Debug.Log($"Moving Player over from {currentScene} to {nextScene}");
  
    GameObject[] sceneControllers = GameObject.FindGameObjectsWithTag("SceneController");
    SceneController nextController = null;
    GameObject player = GameObject.FindGameObjectWithTag("PlayerRoot");

    if (sceneControllers.Length != 2)
    {
      Debug.LogWarning("You dont have two scene controllers when trying to transition scenes and thus cant transition");
      return;
    }

    if (player == null)
    {
      Debug.LogWarning("Player could not be found and scene transition stopped");
      return;
    }

    SetCurrentSceneActive();

    for (int i = 0; i < sceneControllers.Length; i++)
    {
      if (sceneControllers[i].GetComponent<SceneController>() == this)
        nextController = sceneControllers[i].GetComponent<SceneController>();
    }

    if (nextController != null)
      SendPlayer(player, nextController);
    else
      Debug.LogWarning($"Couldnt send playe to next scene because not controller that wasnt {transform.name} was found");

    ClosePreviousScene();
  }

  /// <summary>
  /// Sets the next scene to active
  /// </summary>
  private void SetCurrentSceneActive()
  {
    Debug.Log($"Setting {currentScene} as active");
    SceneManager.SetActiveScene(SceneManager.GetSceneByName(currentScene));
  }

  /// <summary>
  /// Closes door to the previous scene
  /// </summary>
  public void ClosePreviousScene()
  {
    Debug.Log($"Closing scene {previousScene}");
    entranceDoor.SetActive(true);
    UnloadPreviousScene();
  }

  private void SendPlayer(GameObject player, SceneController controller)
  {
    Debug.Log($"Sending player over to {nextScene}");
    controller.RecivePlayer(player);
  }

  private void RecivePlayer(GameObject player)
  {
    Debug.Log($"Reciving player on {currentScene}");

    player.transform.parent = this.transform;
    player.transform.parent = null;
  }

  private void OnTriggerEnter(Collider other)
  {
    if (other.CompareTag("Player"))
    {
      Debug.Log($"{currentScene} trigger entered");
      collisionChecker.SetActive(false);
      MovePlayerOver();
    }
  }

  #region Scene Loading
  /// <summary>
  /// starts to load next scene
  /// </summary>
  public void LoadNextScene()
  {
    Debug.Log($"Starting load {nextScene}");

    if (nextScene != string.Empty)
      StartCoroutine(LoadSceneAsync());
    else
      Debug.LogWarning("Can't load next scene without next scene assigned");
  }

  /// <summary>
  /// Unloads the previous scene
  /// </summary>
  public void UnloadPreviousScene()
  {
    Debug.Log($"Starting unload previous scene {previousScene}");
    StartCoroutine(UnloadSceneAsync());
  }

  /// <summary>
  /// Does the async loading
  /// </summary>
  /// <returns></returns>
  private IEnumerator LoadSceneAsync()
  {
    AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(nextScene, LoadSceneMode.Additive);

    while (!asyncLoad.isDone)
    {
      yield return null;
    }
  }

  private IEnumerator UnloadSceneAsync()
  {
    AsyncOperation asyncUnLoad = SceneManager.UnloadSceneAsync(previousScene);

    while (!asyncUnLoad.isDone)
    {
      yield return null;
    }

    LoadNextScene();
  }
  #endregion
}
