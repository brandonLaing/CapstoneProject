using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformProjectorScript : MonoBehaviour
{
  /** PlatformProjectorScript Does:
   * Author: Brandon Laing
   * This will display a platform in front of the object its attached to and lets the player choose between a few
   * platforms
   */

  #region Variables
  public GameObject[] platforms;

  public Material[] platformMaterials;

  public GameObject projectionLocation;

  public GameObject fakePlatform;

  public GameObject currentPlatform;
  [Header("Make this the same as ammo count for the level")]
  public List<GameObject> listOfPlatforms = new List<GameObject>();

  public int bulletCount = 50;

  public float defaultPlatformRange = 10;
  public float minRange = 5;
  public float maxRange = 15;

  public bool debugging = false;

  private int currentPlatformTypeInt = 0;
  private bool preSpawing = true;
  private bool editSpawning = false;
  private float scrollWheel;
  private float platformRange;
  private int desiredPlatform;

  public float controllerSlowDonw;

  //public PauseMenuController UIScript;

  #endregion

  #region EnableDebugging variables
  private string debuggingLock = "19573";
  private string debuggingKey = null;

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

  public bool dPadXChange;
  public bool dPadYChange;

  #endregion

  // doesn't really do shit
  private void Awake()
  {
    platformRange = defaultPlatformRange;

  } // end Awake

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

    if (Input.GetButtonDown("aButton"))
    {
      aButton = true;

    } // if button down A

    else
    {
      aButton = false;

    }

    if (Input.GetButtonDown("bButton"))
    {
      bButton = true;

    } // if button down A

    else
    {
      bButton = false;

    }

    if (Input.GetButtonDown("xButton"))
    {
      xButton = true;

    } // if button down A

    else
    {
      xButton = false;

    }


    if (Input.GetButtonDown("yButton"))
    {
      yButton = true;

    } // if button down A

    else
    {
      yButton = false;

    }

    if (Input.GetButtonDown("LeftBumper"))
    {
      leftBumper = true;

    } // if button down A

    else
    {
      leftBumper = false;

    }

    if (Input.GetButtonDown("RightBumper"))
    {
      rightBumper = true;

    } // if button down A

    else
    {
      rightBumper = false;

    }

    if (Input.GetButtonDown("BackButton"))
    {
      backButton = true;

    } // if button down A

    else
    {
      backButton = false;

    }

    if (Input.GetButtonDown("StartButton"))
    {
      startButton = true;

    } // if button down A

    else
    {
      startButton = false;

    }

    if (Input.GetButtonDown("LeftStickClick"))
    {
      leftStickClick = true;

    } // if button down A

    else
    {
      leftStickClick = false;

    }

    if (Input.GetButtonDown("RightStickClick"))
    {
      rightStickClick = true;

    } // if button down A

    else
    {
      rightStickClick = false;

    }

    #endregion

    // set the position of the target location
    projectionLocation.transform.position = gameObject.transform.position + gameObject.transform.forward * platformRange;

    // if there 
    if (currentPlatform != null)
    {
      currentPlatform.transform.position = projectionLocation.transform.position;
      currentPlatform.transform.forward = projectionLocation.transform.forward;

      #region DesiredPlatformChoice and Display
      // sets the platform you want to use
      if (Input.GetKeyDown(KeyCode.Alpha1))
      {
        desiredPlatform = 0;

      }

      if (Input.GetKeyDown(KeyCode.Alpha2))
      {
        desiredPlatform = 1;
      }

      if (Input.GetKeyDown(KeyCode.Alpha3))
      {
        desiredPlatform = 2;

      }

      if (Input.GetKey(KeyCode.Alpha4))
      {
        desiredPlatform = 3;

      }

      if (dPadXChange)
      {
        if (dPad.x > .8)
        {
          desiredPlatform++;
          dPadXChange = false;

        }

        if (dPad.x < -.8)
        {
          desiredPlatform--;
          dPadXChange = false;

        }

      }

      if (dPad.x == 0)
      {
        dPadXChange = true;
      }

      if (desiredPlatform < 0)
      {
        desiredPlatform = 3;

      }

      if (desiredPlatform > 3)
      {
        desiredPlatform = 0;

      }

      currentPlatform.transform.GetChild(0).GetComponent<MeshRenderer>().material = platformMaterials[desiredPlatform];

    }

    #endregion

    #region SpawingPlatform
    //if (bulletCount > 0 && !UIScript.gameIsPaused)
    {
      if (preSpawing)
      {
        if (Input.GetKeyUp(KeyCode.Mouse0) || Input.GetButtonUp("LeftBumper"))
        {
          preSpawing = false;
          currentPlatform = Instantiate(fakePlatform);
          editSpawning = true;

        } // end get key left click

      } // end pres pawing

      if (editSpawning)
      {
        // rotates the platform
        if ((!Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Mouse1)) || xButton)
        {
          projectionLocation.transform.Rotate(0, -90, 0);

        }

        if ((Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Mouse1)) || bButton)
        {
          projectionLocation.transform.Rotate(0, 90, 0);

        }

        // spawns in the new platform
        if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetButtonDown("LeftBumper"))

        {
          Destroy(currentPlatform);
          listOfPlatforms.Add(Instantiate(platforms[desiredPlatform], projectionLocation.transform.position + new Vector3(0,.5f,0), projectionLocation.transform.rotation));

          if (!debugging)
          {
            bulletCount--;

          }

          editSpawning = false;
          preSpawing = true;

        }

        // exits spawning sequence 
        if (Input.GetKeyDown(KeyCode.Tab) || backButton)
        {
          Destroy(currentPlatform);
          editSpawning = false;
          preSpawing = true;
        }
      }
    }

    #endregion

    #region Scroll wheel
    platformRange += (Input.GetAxis("Mouse ScrollWheel") + (dPad.y * controllerSlowDonw))  * 1.5F;

    platformRange = Mathf.Clamp(platformRange, minRange, maxRange);


    #endregion

    #region EnableDebugging
    if (!debugging)
    {
      if (Input.GetKeyDown(KeyCode.Keypad1))
      {
        debuggingKey += "1";

      }

      if (Input.GetKeyDown(KeyCode.Keypad3))
      {
        debuggingKey += "3";

      }

      if (Input.GetKeyDown(KeyCode.Keypad5))
      {
        debuggingKey += "5";

      }

      if (Input.GetKeyDown(KeyCode.Keypad7))
      {
        debuggingKey += "7";

      }

      if (Input.GetKeyDown(KeyCode.Keypad9))
      {
        debuggingKey += "9";

      }

      if (Input.GetKeyDown(KeyCode.KeypadEnter))
      {
        if (debuggingKey == debuggingLock)
        {
          debugging = true;
          debuggingKey = null;

        }

        else
        {
          debuggingKey = null;
        }
      }

    } // end unlock debugging

    if (bulletCount < 0)
    {
      bulletCount = 1;

    }
    #endregion



  }

  public void ResetList()
  {
    listOfPlatforms = new List<GameObject>();

  }

  public void DestroyObjectsInList()
  {
    foreach (GameObject plat in listOfPlatforms)
    {
      Destroy(plat);

    }
  }


} // end PlatformProjectorScipt
