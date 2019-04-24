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

  public event Action OnStartPressedDown = delegate { };

  [SerializeField]
  private KeyHolder keys = new KeyHolder();

  private bool IsUsingController
  {
    get
    {
      return Input.GetJoystickNames().Length > 0;
    }
  }
  private void Update()
  {
    GetGunActions();
    GetMovement();
    GetCameraDirection();
    CyclePlatforms();
    SelectPlatform();
    InteractionCheck();

    if (Input.GetKeyDown(keys.start) || Input.GetButtonDown("AllStartButton"))
      OnStartPressedDown();
  }

  bool rightTriggerReset = true, leftTriggerReset = true;
  float xHeldTimer, yHeldTimer;
  private void GetGunActions()
  {
    if (IsUsingController)
    {
      if (Input.GetAxis("AllRightTrigger") < 0.2F) rightTriggerReset = true;
      if (Input.GetAxis("AllLeftTrigger") < 0.2F) leftTriggerReset = true;
    }

    if (Input.GetMouseButtonDown(0) || (IsUsingController ? (leftTriggerReset && Input.GetAxis("AllLeftTrigger") > 0.9F) : false))
    {
      OnGunTriggered();
      leftTriggerReset = false;
    }

    if (Input.GetMouseButtonDown(1) || (IsUsingController ? (rightTriggerReset && Input.GetAxis("AllRightTrigger") > 0.9F) : false))
    {
      OnGunShot();
      rightTriggerReset = false;
    }

    OnGunRangeChanged(
      Input.GetAxis("Mouse ScrollWheel") + 
      (IsUsingController ? (Input.GetButtonDown("AllyButton") ? 0.1F : 0) : 0) + 
      (IsUsingController ? (Input.GetButtonDown("AllxButton") ? -0.1F : 0): 0)
      );

    if (IsUsingController)
    {
      if (Input.GetButton("AllyButton")) xHeldTimer += Time.deltaTime;
      if (Input.GetButton("AllxButton")) yHeldTimer += Time.deltaTime;

      if (xHeldTimer > 0.5)
        OnGunRangeChanged(0.1F);
      if (yHeldTimer > 0.5)
        OnGunRangeChanged(-0.1F);

      if (Input.GetButtonUp("AllyButton")) xHeldTimer = 0;
      if (Input.GetButtonUp("AllxButton")) yHeldTimer = 0;

    }
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

    if (IsUsingController) moveDirection += (transform.forward * Input.GetAxis("AllLeftStickY")) + (transform.right * Input.GetAxis("AllLeftStickX"));

    OnMove(moveDirection);

    if (Input.GetKeyDown(keys.jumpKey) || (IsUsingController ? Input.GetButtonDown("AllaButton") : false))
      OnJumpPressed();
    if (Input.GetKey(keys.jumpKey) || (IsUsingController ? Input.GetButton("AllaButton") : false))
      OnJumpHeld();
  }

  private void GetCameraDirection()
  {
    Vector2 cameraDirection = new Vector2
    {
      x = Input.GetAxis("Mouse X") + (IsUsingController ? Input.GetAxis("AllRightStickX") : 0),
      y = Input.GetAxis("Mouse Y") + (IsUsingController ? Input.GetAxis("AllRightStickY") : 0)
    };

    OnLook(cameraDirection);
  }

  private void CyclePlatforms()
  {
    if (Input.GetKeyDown(keys.nextPlatform) || (IsUsingController ? Input.GetButtonDown("AllRightBumper") : false))
      OnNextPlatform();
    if (Input.GetKeyDown(keys.previousPlatform) || (IsUsingController ? Input.GetButtonDown("AllLeftBumper") : false))
      OnPreviousPlatform();
  }

  bool upDpadReset = false, downDpadReset = false, leftDpadReset = false, rightDpadRest = false;
  private void SelectPlatform()
  {
    if (IsUsingController)
    {
      if (!upDpadReset && Input.GetAxis("AllDPadY") < 0.2F) upDpadReset = true;
      if (!downDpadReset&& Input.GetAxis("AllDPadY") > -0.2F) downDpadReset = true;
      if (!leftDpadReset && Input.GetAxis("AllDPadX") < 0.2F) leftDpadReset = true;
      if (!rightDpadRest && Input.GetAxis("AllDPadX") > -0.2F) rightDpadRest = true;
    }

    if (Input.GetKeyDown(keys.firstPlatform) || (IsUsingController ? upDpadReset && Input.GetAxis("AllDPadY") > 0.8F : false))
    {
      OnPlatformOneSelected();
      if (IsUsingController) upDpadReset = false;
    }
    if (Input.GetKeyDown(keys.secondPlatform) || (IsUsingController ? downDpadReset && Input.GetAxis("AllDPadY") < -0.8F : false))
    {
      OnPlatformTwoSelected();
      if (IsUsingController) downDpadReset = false;
    }
    if (Input.GetKeyDown(keys.thirdPlatform) || (IsUsingController ? leftDpadReset && Input.GetAxis("AllDPadX") < -0.8F : false))
    {
      OnPlatformThreeSelected();
      if (IsUsingController) leftDpadReset = false;
    }
    if (Input.GetKeyDown(keys.fourthPlatform) || (IsUsingController ? rightDpadRest && Input.GetAxis("AllDPadX") > 0.8F : false))
    {
      OnPlatformFourSelected();
      if (IsUsingController) rightDpadRest = false;
    }
  }

  private void InteractionCheck()
  {
    if (Input.GetKeyDown(keys.interact) || (IsUsingController ? Input.GetButtonDown("AllbButton"): false))
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
  public KeyCode start = KeyCode.Escape;
}