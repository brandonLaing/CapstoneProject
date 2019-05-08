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

  private void OnTriggerEnter(Collider other)
  {
    Debug.Log("Hit something");
    if (other.tag == "Player")
    {
      Debug.Log("Other is player");
      PlayerGunPlatformSelector selector = other.GetComponent<PlayerGunPlatformSelector>();

      if (changesPrefab)
        selector.NewPlatformUnlocked(unlockType, newPrefab);
      else
        selector.NewPlatformUnlocked(unlockType, unlockState);

      if (destroyOnEnter)
        Destroy(this.gameObject);
    }
  }
}
