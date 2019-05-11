using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// @author Brandon Laing

/// <summary>
/// Controls each individual wall tile
/// </summary>
public class WallTileRotator : MonoBehaviour
{
  #region Variables
  /// <summary>
  /// Current state of the wall tile
  /// </summary>0
  [Tooltip("Current state of the wall tile")]
  public WallTileState tileCurrentState = WallTileState.Length;

  /// <summary>
  /// Speed at which tile rotates
  /// </summary>
  [Tooltip("Speed at which tile rotates")]
  public float rotationSpeed;

  /// <summary>
  /// Check to see if the tile is currently rotating
  /// </summary>
  private bool rotating = false;

  /// <summary>
  /// Minimum space to stop rotating
  /// </summary>
  [Tooltip("Minimum space to stop rotating")]
  public float deadSpace = 2;
  
  /// <summary>
  /// Rotation that the wall tile is trying to reach
  /// </summary>
  private Vector3 _targetRotation = new Vector3(0, 0, 0);

  public Transform arrows;

  private WallTileSuccessTracker successTracker;

  [SerializeField]
  private AudioSource rotatorSound;
  #endregion

  #region Properties
  /// <summary>
  /// Rotation the tile wants to rotate too
  /// </summary>
  private Vector3 TargetRotation
  {
    get
    {
      return _targetRotation;
    }
    set
    {
      if (value.z > 360)
      {
        value.z -= 360;
      }

      _targetRotation = value;

    }
  }
  #endregion

  private void Awake()
  {
    successTracker = GetComponent<WallTileSuccessTracker>();
  }

  private void Start()
  {
    SetStartingRotation();
  }

  /// <summary>
  /// Sets the starting rotation in line with the tileState
  /// </summary>
  private void SetStartingRotation()
  {
    switch (successTracker.tileStartState)
    {
      case WallTileState.Up:
        arrows.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
        TargetRotation = new Vector3(0, 0, 0);
        break;

      case WallTileState.Right:
        arrows.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 90));
        TargetRotation = new Vector3(0, 0, 90);
        break;

      case WallTileState.Down:
        arrows.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 180));
        TargetRotation = new Vector3(0, 0, 180);
        break;

      case WallTileState.Left:
        arrows.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 270));
        TargetRotation = new Vector3(0, 0, 270);
        break;
    }

    tileCurrentState = successTracker.tileStartState;
  }


  /// <summary>
  /// When the tile is intersected with it increments the walls state
  /// </summary>
  public void IncrementState()
  {
    successTracker.CheckPassed = false;

    if ((WallTileState)((int)tileCurrentState + 1) >= WallTileState.Length)
      tileCurrentState = (WallTileState)0;
    else
      tileCurrentState = (WallTileState)((int)tileCurrentState + 1);

    TargetRotation += new Vector3(0, 0, 90);

    if (rotating == false)
      StartCoroutine(RotateTile());
  }

  /// <summary>
  /// Rotates the tile to a specific state
  /// </summary>
  /// <param name="desiredState">State you want the tile to rotate too</param>
  public void IncrementState(WallTileState desiredState)
  {
    if (tileCurrentState != desiredState)
    {
      tileCurrentState = desiredState;
      if (rotating == false)
      {
        StartCoroutine(RotateTile());
      }
    }
  }

  /// <summary>
  /// Rotates the tile until its gets within an expectable range then snaps the tile to its target range
  /// </summary>
  /// <returns></returns>
  private IEnumerator RotateTile()
  {
    rotating = true;
    rotatorSound.Play();

    while (TargetRotation.z > arrows.transform.localRotation.eulerAngles.z + deadSpace || TargetRotation.z < arrows.transform.localRotation.eulerAngles.z - deadSpace)
    {
      arrows.transform.Rotate(transform.forward * rotationSpeed * Time.deltaTime, Space.World);
      yield return new WaitForEndOfFrame();
    }

    arrows.transform.localRotation = Quaternion.Euler(TargetRotation);
    successTracker.CheckForCorrectState();
    rotatorSound.Stop();

    rotating = false;
  }
}
