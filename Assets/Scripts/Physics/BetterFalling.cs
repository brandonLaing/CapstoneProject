using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BetterFalling : MonoBehaviour
{
  public float fallMultiplier = 2.5F;

  public bool isPlayer = false;
  private bool isJumping = false;
  private float lowJumpMultiplier = 2F;

  private Rigidbody rb;

  private void Awake()
  {
    rb = GetComponent<Rigidbody>();
    GetComponent<PlayerInputManager>().OnJumpHeld += CheckForJumpHeld;
  }

  public void CheckForJumpHeld()
  {
    isJumping = true;
  }

  private void FixedUpdate()
  {
    if (rb.velocity.y < 0)
      rb.velocity += (Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime);
    else if (rb.velocity.y > 0 && isPlayer && isJumping)
    {
      rb.velocity += (Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.fixedDeltaTime);
      Debug.Log("Did low Jump");
    }
    isJumping = false;
  }
}
