using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerGroundChecker))]
public class PlayerController : MonoBehaviour
{
  #region Variables
  #endregion


  #region Update Functions
  private void Update()
  {
    GetMoveDirection();
  }

  private void GetMoveDirection()
  {

  }
  #endregion

  #region FixedUpdate Functions
  private void FixedUpdate()
  {
    UpdatePosition();
  }

  private void UpdatePosition()
  {
  }

  #endregion


}
