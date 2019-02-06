using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
  #region Variables
  [Header("Movement")]
  /// <summary>
  /// The players WASD movement speed
  /// </summary>'
  [Tooltip("The players WASD movement speed")]
  [Range(5,20)]
  public float moveSpeed = 10;

  /// <summary>
  /// The force that is added onto the player for jumps
  /// </summary>
  [Tooltip("The force that is added onto the player for jumps")]
  [Range(2,10)]
  public float jumpVelocity;

  /// <summary>
  /// The ammount of force that is added onto the player as they fall
  /// </summary>
  [Tooltip("The ammount of force that is added onto the player as they fall")]
  [Range(1, 5)]
  public float fallMultiplier = 2.5F;

  [Header("Camera")]
  /// <summary>
  /// Rotation speed of the mouse look
  /// </summary>
  [Tooltip("Rotations speeds of the mouse look")]
  [Range(50, 200)]
  public float cameraXSpeed, cameraYSpeed;

  /// <summary>
  /// Lower and upper bound on the angle at which the player can look
  /// </summary>
  private float minView = -90F, maxView = 90F;

  /// <summary>
  /// If the mouse should invert its look
  /// </summary>
  [Tooltip("If the mouse should invert its look")]
  public bool invertX, invertY;

  [Header("Components")]
  /// <summary>
  /// Reference to the players childed camera
  /// </summary>
  [Tooltip("Reference to the players childed camera")]
  public Transform playerCamera;

  /// <summary>
  /// Reference to the objects rigidbody
  /// </summary>
  private Rigidbody rb;

  /// <summary>
  /// Current direction the player is trying to move in
  /// </summary>
  private Vector3 moveDirection = Vector3.zero;

  /// <summary>
  /// Current request from the player to jump
  /// </summary>
  private bool doJump = false;

  /// <summary>
  /// List of objects bellow the player to stand on
  /// </summary>
  private List<GameObject> groundObjects = new List<GameObject>();

  /// <summary>
  /// Tells if the player has something bellow them to stand on
  /// </summary>
  public bool IsTouchingGround
  {
    get { return groundObjects.Count > 0; }
  }
  #endregion

  #region Start Functions
  private void Awake()
  {
    rb = GetComponent<Rigidbody>();
    rb.freezeRotation = true;
    Cursor.lockState = CursorLockMode.Locked;

    if (playerCamera == null)
    {
      Debug.LogError("You need a player camera attached to the player controller. Ill make one for now");
      if (GetComponentInChildren<Camera>() == null)
      {
        var tempCameraObj = new GameObject();
        tempCameraObj.AddComponent<Camera>(); tempCameraObj.AddComponent<AudioListener>();
        tempCameraObj.transform.parent = this.transform; tempCameraObj.name = "Player Camera";
        tempCameraObj.transform.position = this.transform.position + new Vector3(0, 0.5F, 0);
        playerCamera = tempCameraObj.transform;
      }
      else
        playerCamera = GetComponentInChildren<Camera>().transform;
      
    }
  }
  #endregion

  #region Update Functions
  private void Update()
  {
    CheckForJump();
    GetMoveDirection();
    CameraMovement();
  }

  private void GetMoveDirection()
  {
    moveDirection = new Vector3();

    if (Input.GetKey(KeyCode.W))
      moveDirection += transform.forward;
    if (Input.GetKey(KeyCode.S))
      moveDirection -= transform.forward;
    if (Input.GetKey(KeyCode.D))
      moveDirection += transform.right;
    if (Input.GetKey(KeyCode.A))
      moveDirection -= transform.right;
  }

  private void CameraMovement()
  {
    #region X
    var currentX = Input.GetAxis("Mouse X");

    if (invertX)
      currentX *= -1;

    transform.Rotate(Vector3.up, currentX * cameraXSpeed * Time.deltaTime);
    #endregion

    #region Y
    var mouseY = Input.GetAxis("Mouse Y");

    var angleEulerLimit = playerCamera.transform.eulerAngles.x;

    if (angleEulerLimit > 180)
      angleEulerLimit -= 360;
    if (angleEulerLimit < -180)
      angleEulerLimit += 360;

    var invertYInt = 1;
    if (invertY)
      invertYInt = -1;

    var targetYRotation = angleEulerLimit + mouseY * cameraYSpeed * invertYInt * Time.deltaTime;

    if (targetYRotation < maxView && targetYRotation > minView)
      playerCamera.transform.eulerAngles += new Vector3(mouseY * cameraYSpeed * invertYInt * Time.deltaTime, 0, 0);


    #endregion
  }

  private void CheckForJump()
  {
    if (Input.GetKeyDown(KeyCode.Space))
      doJump = true;
  }
  #endregion

  #region FixedUpdate Functions
  private void FixedUpdate()
  {
    UpdatePosition();
    PlayerJump();
    BetterFall();
  }

  private void UpdatePosition()
  {
    transform.position += moveDirection.normalized * moveSpeed * Time.fixedDeltaTime;
  }

  private void PlayerJump()
  {
    if (doJump)
    {
      doJump = false;
      rb.velocity += transform.up * jumpVelocity;
    }
  }

  private void BetterFall()
  {
    if (rb.velocity.y < 0)
    {
      rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
    }
  }
  #endregion

  #region Checking Ground Functions
  private void OnTriggerEnter(Collider other)
  {
    if (other.gameObject.layer == 9)
    {
      groundObjects.Add(other.gameObject);
    }
  }

  private void OnTriggerExit(Collider other)
  {
    if (groundObjects.Contains(other.gameObject))
      groundObjects.Remove(other.gameObject);
  }
  #endregion
}
