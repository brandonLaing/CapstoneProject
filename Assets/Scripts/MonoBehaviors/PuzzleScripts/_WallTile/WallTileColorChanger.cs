using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallTileColorChanger : MonoBehaviour
{
  /// <summary>
  /// Pointers on the wall tile
  /// </summary>
  [Tooltip("Pointers on the wall tile mesh renderer")]
  public MeshRenderer[] pointersRenderer, flowRenderer;

  public Material successMat, failMat;

  public Material flowOff, flowOn;

  public void CheckPassed(bool passed)
  {
    Material applyingWheelMat;
    Material applyingFlowMat;

    if (passed)
    {
      applyingWheelMat = successMat;
      applyingFlowMat = flowOn;
    }
    else
    {
      applyingWheelMat = failMat;
      applyingFlowMat = flowOff;
    }

    for (int i = 0; i < pointersRenderer.Length; i++)
    {
      pointersRenderer[i].material = applyingWheelMat;
    }

    for (int i = 0; i < flowRenderer.Length; i++)
      flowRenderer[i].material = applyingFlowMat;
  }
}
