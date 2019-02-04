using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePlatform : MonoBehaviour
{
  /** BouncePlatform:
   * Author: Brandon Laing
   * This script has it so when a player hits this platform it will shoot them up into the air using 
   * the Rigidbody force.
   */

  #region Variables
  // You know if you change any of these variables in here they don't actually effect what your working on.
  public float bounceVelocity = 10;

  public Material bouncePlatformMaterial;

  public string playerTag = "Player";

  #endregion

  // check any other platform
  private void Awake()
  {
    GetComponent<MeshRenderer>().material = bouncePlatformMaterial;

  } // end Awake

  // when the player hits the platform it adds upward velocity
  // Hint: Check elevator or moving script for a cool other option
  private void OnCollisionEnter(Collision collision)
  {
    if (collision.transform.tag == playerTag)
    {
      Rigidbody playerRigidbody = collision.gameObject.GetComponent<Rigidbody>();

      playerRigidbody.velocity += transform.up * bounceVelocity;
    }
  }

} // end OnCollisionEnter

