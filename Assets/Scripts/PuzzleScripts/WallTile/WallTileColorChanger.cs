using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallTileColorChanger : MonoBehaviour
{
  /// <summary>
  /// Pointers on the wall tile
  /// </summary>
  [Tooltip("Pointers on the wall tile mesh renderer")]
  public MeshRenderer[] pointersRenderer;

  public Material successMat, failMat;

  public void CheckPassed(bool passed)
  {
    Material applyingMat;
    if (passed)
      applyingMat = successMat;
    else
      applyingMat = failMat;

    for (int i = 0; i < pointersRenderer.Length; i++)
    {
      pointersRenderer[i].material = applyingMat;
    }
  }
}
