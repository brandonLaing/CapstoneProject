using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BetterFalling : MonoBehaviour
{
  public float fallMultiplier = 2.5F;

  public bool isPlayer = false;

  public float lowJumpMultiplier = 2F;

  private bool _isJumping = false;
  public bool IsJumping
  {
    get { return _isJumping; }
    private set { _isJumping = value; }
  }

  private Rigidbody rb;

  private void Awake()
  {
    rb = GetComponent<Rigidbody>();

    if (isPlayer)
    {
      if (GetComponent<PlayerInputManager>() != null)
        GetComponent<PlayerInputManager>().OnJumpHeld += CheckForJumpHeld;
      else
      {
        Debug.LogError($"{transform.name} is set to player but has no input manager. Setting to non player");
        isPlayer = false;
      }
    }
  }

  public void CheckForJumpHeld()
  {
    IsJumping = true;
  }

  private void FixedUpdate()
  {
    if (rb.velocity.y < 0)
    {
      rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
    }
    else if (rb.velocity.y > 0 && isPlayer && !IsJumping)
    {
      rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.fixedDeltaTime;
    }
    IsJumping = false;
  }

  private void OnDestroy()
  {
    if (isPlayer)
      GetComponent<PlayerInputManager>().OnJumpHeld -= CheckForJumpHeld;
  }
}
