using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGunPlatformPlacer : MonoBehaviour
{
  #region Variables
  public event Action<GameObject> OnNewPlatformCreated = delegate { };
  public event Action<Vector3> OnEndPointSet = delegate { };
  public GameObject movingPlatform;

  public Transform platformLocation;

  [SerializeField]
  private AudioSource shootSound;
  #endregion

  #region Unity Events
  private void Awake()
  {
    GetComponent<PlayerGunShooter>().OnGunFired += ShootGun;
    GetComponent<PlayerGunShooter>().OnGunStandby += DestroyMovingPlatform;
    GetComponent<PlayerGunShooter>().OnMovingInterupted += DestroyMovingPlatform;
  }

  private void OnDestroy()
  {
    GetComponent<PlayerGunShooter>().OnGunFired -= ShootGun;
    GetComponent<PlayerGunShooter>().OnGunStandby -= DestroyMovingPlatform;
    GetComponent<PlayerGunShooter>().OnMovingInterupted -= DestroyMovingPlatform;
  }

  private void FixedUpdate()
  {
    
  }
  #endregion

  #region Functions
  /// <summary>
  /// Executes logic on when to fire the gun
  /// </summary>
  /// <param name="platformPrefab">Prefab that will be created</param>
  /// <param name="currentState">State of the gun</param>
  /// <param name="platformType">Type of platform that is going to be spawned</param>
  private void ShootGun(GameObject platformPrefab, GunState currentState, ProjectionType platformType)
  {
    switch (currentState)
    {
      case GunState.Triggered:
        PlaceNewPlatform(platformPrefab, platformType);
        break;
      case GunState.PlaceingMovingEndPoint:
        PlaceMovingEndPoint();
        break;
    }
  }

  /// <summary>
  /// Placed down a new platform
  /// </summary>
  /// <param name="platformPrefab">Prefab to be spawned in</param>
  /// <param name="platformType">Type of platform being spawned</param>
  private void PlaceNewPlatform(GameObject platformPrefab, ProjectionType platformType)
  {
    if (platformPrefab == null)
      return;

    GameObject newPlatform = Instantiate(platformPrefab, platformLocation.position, platformLocation.rotation);
    newPlatform.name += ("(Clone)");
    shootSound.Play();

    if (platformType == ProjectionType.MovingPlatform)
    {
      movingPlatform = newPlatform;
      OnEndPointSet += movingPlatform.GetComponent<PlatformMoving>().AddEndPoint;
    }
    else
      OnNewPlatformCreated(newPlatform);
  }

  /// <summary>
  /// If gun is put back into standby mode the moving platform currently being placed will be removed
  /// </summary>
  private void DestroyMovingPlatform()
  {
    if (movingPlatform != null)
    {
      OnEndPointSet -= movingPlatform.GetComponent<PlatformMoving>().AddEndPoint;
      Destroy(movingPlatform);
      movingPlatform = null;
    }
  }

  /// <summary>
  /// Places the other end for a moving platform
  /// </summary>
  private void PlaceMovingEndPoint()
  {
    if (platformLocation != null)
    {
      OnNewPlatformCreated(movingPlatform);
      OnEndPointSet(platformLocation.transform.position);
      OnEndPointSet -= movingPlatform.GetComponent<PlatformMoving>().AddEndPoint;
    }

    movingPlatform = null;
  }
  #endregion
}
