using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallTileManager : MonoBehaviour, ILockable
{
  public bool puzzleLocked = false;

  public List<WallTileSuccessTracker> puzzleTiles = new List<WallTileSuccessTracker>();

  public PuzzleEffect puzzleEffect;

  public void Lock()
  {
    puzzleLocked = true;
  }

  public void PuzzleComplete(bool completed)
  {
    puzzleEffect.Solved(!completed);
  }
}
