using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGunPlatformCountManager : MonoBehaviour
{
  #region Variables
  public int maxNumberOfPlatforms;
  private Queue<GameObject> platformQue = new Queue<GameObject>();
  #endregion

  #region Start/End Functions
  private void Awake()
  {
    GetComponent<PlayerGunPlatformPlacer>().OnNewPlatformCreated += NewPlatformCreated;
  }

  private void OnDestroy()
  {
    GetComponent<PlayerGunPlatformPlacer>().OnNewPlatformCreated -= NewPlatformCreated;
  }
  #endregion

  #region Functions
  public void NewPlatformCreated(GameObject newPlatform)
  {
    while (platformQue.Count >= maxNumberOfPlatforms)
    {
      Destroy(platformQue.Dequeue());
    }

    platformQue.Enqueue(newPlatform);
  }
  #endregion
}