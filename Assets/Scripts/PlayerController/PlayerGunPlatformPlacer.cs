using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGunPlatformPlacer : MonoBehaviour
{
  #region Variables
  public GunState gunState = GunState.Standby;
  public GameObject currentMovingPlatform;
  public event Action<GameObject> OnNewPlatformCreated = delegate { };
  public event Action<Vector3> OnEndPointSet = delegate { };

  public Transform platformLocation;
  public GameObject platformPrefab;
  public ProjectionType currentPlatformType;
  #endregion

  #region Start/End Functions

  #endregion

  #region Functions
  /// <summary>
  /// Do logic on what to shoot
  /// </summary>
  public void ShootGun()
  {
    switch (gunState)
    {
      case GunState.Triggered:
        PlacePlatform();
        break;
      case GunState.PlaceingMovingEndPoint:
        PlaceMovingEndPoint();
        break;
    }
  }

  /// <summary>
  /// Places down platforms into the world
  /// </summary>
  private void PlacePlatform()
  {
    GameObject newPlatform = Instantiate(platformPrefab, platformLocation);
    newPlatform.name = platformPrefab.name + " (Clone)";


    if (currentPlatformType == ProjectionType.MovingPlatform)
    {
      gunState = GunState.PlaceingMovingEndPoint;
      OnEndPointSet += newPlatform.GetComponent<PlatformMoving>().AddEndPoint;
    }
    else
      OnNewPlatformCreated(newPlatform);
  }

  /// <summary>
  /// Places the other end for a moving platform
  /// </summary>
  private void PlaceMovingEndPoint()
  {
    OnEndPointSet(platformLocation.transform.position);
    OnEndPointSet = null;
  }

  public void SetGunToStandby()
  {
    if (gunState == GunState.PlaceingMovingEndPoint)
    {
      Destroy(currentMovingPlatform);
    }
  }
  #endregion
}
