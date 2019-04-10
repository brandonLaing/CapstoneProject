using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
  private void Awake()
  {
    GetComponent<PlayerInputManager>().OnMove += SetMoveDirection;
  }

  /// <summary>
  /// The players WASD movement speed
  /// </summary>'
  [Tooltip("The players WASD movement speed")] [Range(5, 20)] [SerializeField]
  private float moveSpeed = 10;

  /// <summary>
  /// Move direction grabbed from the input manager
  /// </summary>
  private Vector3 rawMoveDirection = Vector3.zero;


  private void SetMoveDirection(Vector3 moveDirection)
  {
    rawMoveDirection = moveDirection;
  }

  private void FixedUpdate()
  {
    transform.position += (rawMoveDirection.normalized * moveSpeed) * Time.fixedDeltaTime;
    rawMoveDirection = Vector3.zero;
  }
}