using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class PlayerInputManager : MonoBehaviour
{
  public event Action OnGunTriggered = delegate { };
  public event Action OnGunShot = delegate { };
  public event Action<float> OnGunRangeChanged = delegate { };

  public event Action<Vector3> OnMove = delegate { };
  public event Action OnJumpPressed = delegate { };
  public event Action OnJumpHeld = delegate { };

  public event Action<Vector2> OnLook = delegate { };

  public event Action OnNextPlatform = delegate { };
  public event Action OnPreviousPlatform = delegate { };

  public event Action OnPlatformOneSelected = delegate { };
  public event Action OnPlatformTwoSelected = delegate { };
  public event Action OnPlatformThreeSelected = delegate { };
  public event Action OnPlatformFourSelected = delegate { };

  [SerializeField]
  private KeyHolder keys = new KeyHolder();

  private void Update()
  {
    GetMovement();
    GetGunActions();
    CyclePlatforms();
    SelectPlatform();
  }

  private void GetMovement()
  {
    Vector3 moveDirection = Vector3.zero;

    if (Input.GetKey(keys.forwardMovement))
      moveDirection += Vector3.forward;
    if (Input.GetKey(keys.backMovement))
      moveDirection += Vector3.back;
    if (Input.GetKey(keys.leftMovement))
      moveDirection += Vector3.left;
    if (Input.GetKey(keys.rightMovement))
      moveDirection += Vector3.right;

    if (moveDirection != Vector3.zero)
      OnMove(moveDirection);

    if (Input.GetKeyDown(keys.jumpKey))
      OnJumpPressed();
    if (Input.GetKey(keys.jumpKey))
      OnJumpHeld();
  }

  private void GetGunActions()
  {
    if (Input.GetMouseButtonDown(0))
      OnGunTriggered();
    if (Input.GetMouseButtonDown(1))
      OnGunShot();

    OnGunRangeChanged(Input.GetAxis("Mouse ScrollWheel"));
  }

  private void CyclePlatforms()
  {
    if (Input.GetKeyDown(keys.nextPlatform))
      OnNextPlatform();
    if (Input.GetKeyDown(keys.previousPlatform))
      OnPreviousPlatform();
  }

  private void SelectPlatform()
  {
    if (Input.GetKeyDown(keys.firstPlatform))
      OnPlatformOneSelected();
    if (Input.GetKeyDown(keys.secondPlatform))
      OnPlatformTwoSelected();
    if (Input.GetKeyDown(keys.thirdPlatform))
      OnPlatformThreeSelected();
    if (Input.GetKeyDown(keys.fourthPlatform))
      OnPlatformFourSelected();
  }
}

[Serializable]
public class KeyHolder
{
  public KeyCode forwardMovement = KeyCode.W;
  public KeyCode backMovement = KeyCode.S;
  public KeyCode leftMovement = KeyCode.A;
  public KeyCode rightMovement = KeyCode.D;

  public KeyCode jumpKey = KeyCode.Space;

  public KeyCode nextPlatform = KeyCode.E;
  public KeyCode previousPlatform = KeyCode.Q;

  public KeyCode firstPlatform = KeyCode.Alpha1;
  public KeyCode secondPlatform = KeyCode.Alpha2;
  public KeyCode thirdPlatform = KeyCode.Alpha3;
  public KeyCode fourthPlatform = KeyCode.Alpha4;
}