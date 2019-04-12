using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnlock : MonoBehaviour
{
  public ProjectionType unlockType;
  public bool destroyOnEnter;
  public bool unlockState;

  public bool changesPrefab;
  public GameObject newPrefab;

  //public ProjectionType _unlockType;
  //public bool _destroyOnEnter;
  //public bool _unlockState;

  //public bool _changesPrefab;
  //public GameObject _newPrefab;

  private void OnTriggerEnter(Collider other)
  {
    if (other.tag == "Player")
    {
      Debug.Log("Shit");
    
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
