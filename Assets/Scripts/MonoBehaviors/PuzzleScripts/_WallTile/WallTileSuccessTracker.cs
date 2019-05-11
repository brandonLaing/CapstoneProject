using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(WallTileInteractor))]
[RequireComponent(typeof(WallTileRotator))]
[RequireComponent(typeof(WallTileColorChanger))]
public class WallTileSuccessTracker : MonoBehaviour
{
  #region Variables
  /// <summary>
  /// State the wall tile will start in
  /// </summary>
  [Tooltip("State the wall tile will start in")]
  public WallTileState tileStartState = WallTileState.Length;

  /// <summary>
  /// Correct state of the wall tile
  /// </summary>
  [Tooltip("Correct state of the wall tile")]
  public WallTileState tileCorrectState = WallTileState.Length;

  /// <summary>
  /// Hidden checkPassed
  /// </summary>
  private bool _checkPassed = false;

  public List<WallTileSuccessTracker> previousTiles = new List<WallTileSuccessTracker>();
  public WallTileSuccessTracker nextTile;

  private WallTileRotator rotator;
  private WallTileColorChanger colorChanger;

  [SerializeField]
  private AudioSource successSound;
  #endregion

  #region Properties
  /// <summary>
  /// Checks if the currentState == the correct state and it has been checked
  /// </summary>
  public bool IsCorrect
  { get { return rotator.tileCurrentState == tileCorrectState && CheckPassed; } }

  /// <summary>
  /// Checked at the end of a rotation to see if the tile is in the right position
  /// </summary>
  public bool CheckPassed
  {
    get { return _checkPassed; }
    set
    {
      _checkPassed = value;
      colorChanger.CheckPassed(value);

      if (nextTile != null)
        nextTile.CheckForCorrectState();
      else
        GetComponentInParent<WallTileManager>().PuzzleComplete(_checkPassed);
    }
  }

  #endregion

  #region Initialization
  private void Awake()
  {
    rotator = GetComponent<WallTileRotator>();
    colorChanger = GetComponent<WallTileColorChanger>();
  }

  private void Start()
  {
    if (tileStartState == WallTileState.Length || tileCorrectState == WallTileState.Length)
      Debug.LogError($"Set all states for tiles {transform.name} dipshit");

    CheckForCorrectState();
  }
  #endregion

  /// <summary>
  /// Called once tile is set in place, updates material anc checkPassed
  /// </summary>
  public void CheckForCorrectState()
  {
    if (rotator.tileCurrentState == tileCorrectState)
    {
      // if there is no previous tiles return true
      if (previousTiles.Count == 0)
      {
        CheckPassed = true;
        successSound.Play();
        return;
      }

      // for ever previous tile
      for (int i = 0; i < previousTiles.Count; i++)
      {
        WallTileSuccessTracker previousTracker = previousTiles[i];

        // if one is false set check passed to false and return from loop
        if (previousTracker.CheckPassed == false)
        {
          CheckPassed = false;
          return;
        }
      }

      CheckPassed = true;
      successSound.Play();
    }
    else
      CheckPassed = false;
  }
}