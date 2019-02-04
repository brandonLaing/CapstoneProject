using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravetyWellCollider : MonoBehaviour
{
  /** GravetyWellCollider Does:
   * Author: Brandon Laing
   * When player is in the collider it lets the gravity well know
   */

  #region Variables
  public bool playerColliding;
  public string playerTag = "Player";

  #endregion

  // honestly if you cant understand this thats kinda sad
  private void OnTriggerStay(Collider other)
  {
    if (other.transform.tag == playerTag)
    {
      playerColliding = true;

    }

  }

  private void OnTriggerExit(Collider other)
  {
    if (other.transform.tag == playerTag)
    {
      playerColliding = false;

    }

  }

}
