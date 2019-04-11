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

  public event Action OnInteract = delegate { };

  [SerializeField]
  private KeyHolder keys = new KeyHolder();

  private void Update()
  {
    GetGunActions();
    GetMovement();
    GetCameraDirection();
    CyclePlatforms();
    SelectPlatform();
    InteractionCheck();
  }

  private void GetGunActions()
  {
    if (Input.GetMouseButtonDown(0))
      OnGunTriggered();
    if (Input.GetMouseButtonDown(1))
      OnGunShot();

    OnGunRangeChanged(Input.GetAxis("Mouse ScrollWheel"));
  }

  private void GetMovement()
  {
    Vector3 moveDirection = new Vector3();

    if (Input.GetKey(keys.forwardMovement))
      moveDirection += transform.forward;
    if (Input.GetKey(keys.backMovement))
      moveDirection -= transform.forward;
    if (Input.GetKey(keys.leftMovement))
      moveDirection -= transform.right;
    if (Input.GetKey(keys.rightMovement))
      moveDirection += transform.right;
    moveDirection.Normalize();

    float x = Input.GetAxis("AllLeftStickX"), y = Input.GetAxis("AllLeftStickY");
    moveDirection += (transform.forward * y) + (transform.right * x);

    OnMove(moveDirection);

    if (Input.GetKeyDown(keys.jumpKey) || Input.GetButtonDown("AllaButton"))
      OnJumpPressed();
    if (Input.GetKey(keys.jumpKey) || Input.GetButton("AllaButton"))
      OnJumpHeld();
  }

  private void GetCameraDirection()
  {
    Vector2 cameraDirection = new Vector2
    {
      x = Input.GetAxis("Mouse X") + Input.GetAxis("AllRightStickX"),
      y = Input.GetAxis("Mouse Y") + Input.GetAxis("AllRightStickY")
    };

    OnLook(cameraDirection);
  }

  private void CyclePlatforms()
  {
    if (Input.GetKeyDown(keys.nextPlatform) || Input.GetButtonDown("AllRightBumper"))
      OnNextPlatform();
    if (Input.GetKeyDown(keys.previousPlatform) || Input.GetButtonDown("AllLeftBumper"))
      OnPreviousPlatform();
  }

  private void SelectPlatform()
  {
    if (Input.GetKeyDown(keys.firstPlatform) || Input.GetAxis("AllDPadY") > 0.5F)
      OnPlatformOneSelected();
    if (Input.GetKeyDown(keys.secondPlatform) || Input.GetAxis("AllDPadY") < -0.5F)
      OnPlatformTwoSelected();
    if (Input.GetKeyDown(keys.thirdPlatform) || Input.GetAxis("AllDPadX") < -0.5F)
      OnPlatformThreeSelected();
    if (Input.GetKeyDown(keys.fourthPlatform) || Input.GetAxis("AllDPadX") > 0.5F)
      OnPlatformFourSelected();
  }

  private void InteractionCheck()
  {
    if (Input.GetKeyDown(keys.interact) || Input.GetButtonDown("AllbButton"))
      OnInteract();
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

  public KeyCode interact = KeyCode.F;
}