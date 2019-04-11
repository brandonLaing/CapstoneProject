using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerUnlock : MonoBehaviour
{
  public ProjectionType unlockType;
  public bool destroyOnEnter = true;
  public bool unlockState = true;
  public bool changesPrefab;
  public GameObject newPrefab;

  private void OnTriggerEnter(Collider other)
  {
    if (other.tag == "Player")
    {
      PlayerGunPlatformSelector selector = other.GetComponent<PlayerGunPlatformSelector>();
      switch (unlockType)
      {
        case ProjectionType.StaticPlatform:
          selector.staticAvailable = unlockState;
          if (changesPrefab)
            selector.staticPrefab = newPrefab;
          break;

        case ProjectionType.MovingPlatform:
          selector.movingAvailable = unlockState;
          if (changesPrefab)
            selector.movingPrefab = newPrefab;
          break;

        case ProjectionType.SpeedPlatform:
          selector.speedAvailable = unlockState;
          if (changesPrefab)
            selector.speedPrefab = newPrefab;
          break;

        case ProjectionType.BouncePlatform:
          selector.bounceAvailable = unlockState;
          if (changesPrefab)
            selector.bounceAvailable = newPrefab;
          break;
      }

      if (destroyOnEnter)
        Destroy(this.gameObject);
    }
  }
}
