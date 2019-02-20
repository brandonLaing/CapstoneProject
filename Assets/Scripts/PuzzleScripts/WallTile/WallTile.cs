using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// @author Brandon Laing

/// <summary>
/// The position that a wall tile can be in
/// </summary>
public enum WallTileState
{
  Up, Right, Down, Left, Length
}

/// <summary>
/// Controls each indivdual wall tile
/// </summary>
public class WallTile : MonoBehaviour, IInteractable
{
  /// <summary>
  /// State the wall tile will start in
  /// </summary>
  [Tooltip("State the wall tile will start in")]
  public WallTileState tileStart = WallTileState.Length;

  /// <summary>
  /// Current state of the wall tile
  /// </summary>0
  [Tooltip("Current state of the wall tile")]
  public WallTileState tileCurrent = WallTileState.Length;

  /// <summary>
  /// Correct state of the wall tile
  /// </summary>
  [Tooltip("Correct state of the wall tile")]
  public WallTileState tileCorrect = WallTileState.Length;

  /// <summary>
  /// Checks if the currentState == the correct state and it has been checked
  /// </summary>
  public bool IsCorrect
  { get { return tileCurrent == tileCorrect && checkPassed; } }

  /// <summary>
  /// Checked at the end of a rotation to see if the tile is in the right position
  /// </summary>
  private bool checkPassed = false;

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

  private Vector3 _targetRotation = new Vector3(0, 0, 0);

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

  public bool AbleToInteract
  {
    get
    {
      return true;
    }
  }

  private void Start()
  {
    if (tileStart == WallTileState.Length || tileCorrect == WallTileState.Length)
      Debug.LogError($"Set all states for tiles {transform.name} dipshit");

    SetStartingRotation();
  }

  /// <summary>
  /// Sets the starting rotation in line with the tileState
  /// </summary>
  private void SetStartingRotation()
  {
    switch (tileStart)
    {
      case WallTileState.Up:
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        break;

      case WallTileState.Right:
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
        break;

      case WallTileState.Down:
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
        break;

      case WallTileState.Left:
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, 270));
        break;
    }

    tileCurrent = tileStart;
  }


  /// <summary>
  /// When the tile is interected with it increments the walls state
  /// </summary>
  private void IncrementState()
  {
    if ((WallTileState)((int)tileCurrent + 1) >= WallTileState.Length)
      tileCurrent = (WallTileState)0;
    else
      tileCurrent = (WallTileState)((int)tileCurrent + 1);

    TargetRotation += new Vector3(0, 0, 90);

    if (rotating == false)
      StartCoroutine(RotateTile());
  }

  /// <summary>
  /// Rotates the tile to a specific state
  /// </summary>
  /// <param name="desiredState">State you want the tile to rotate too</param>
  private void IncrementState(WallTileState desiredState)
  {
    if (tileCurrent != desiredState)
    {
      tileCurrent = desiredState;
      if (rotating == false)
      {
        StartCoroutine(RotateTile());
      }
    }
  }

  /// <summary>
  /// Rotates the tile untill its gets within an exceptable range then snaps the tile to its target range
  /// </summary>
  /// <returns></returns>
  private IEnumerator RotateTile()
  {
    rotating = true;

    while (TargetRotation.z > transform.rotation.eulerAngles.z + deadSpace || TargetRotation.z < transform.rotation.eulerAngles.z - deadSpace)
    {
      transform.Rotate(transform.forward * rotationSpeed * Time.deltaTime);
      yield return new WaitForEndOfFrame();
    }

    transform.rotation = Quaternion.Euler(TargetRotation);
    CheckForCorrectState();

    rotating = false;
  }

  /// <summary>
  /// Called once tile is set in place, updates material anc checkPassed
  /// </summary>
  private void CheckForCorrectState()
  {
    if (tileCurrent == tileCorrect)
    {
      checkPassed = true;

      // Update the materials
    }
  }

  /// <summary>
  /// Starts process of turing the wall tile
  /// </summary>
  public void Interact()
  {
    checkPassed = false;
    IncrementState();
  }
}
