using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpControls : MonoBehaviour
{
  /// <summary>
  /// The force that is added onto the player for jumps
  /// </summary>
  [Tooltip("The force that is added onto the player for jumps")]
  [Range(2, 10)]
  public float jumpVelocity;

  /// <summary>
  /// The ammount of force that is added onto the player as they fall
  /// </summary>
  [Tooltip("The amount of force that is added onto the player as they fall")]
  [Range(1, 5)]
  public float fallMultiplier = 2.5F;

  /// <summary>
  /// The amount of force being added while jumping to slow down jump
  /// </summary>
  [Tooltip("The amount of force being added while jumping to slow down jump")]
  public float lowJumpMultiplier = 2F;

  /// <summary>
  /// Character controllers ground checker
  /// </summary>
  private PlayerGroundChecker groundChecker;

  /// <summary>
  /// Current request from the player to jump
  /// </summary>
  private bool doJump = false;

  /// <summary>
  /// Reference to the objects rigidbody
  /// </summary>
  private Rigidbody rb;

  private void Awake()
  {
    groundChecker = GetComponent<PlayerGroundChecker>();
    rb = GetComponent<Rigidbody>();
    rb.freezeRotation = true;
  }

  private void Update()
  {
    if (Input.GetKeyDown(KeyCode.Space) && groundChecker.IsTouchingGround)
      doJump = true;
  }

  private void FixedUpdate()
  {
    Jump();
    Fall();
  }

  /// <summary>
  /// Logic for making the player jump
  /// </summary>
  private void Jump()
  {
    if (doJump)
    {
      doJump = false;
      rb.AddForce(transform.up * jumpVelocity, ForceMode.Impulse);
    }
  }

  /// <summary>
  /// A little bit of code that makes the players jump feel better
  /// </summary>
  private void Fall()
  {
    if (rb.velocity.y < 0)
      rb.velocity += (Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime);
    else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
      rb.velocity += (Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.fixedDeltaTime);
  }
}
