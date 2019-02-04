using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatFormErPlayerController : MonoBehaviour
{
  /** PlatFormErPlayerController Does:
   * Author: Brandon Laing
   * This will take care of the player controller for the platformer game
   * The fetures will be:
   * moving forward backward left and right
   * jumping
   * double jumping
   * wall jumping
   * fuck
   */

  #region Variables
  public float Speed;

  public float jumpVelocity;

  public float cameraXAxisRotationSpeed;
  public float cameraYAxisRotationSpeed;

  public float minView = -90.0F;
  public float maxView = 90.0F;

  public bool invertLook;

  private int invertInt;

  private float mouseY;

  public Transform playerCamera;

  private TouchingGround touchingGroundScript;

  private Rigidbody playerRigidbody;

  #endregion

  #region ControllerVariables
  [Header("Controller Variables")]
  public Vector3 leftJoystick;
  public Vector3 rightJoystick;
  public Vector2 dPad;

  public float leftTrigger;
  public float rightTrigger;

  public bool aButton;
  public bool bButton;
  public bool xButton;
  public bool yButton;

  public bool leftBumper;
  public bool rightBumper;
  public bool backButton;
  public bool startButton;

  public bool leftStickClick;
  public bool rightStickClick;
  #endregion

  private void Awake()
  {
    // Basic SetUp
    //playerCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>();

    touchingGroundScript = GetComponent<TouchingGround>();

    playerRigidbody = GetComponent<Rigidbody>();

        playerRigidbody.freezeRotation = true;

    transform.tag = "Player";

    Cursor.lockState = CursorLockMode.Locked;

  }

  private void Update()
  {
    #region Controls
    leftJoystick = Vector3.zero;
    leftJoystick.x = Input.GetAxis("LeftStickX");
    leftJoystick.z = Input.GetAxis("LeftStickY");

    rightJoystick = Vector3.zero;
    rightJoystick.x = Input.GetAxis("RightStickX");
    rightJoystick.z = Input.GetAxis("RightStickY");

    dPad = Vector2.zero;
    dPad.x = Input.GetAxis("DPadX");
    dPad.y = Input.GetAxis("DPadY");

    rightTrigger = Input.GetAxis("RightTrigger");
    leftTrigger = Input.GetAxis("LeftTrigger");

    if (Input.GetButton("aButton"))
    {
      aButton = true;

    } // if button down A

    else
    {
      aButton = false;

    }

    if (Input.GetButton("bButton"))
    {
      bButton = true;

    } // if button down A

    else
    {
      bButton = false;

    }

    if (Input.GetButton("xButton"))
    {
      xButton = true;

    } // if button down A

    else
    {
      xButton = false;

    }


    if (Input.GetButton("yButton"))
    {
      yButton = true;

    } // if button down A

    else
    {
      yButton = false;

    }

    if (Input.GetButton("LeftBumper"))
    {
      leftBumper = true;

    } // if button down A

    else
    {
      leftBumper = false;

    }

    if (Input.GetButton("RightBumper"))
    {
      rightBumper = true;

    } // if button down A

    else
    {
      rightBumper = false;

    }

    if (Input.GetButton("BackButton"))
    {
      backButton = true;

    } // if button down A

    else
    {
      backButton = false;

    }

    if (Input.GetButton("StartButton"))
    {
      startButton = true;

    } // if button down A

    else
    {
      startButton = false;

    }

    if (Input.GetButton("LeftStickClick"))
    {
      leftStickClick = true;

    } // if button down A

    else
    {
      leftStickClick = false;

    }

    if (Input.GetButton("RightStickClick"))
    {
      rightStickClick = true;

    } // if button down A

    else
    {
      rightStickClick = false;

    }

    #endregion

    #region WASD Movement
    Vector3 movedirection = Vector3.zero;

    // get move direction
    if (Input.GetKey(KeyCode.W))
    {
      movedirection += transform.forward;

    }

    if (Input.GetKey(KeyCode.S))
    {
      movedirection -= transform.forward;

    }

    if (Input.GetKey(KeyCode.A))
    {
      movedirection -= transform.right;

    }

    if (Input.GetKey(KeyCode.D))
    {
      movedirection += transform.right;

    }

    if (leftJoystick.z > 0)
    {
      movedirection += transform.forward;

    }

    if (leftJoystick.z < 0)
    {
      movedirection -= transform.forward;

    }

    if (leftJoystick.x < 0)
    {
      movedirection -= transform.right;

    }

    if (leftJoystick.x > 0)
    {
      movedirection += transform.right;

    }

    // move in that direction
    transform.position += movedirection.normalized * Speed * Time.deltaTime;

    #endregion

    #region Camera Controlls
    // rotate around the X axis
    transform.Rotate(Vector3.up, (Input.GetAxis("Mouse X") + rightJoystick.x)* cameraXAxisRotationSpeed * Time.deltaTime);

    // rotate around the Y axis
    mouseY = Input.GetAxis("Mouse Y") + rightJoystick.z;

    float angleEulerLimit = playerCamera.transform.eulerAngles.x;

    if (angleEulerLimit > 180)
    {
      angleEulerLimit -= 360;

    }

    if (angleEulerLimit < -180)
    {
      angleEulerLimit += 360;

    }

    #region Invert Look
    if (invertLook)
    {
      invertInt = 1;

    }

    else
    {
      invertInt = -1;
    }

    #endregion

    float targetRotation = angleEulerLimit + mouseY * cameraYAxisRotationSpeed * Time.deltaTime * invertInt;

    if (targetRotation < maxView && targetRotation > minView)
    {
      playerCamera.transform.eulerAngles += new Vector3(mouseY * cameraYAxisRotationSpeed * Time.deltaTime * invertInt, 0, 0);

    }
    #endregion

    #region Jump
    if ((Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("aButton")) && touchingGroundScript.touchingGround)
    {
      playerRigidbody.velocity += transform.up * jumpVelocity;

    }
    #endregion

  }
}
