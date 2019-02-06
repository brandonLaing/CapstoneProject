using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GunState
{
  Standby, Triggered
}
public enum ProjectionType
{
  StaticPlatform, SpeedPlatform, JumpPlatform, MovingPlatform, Length
}

public class PlayerPlatformGun : MonoBehaviour
{
  #region Variables
  public Transform cameraTransform;

  public Transform projectionLocation;

  public float minPlatformRange, maxPlatformRange;
  public float _platformRange = 10;
  public float PlatformRange
  {
    get
    {
      return _platformRange;
    }
    set
    {
      if (value >= minPlatformRange && value <= maxPlatformRange)
        _platformRange = value;
    }
  }

  private GunState _gunState = GunState.Standby;
  public GunState CurrentGunState
  {
    get
    {
      return _gunState;
    }
    set
    {
      _gunState = value;
      if (_gunState == GunState.Standby)
        projectionLocation.gameObject.SetActive(false);
      if (_gunState == GunState.Triggered)
        projectionLocation.gameObject.SetActive(true);
    }
  }

  public ProjectionType projectionType;
  #endregion

  private void Start()
  {
    CurrentGunState = GunState.Standby;
  }

  private void Update()
  {
    switch (CurrentGunState)
    {
      case GunState.Standby:
        StandbyStateLogic();
        break;
      case GunState.Triggered:
        TriggeredStateLogic();
        break;
    }
  }

  private void StandbyStateLogic()
  {
    if (Input.GetMouseButtonDown(0))
    {
      CurrentGunState = GunState.Triggered;
    }
  }

  /// <summary>
  /// All the logic while the gun is in triggered mode
  /// </summary>
  private void TriggeredStateLogic()
  {
    // check to put the gun back into standby
    if (Input.GetMouseButtonDown(0))
      CurrentGunState = GunState.Standby;

    // check for the zoom of the gun
    PlatformRange += Input.GetAxis("Mouse ScrollWheel");

    // check if the type of platform has changed
    CyclingCheck();

    if (Input.GetKeyDown(KeyCode.Alpha1))
      projectionType = ProjectionType.StaticPlatform;
    if (Input.GetKeyDown(KeyCode.Alpha2))
      projectionType = ProjectionType.SpeedPlatform;
    if (Input.GetKeyDown(KeyCode.Alpha3))
      projectionType = ProjectionType.JumpPlatform;


    // update the material of the platform

    // set the transform of the projection
  }

  /// <summary>
  /// Checks for platform Cycling with Q and E
  /// </summary>
  private void CyclingCheck()
  {
    int currentState = (int)projectionType;

    if (Input.GetKeyDown(KeyCode.Q))
    {
      currentState--;
      if (currentState < 0)
        currentState = (int)ProjectionType.Length - 1;

    }
    if (Input.GetKeyDown(KeyCode.E))
    {
      currentState++;
      if (currentState >= (int)ProjectionType.Length)
        currentState = 0;
    }
  }

  private void HouseKeeping()
  {

  }
}
