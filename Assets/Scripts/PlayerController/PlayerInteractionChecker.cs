using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// @author Brandon Laing

/// <summary>
/// This is be the controller for when the player interacts with certain things
/// </summary>
public class PlayerInteractionChecker : MonoBehaviour
{
  /// <summary>
  /// The camera for the player controller
  /// </summary>
  [Tooltip("The camera for the player controller")]
  public Transform cameraTransform;

  /// <summary>
  /// Range the the player will be able to interact with things
  /// </summary>
  [Tooltip("Range the the player will be able to interact with things")]
  public float range;

  /// <summary>
  /// Layer the raycast will be shot on
  /// </summary>
  [Tooltip("Layer the raycast will be shot on")]
  public LayerMask targetlayer;

  /// <summary>
  /// Checks if the players raycast hit this frame
  /// </summary>
  private bool hitThisFrame;

  private void Update()
  {
    hitThisFrame = false;
    RaycastHit hit;

    if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, range, targetlayer))
    {
      hitThisFrame = true;
      if (Input.GetKeyDown(KeyCode.F))
      {
        if (hit.transform.GetComponent<IInteractable>() != null)
          hit.transform.GetComponent<IInteractable>().Interact();
        else if (hit.transform.GetComponentInParent<IInteractable>() != null)
          hit.transform.GetComponentInParent<IInteractable>().Interact();
      }
    }
  }

  private void OnDrawGizmos()
  {
    if (UnityEditor.EditorApplication.isPlaying)
    {
      if (hitThisFrame)
        Gizmos.color = Color.red;
      else
        Gizmos.color = Color.green;

      Gizmos.DrawLine(cameraTransform.position, cameraTransform.position + cameraTransform.forward * range);
    }
  }
}
