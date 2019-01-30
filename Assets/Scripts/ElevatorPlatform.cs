using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorPlatform : MonoBehaviour
{
  /** ElevatorPlatform Does:
   * Author: Brandon Laing
   * This script will move an object up for a certain amount of time then move them back down for that same amount of time
   */

  #region Variables
  // How fast the platform moves
  public float moveSpeed = 10;

  // How long it should move
  public float moveInterval;

  // material for the platform
  public Material movingPlatformMaterial;

  // players tag
  public string playerTag = "Player";

  // if it should be moving up and how long till it should change
  private bool movingUp = false;
  private float upCooldown;

  #endregion

  // This will set the players material to the one stated above just in case something happens to it
  private void Awake()
  {
    GetComponent<MeshRenderer>().material = movingPlatformMaterial;

  } // end Awake

  private void Update()
  {
    #region Move object
    // if it should be moving up adjust the position upward and if not adjust down
    if (movingUp)
    {
      gameObject.transform.position += transform.up * moveSpeed * Time.deltaTime;

    }

    else
    {
      gameObject.transform.position += -transform.up * moveSpeed * Time.deltaTime;

    }

    // check if it should be going up or down
    if (Time.time > upCooldown)
    {
      movingUp = !movingUp;
      upCooldown = Time.time + moveInterval;

    }
    
    #endregion

  } // end Update

  #region Collisions
  // if the player hits the platform make them a child and if they leave remove them as a child
  private void OnCollisionEnter(Collision collision)
  {
    if (collision.gameObject.tag == playerTag)
    {
      collision.transform.parent = transform;

    }

  } // end OnCollisionEnter

  private void OnCollisionExit(Collision collision)
  {
    if (collision.transform.tag == playerTag)
    {
      collision.transform.parent = null;

    }

  } // end OnCollisionExit

  #endregion

} // end Elevator Platform
