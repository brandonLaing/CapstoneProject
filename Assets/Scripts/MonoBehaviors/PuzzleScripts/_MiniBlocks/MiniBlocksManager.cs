using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBlocksManager : MonoBehaviour
{
  public Activator activator;
  public PuzzleEffect effect;
  public void CheckPassed()
  {
    activator.Lock();
    effect.Solved(false);
  }
}
