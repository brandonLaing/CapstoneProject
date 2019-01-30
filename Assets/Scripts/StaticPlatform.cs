using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticPlatform : MonoBehaviour
{
  /** Static Platform Does:
   * Author: Brandon Laing
   * I mean its a static platform what do you want it to do?
   * Never mind i cant believe I'm actually writing code for this stupid piece of
   */
  #region Variables
  public Material starticPlatformMaterial;

  #endregion

  // reset platform material
  private void Awake()
  {
    GetComponent<MeshRenderer>().material = starticPlatformMaterial;

  } // end Awake

} // end StaticPlatform
