using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// @author Brandon Laing

/// <summary>
/// This is be the controller for when the player interacts with certain things
/// </summary>
public class PlayerInteractionChecker : MonoBehaviour
{
  [Tooltip("Range the the player will be able to interact with things")]
  [Range(1, 10)]
  public float range = 3;

  [Tooltip("Layer to raycast on")]
  public LayerMask targetlayer;

  /// <summary>
  /// Players viewing camera
  /// </summary>
  private Transform cameraTransform;

  private void Awake()
  {
    cameraTransform = GetComponentInChildren<Camera>().transform;
    GetComponent<PlayerInputManager>().OnInteract += TryToInteract;
  }

  public void TryToInteract()
  {
    Debug.Log("Sent interaction call");
    RaycastHit hit;

    if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, range, (int)targetlayer))
    {
      if (hit.transform.GetComponent<IInteractable>() != null)
        hit.transform.GetComponent<IInteractable>().Interact();
      else if (hit.transform.GetComponentInParent<IInteractable>() != null)
        hit.transform.GetComponentInParent<IInteractable>().Interact();
    }
  }

  private void OnDestroy()
  {
    GetComponent<PlayerInputManager>().OnInteract -= TryToInteract;
  }
}
