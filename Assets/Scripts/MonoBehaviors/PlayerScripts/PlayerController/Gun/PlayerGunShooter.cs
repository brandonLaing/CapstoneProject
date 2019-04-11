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
    GetComponent<PlayerGunPlatformSelector>().OnPlatformSelected += SelectPlatform;
    GetComponent<PlayerInputManager>().OnGunTriggered += ToggleGunTrigger;
    GetComponent<PlayerInputManager>().OnGunShot += ShootGun;
  }
  private void OnDestroy()
  {
    GetComponent<PlayerGunPlatformSelector>().OnPlatformSelected -= SelectPlatform;
    GetComponent<PlayerInputManager>().OnGunTriggered -= ToggleGunTrigger;
    GetComponent<PlayerInputManager>().OnGunShot -= ShootGun;
  }

  /// <summary>
  /// Contains logic on 
  /// </summary>
  private void ShootGun()
  {
    System.Text.StringBuilder sb = new System.Text.StringBuilder();
    if (gunState == GunState.Standby)
      return;

    if (gunState == GunState.PlaceingMovingEndPoint)
    {
      OnGunFired(platformPrefab, gunState, projectionType);
      gunState = GunState.Triggered;
      return;
    }

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

  public event System.Action OnMovingInterupted = delegate { };
  public void SelectPlatform(GameObject prefab, ProjectionType _projectionType)
  {
    if (gunState == GunState.PlaceingMovingEndPoint)
    {
      OnMovingInterupted();
      gunState = GunState.Triggered;
    }
    this.platformPrefab = prefab;
    this.projectionType = _projectionType;
  }
}
