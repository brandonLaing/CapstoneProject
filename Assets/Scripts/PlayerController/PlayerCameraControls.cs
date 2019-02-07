using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraControls : MonoBehaviour
{
  /// <summary>
  /// Rotation speed of the mouse look
  /// </summary>
  [Tooltip("Rotations speeds of the mouse look")]
  [Range(50, 200)]
  public float cameraXSpeed, cameraYSpeed;

  /// <summary>
  /// Lower and upper bound on the angle at which the player can look
  /// </summary>
  private readonly float minView = -90F, maxView = 90F;

  /// <summary>
  /// If the mouse should invert its look
  /// </summary>
  [Tooltip("If the mouse should invert its look")]
  public bool invertX, invertY;

  /// <summary>
  /// Reference to the players childed camera
  /// </summary>
  private Transform playerCamera;

  private void Awake()
  {
    playerCamera = GetComponentInChildren<Camera>().transform;
    Cursor.lockState = CursorLockMode.Locked;
  }

  private void Update()
  {
    #region X
    var currentX = Input.GetAxis("Mouse X");

    if (invertX)
      currentX *= -1;

    transform.Rotate(Vector3.up, currentX * cameraXSpeed * Time.deltaTime);
    #endregion

    #region Y
    var mouseY = Input.GetAxis("Mouse Y");

    var angleEulerLimit = playerCamera.transform.eulerAngles.x;

    if (angleEulerLimit > 180)
      angleEulerLimit -= 360;
    if (angleEulerLimit < -180)
      angleEulerLimit += 360;

    var invertYInt = 1;
    if (invertY)
      invertYInt = -1;

    var targetYRotation = angleEulerLimit + mouseY * cameraYSpeed * invertYInt * Time.deltaTime;

    if (targetYRotation < maxView && targetYRotation > minView)
      playerCamera.transform.eulerAngles += new Vector3(mouseY * cameraYSpeed * invertYInt * Time.deltaTime, 0, 0);
    #endregion
  }
}
