using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpControls : MonoBehaviour
{
  [Tooltip("The force that is added onto the player for jumps")]
  [Range(2, 10)]
  public float jumpVelocity = 5;

  /// <summary>
  /// Check for jump
  /// </summary>
  private bool doJump = false;
  /// <summary>
  /// Check for touching ground
  /// </summary>
  private bool isTouchingGround = false;

  /// <summary>
  /// Reference to the objects rigidbody
  /// </summary>
  private Rigidbody rb;

  private void Awake()
  {
    GetComponent<PlayerGroundChecker>().OnTouchingGround += SetTouchedGround;
    GetComponent<PlayerInputManager>().OnJumpPressed += SetDoJump;

    rb = GetComponent<Rigidbody>();
  }

  private void FixedUpdate()
  {
    if (doJump && isTouchingGround)
    {
      Jump();
    }

    doJump = false;
    isTouchingGround = false;
  }

  /// <summary>
  /// Logic for making the player jump
  /// </summary>
  private void Jump() => rb.AddForce(transform.up * jumpVelocity, ForceMode.Impulse);

  private void SetTouchedGround() => isTouchingGround = true;
  private void SetDoJump() => doJump = true;

  private void OnDestroy()
  {
    GetComponent<PlayerGroundChecker>().OnTouchingGround -= SetTouchedGround;
    GetComponent<PlayerInputManager>().OnJumpPressed -= SetDoJump;
  }
}
