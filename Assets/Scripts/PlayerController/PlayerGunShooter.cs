using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGunShooter : MonoBehaviour
{
  public event System.Action<GameObject, GunState, ProjectionType> OnGunFired = delegate { };
  public event System.Action OnGunTriggered = delegate { };
  public event System.Action OnGunStandby = delegate { };

  private GunState gunState = GunState.Standby;
  private GameObject platformPrefab;
  private ProjectionType projectionType = ProjectionType.Length;

  private void Awake()
  {
    // OnPlatformSelected
    GetComponent<PlayerInputManager>().OnGunTriggered += ToggleGunTrigger;
    GetComponent<PlayerInputManager>().OnGunShot += ShootGun;
  }
  private void OnDestroy()
  {
    GetComponent<PlayerInputManager>().OnGunTriggered -= ToggleGunTrigger;
    GetComponent<PlayerInputManager>().OnGunShot -= ShootGun;
  }

  /// <summary>
  /// Contains logic on 
  /// </summary>
  private void ShootGun()
  {
    if (gunState == GunState.Standby)
      return;

    if (gunState == GunState.PlaceingMovingEndPoint)
      OnGunFired(platformPrefab, gunState, projectionType);

    if (gunState == GunState.Triggered)
    {
      if (projectionType != ProjectionType.MovingPlatform)
        OnGunFired(platformPrefab, gunState, projectionType);
      else
      {
        OnGunFired(platformPrefab, gunState, projectionType);
        gunState = GunState.PlaceingMovingEndPoint;
      }
    }
  }

  /// <summary>
  /// Sets gunstate when gun trigger is called
  /// </summary>
  private void ToggleGunTrigger()
  {
    switch (gunState)
    {
      case GunState.Standby:
        gunState = GunState.Triggered;
        OnGunTriggered();
        break;
      default:
        gunState = GunState.Standby;
        OnGunStandby();
        break;
    }
  }
}
