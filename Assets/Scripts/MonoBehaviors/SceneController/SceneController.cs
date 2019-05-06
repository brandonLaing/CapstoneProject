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
  public bool IsEnding;

  [SerializeField]
  private bool IsLoadingAdditive = true;

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
  private void MovePlayerOver()
  {
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
    SceneManager.SetActiveScene(SceneManager.GetSceneByName(currentScene));
  }

  /// <summary>
  /// Closes door to the previous scene
  /// </summary>
  private void ClosePreviousScene()
  {
    entranceDoor.SetActive(true);
    UnloadPreviousScene();
  }

  /// <summary>
  /// Sends message to send player over to next scene
  /// </summary>
  /// <param name="player">players root object</param>
  /// <param name="controller">Other scene controller</param>
  private void SendPlayer(GameObject player, SceneController controller)
  {
    controller.RecivePlayer(player);
  }

  /// <summary>
  /// Gets player from other scene
  /// </summary>
  /// <param name="player">Player</param>
  private void RecivePlayer(GameObject player)
  {
    player.transform.parent = this.transform;
    player.transform.parent = null;
  }

  /// <summary>
  /// This checks if the player hits the exit hitbox
  /// </summary>
  /// <param name="other"></param>
  private void OnTriggerEnter(Collider other)
  {
    if (other.CompareTag("Player"))
    {
      if (IsEnding)
      {
        SceneManager.LoadScene(nextScene);
      }
      else
      {
        collisionChecker.SetActive(false);
        MovePlayerOver();
      }
    }
  }

  #region Scene Loading
  /// <summary>
  /// starts to load next scene
  /// </summary>
  private void LoadNextScene()
  {
    if (nextScene != string.Empty)
      StartCoroutine(LoadSceneAsync());
    else
      Debug.LogWarning("Can't load next scene without next scene assigned");
  }

  /// <summary>
  /// Unloads the previous scene
  /// </summary>
  private void UnloadPreviousScene()
  {
    StartCoroutine(UnloadSceneAsync());
  }

  /// <summary>
  /// Does the async loading
  /// </summary>
  /// <returns></returns>
  private IEnumerator LoadSceneAsync()
  {
    AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(nextScene, IsLoadingAdditive ? LoadSceneMode.Additive : LoadSceneMode.Single);

    while (!asyncLoad.isDone)
    {
      yield return null;
    }
  }

  /// <summary>
  /// Does the async unloading
  /// </summary>
  /// <returns></returns>
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
