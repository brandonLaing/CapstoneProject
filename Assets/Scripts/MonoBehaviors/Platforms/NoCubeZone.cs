using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoCubeZone : MonoBehaviour
{
  private bool IsPlayerInside;
  [SerializeField]
  private bool IsDestroyingPlatforms;

  private void OnTriggerEnter(Collider other)
  {
    if (!IsPlayerInside && other.CompareTag("Player"))
    {
      IsPlayerInside = true;
      other.GetComponent<PlayerGunShooter>().PutGunToStandby();
      other.GetComponent<PlayerGunShooter>().IsAbleToShoot = false;
    }

    Debug.Log(other.name);
    if (IsDestroyingPlatforms && other.CompareTag("Platform"))
    {
      Destroy(other.transform.parent.gameObject);
    }
  }

  private void OnTriggerExit(Collider other)
  {
    if (other.CompareTag("Player") && IsPlayerInside)
    {
      IsPlayerInside = false;
      other.GetComponent<PlayerGunShooter>().IsAbleToShoot = true;
    }
  }
}
