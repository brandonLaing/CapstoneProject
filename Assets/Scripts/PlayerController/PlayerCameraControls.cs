using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraControls : MonoBehaviour
{
  [Tooltip("Rotations speeds of the mouse look")]
  [Range(50, 200)]
  public float cameraXSpeed = 150, cameraYSpeed = 150;

  [Tooltip("If the mouse should invert its look")]
  public bool invertX, invertY = true;

  /// <summary>
  /// Min and max view the camera can go in
  /// </summary>
  private readonly float minView = -90F, maxView = 90F;

  /// <summary>
  /// Reference to the players childed camera
  /// </summary>
  private Transform playerCamera;

  /// <summary>
  /// Direction from the input manager to look at
  /// </summary>
  private Vector2 lookDirection;


  private void Awake()
  {
    playerCamera = GetComponentInChildren<Camera>().transform;
    GetComponent<PlayerInputManager>().OnLook += UpdateLook;
    Cursor.lockState = CursorLockMode.Locked;
  }

  public void UpdateLook(Vector2 lookDir)
  {
    #region X
    var currentX = lookDir.x;

    if (invertX)
      currentX *= -1;

    transform.Rotate(Vector3.up, currentX * cameraXSpeed * Time.deltaTime);
    #endregion

    #region Y
    var mouseY = lookDir.y;

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

  private void OnDestroy()
  {
    GetComponent<PlayerInputManager>().OnLook -= UpdateLook;
  }
}