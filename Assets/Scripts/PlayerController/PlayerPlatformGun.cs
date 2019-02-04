using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlatformGun : MonoBehaviour
{
  public Transform cameraTransform;

  public Transform projectionLocation;
  public bool showProjection;

  public float platformRange = 10;

  private void Update()
  {
    if (Input.GetMouseButtonDown(0))
      showProjection = !showProjection;

    if (showProjection)
    {
      projectionLocation.gameObject.SetActive(true);
      projectionLocation.position = cameraTransform.position + (cameraTransform.forward * platformRange);
    }

    else
      projectionLocation.gameObject.SetActive(false);

  }
}
