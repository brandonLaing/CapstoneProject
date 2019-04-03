using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlatformGun : MonoBehaviour
{
  #region Variables
  public Transform cameraTransform;

  public Transform projectionLocation;

  public Queue<Transform> platformQue = new Queue<Transform>();

  public float minPlatformRange, maxPlatformRange;
  public float _platformRange = 10;

  public LayerMask layerMask;

  public float PlatformRange
  {
    get
    {
      return _platformRange;
    }
    set
    {
      if (value >= minPlatformRange && value <= maxPlatformRange)
        _platformRange = value;
    }
  }

  private GunState _gunState = GunState.Standby;
  public GunState CurrentGunState
  {
    get
    {
      return _gunState;
    }
    set
    {
      _gunState = value;
      if (_gunState == GunState.Standby)
        projectionLocation.gameObject.SetActive(false);
      if (_gunState == GunState.Triggered)
        projectionLocation.gameObject.SetActive(true);
    }
  }

  public ProjectionType projectionType;

  public Material[] materials;
  public GameObject[] platformPrefabs;

  public float scrollMultiplier;

  public GameObject currentMovingPlatform;
  #endregion

  private void Start()
  {
    CurrentGunState = GunState.Standby;
  }

  private void Update()
  {
    switch (CurrentGunState)
    {
      case GunState.Standby:
        StandbyStateLogic();
        break;
      case GunState.Triggered:
        TriggeredStateLogic();
        break;
      case GunState.PlaceingMovingEndPoint:
        PlaceingEndPoint();
        break;
    }
  }

  private void StandbyStateLogic()
  {
    if (Input.GetMouseButtonDown(0))
    {
      CurrentGunState = GunState.Triggered;
    }
  }

  /// <summary>
  /// All the logic while the gun is in triggered mode
  /// </summary>
  private void TriggeredStateLogic()
  {
    // check to put the gun back into standby
    if (Input.GetMouseButtonDown(0))
      CurrentGunState = GunState.Standby;

    // check for the zoom of the gun

    // check if the type of platform has changed
    CyclingCheck();

    if (Input.GetKeyDown(KeyCode.Alpha1))
      projectionType = ProjectionType.StaticPlatform;
    if (Input.GetKeyDown(KeyCode.Alpha2))
      projectionType = ProjectionType.SpeedPlatform;
    if (Input.GetKeyDown(KeyCode.Alpha3))
      projectionType = ProjectionType.JumpPlatform;
    if (Input.GetKeyDown(KeyCode.Alpha4))
      projectionType = ProjectionType.MovingPlatform;

    // update the material of the platform
    projectionLocation.GetComponent<MeshRenderer>().material = materials[(int)projectionType];

    UpdateProjectionLocation();

    // instantiate platform
    if (Input.GetMouseButtonDown(1))
    {
      GameObject newObject = Instantiate(platformPrefabs[(int)projectionType], projectionLocation.position, projectionLocation.rotation);
      newObject.name += System.DateTime.Now.Second;

      if (projectionType == ProjectionType.MovingPlatform)
      {
        GameObject parent = newObject;
        Transform child = newObject.transform.GetChild(0);
        child.parent = null;
        Destroy(parent.gameObject);

        CurrentGunState = GunState.PlaceingMovingEndPoint;
        currentMovingPlatform = child.gameObject;
      }
      else
      {
        CheckNumberOfPlatforms(newObject.transform);
      }
    }
  }

  private void UpdateProjectionLocation()
  {
    PlatformRange += (Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime) * scrollMultiplier;

    // set the transform of the projection
    RaycastHit hit;
    if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, PlatformRange, layerMask))
    {
      projectionLocation.position = hit.point;
    }
    else
      projectionLocation.position = cameraTransform.position + cameraTransform.forward * PlatformRange;


    // update the rotation to the players angle
    Vector3 newRotation = transform.eulerAngles;
    projectionLocation.eulerAngles = newRotation;
  }

  /// <summary>
  /// Checks for platform Cycling with Q and E
  /// </summary>
  private void CyclingCheck()
  {
    int currentState = (int)projectionType;

    if (Input.GetKeyDown(KeyCode.Q))
    {
      currentState--;
      if (currentState < 0)
        currentState = (int)ProjectionType.Length - 1;

    }
    if (Input.GetKeyDown(KeyCode.E))
    {
      currentState++;
      if (currentState >= (int)ProjectionType.Length)
        currentState = 0;
    }

    projectionType = (ProjectionType)currentState;
  }

  private void PlaceingEndPoint()
  {
    UpdateProjectionLocation();

    if (Input.GetMouseButtonDown(1))
    {
      currentMovingPlatform.GetComponent<PlatformMoving>().AddEndPoint(projectionLocation.transform.position);
      CurrentGunState = GunState.Triggered;

      CheckNumberOfPlatforms(currentMovingPlatform.transform);
    }

    if (Input.GetMouseButtonDown(0))
    {
      Destroy(currentMovingPlatform);
      CurrentGunState = GunState.Standby;
    }
  }

  private void CheckNumberOfPlatforms(Transform newPlatform)
  {
    if (platformQue.Count >= 3)
    {
      //Debug.Log("Starting to remove something from the que");
      Transform objectToDestroy = platformQue.Dequeue();

      while (objectToDestroy.childCount > 0)
        for (int i = 0; i < objectToDestroy.childCount; i++)
        {
          //Debug.Log("Destroying children");

          objectToDestroy.GetChild(i).parent = null;
        }

      //Debug.Log("Destroying platform " + objectToDestroy.name);
      Destroy(objectToDestroy.gameObject);
    }

    //Debug.Log($"Adding {newPlatform.name} to the que");
    platformQue.Enqueue(newPlatform);
  }
}
