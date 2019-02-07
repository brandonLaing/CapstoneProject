using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerPlatformGun))]
[RequireComponent(typeof(PlayerCameraControls))]
[RequireComponent(typeof(PlayerJumpControls))]
[RequireComponent(typeof(PlayerGroundChecker))]
public class PlayerMovement : MonoBehaviour
{
  /// <summary>
  /// The players WASD movement speed
  /// </summary>'
  [Tooltip("The players WASD movement speed")]
  [Range(5, 20)]
  public float moveSpeed = 10;

  /// <summary>
  /// Current direction the player is trying to move in
  /// </summary>
  private Vector3 moveDirection = Vector3.zero;

  private void Update()
  {
    moveDirection = new Vector3();

    if (Input.GetKey(KeyCode.W))
      moveDirection += transform.forward;
    if (Input.GetKey(KeyCode.S))
      moveDirection -= transform.forward;
    if (Input.GetKey(KeyCode.D))
      moveDirection += transform.right;
    if (Input.GetKey(KeyCode.A))
      moveDirection -= transform.right;
  }

  private void FixedUpdate()
  {
    transform.position += moveDirection.normalized * moveSpeed * Time.fixedDeltaTime;
  }
}
