using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnlock : MonoBehaviour
{
  public ProjectionType unlockType;
  public bool destroyOnEnter = true;
  public bool unlockState = true;

  public bool changesPrefab = false;
  public GameObject newPrefab;

  private void Start()
  {
    if (changesPrefab && newPrefab == null)
    {
      Debug.LogWarning($"{transform.name} set to swap platform but has no new platform. Setting change trigger to false");
      changesPrefab = false;
    }
  }

  private void OnTriggerEnter(Collider other)
  {
    if (other.tag == "Player")
    {
      PlayerGunPlatformSelector selector = other.GetComponent<PlayerGunPlatformSelector>();

      if (changesPrefab)
        selector.NewPlatformUnlocked(unlockType, newPrefab);
      else
        selector.NewPlatformUnlocked(unlockType, unlockState);

      /**
      //switch (unlockType)
      //{
      //  case ProjectionType.StaticPlatform:
      //    selector.staticAvailable = unlockState;
      //    if (changesPrefab)
      //      selector.staticPrefab = newPrefab;
      //    break;

      //  case ProjectionType.MovingPlatform:
      //    selector.movingAvailable = unlockState;
      //    if (changesPrefab)
      //      selector.movingPrefab = newPrefab;
      //    break;

      //  case ProjectionType.SpeedPlatform:
      //    selector.speedAvailable = unlockState;
      //    if (changesPrefab)
      //      selector.speedPrefab = newPrefab;
      //    break;

      //  case ProjectionType.BouncePlatform:
      //    selector.bounceAvailable = unlockState;
      //    if (changesPrefab)
      //      selector.bounceAvailable = newPrefab;
      //    break;
      //}
  */

      if (destroyOnEnter)
        Destroy(this.gameObject);
    }
  }
}
