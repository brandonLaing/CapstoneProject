using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class PlayerGroundChecker : MonoBehaviour
{
  /// <summary>
  /// List of objects bellow the player to stand on
  /// </summary>
  public List<GameObject> groundObjects = new List<GameObject>();

  /// <summary>
  /// Delegate called when object is touching the ground
  /// </summary>
  public event Action OnTouchingGround = delegate { };

  /// <summary>
  /// Tells if the player has something bellow them to stand on
  /// </summary>
  private bool IsTouchingGround
  {
    get { return groundObjects.Count != 0; }
  }

  public LayerMask groundLayer = 9;
  public bool _isTouchingGround;

  private void Update()
  {
    ValidateGroundObjects();

    if (IsTouchingGround)
    {
      OnTouchingGround();
      _isTouchingGround = true;
    }
    else
      _isTouchingGround = false;
  }

  /// <summary>
  /// Goes though and checks all ground object to make sure they are valid
  /// </summary>
  private void ValidateGroundObjects()
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
        i--;
      }
    }
  }

  /// <summary>
  /// Adds object to ground objects if its on the ground layer
  /// </summary>
  /// <param name="other"></param>
  private void OnTriggerEnter(Collider other)
  {
    if (groundLayer.value == 1 << other.gameObject.layer)
      groundObjects.Add(other.gameObject);
  }

  /// <summary>
  /// Removes object from ground objects if ground objects contains it
  /// </summary>
  /// <param name="other"></param>
  private void OnTriggerExit(Collider other)
  {
    if (groundObjects.Contains(other.gameObject))
      groundObjects.Remove(other.gameObject);
  }
}
