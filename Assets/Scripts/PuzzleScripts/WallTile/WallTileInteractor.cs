using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallTileInteractor : MonoBehaviour, IInteractable
{
  /// <summary>
  /// Check on the tile manager set to whether or not the puzzle has been solved
  /// </summary>
  public bool AbleToInteract
  {
    get
    {
      return !manager.puzzleLocked;
    }
  }

  private WallTileManager manager;
  private WallTileRotator rotator;
  private WallTileSuccessTracker successTracker;

  public WallTileRotator otherTileToRotate;

  private void Awake()
  {
    manager = GetComponentInParent<WallTileManager>();
    rotator = GetComponent<WallTileRotator>();
    successTracker = GetComponent<WallTileSuccessTracker>();
  }

  /// <summary>
  /// Starts process of turing the wall tile
  /// </summary>
  public void Interact()
  {
    if (AbleToInteract)
    {
      successTracker.CheckPassed = false;
      rotator.IncrementState();

      if (otherTileToRotate != null)
        otherTileToRotate.IncrementState();
    }
  }
}
