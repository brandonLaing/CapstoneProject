using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundChecker : MonoBehaviour
{
  /// <summary>
  /// List of objects bellow the player to stand on
  /// </summary>
  private List<GameObject> groundObjects = new List<GameObject>();

  /// <summary>
  /// Tells if the player has something bellow them to stand on
  /// </summary>
  public bool IsTouchingGround
  {
    get { return groundObjects.Count != 0; }
  }

  private void Update()
  { 
    for (int i = 0; i < groundObjects.Count; i++)
    {
      if (groundObjects[i] == null)
      {
        groundObjects.Remove(groundObjects[i]);
      }
      else if (!groundObjects[i].activeSelf)
      {
        groundObjects.Remove(groundObjects[i]);
        return;
      }
    }
  }

  private void OnTriggerEnter(Collider other)
  {
    if (other.gameObject.layer == 9)
      groundObjects.Add(other.gameObject);
  }

  private void OnTriggerExit(Collider other)
  {
    if (groundObjects.Contains(other.gameObject))
      groundObjects.Remove(other.gameObject);
  }
}
