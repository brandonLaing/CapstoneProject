using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using YourMom.GotEm;

public class SpeedPlatform : MonoBehaviour
{
  /** SpeedPlatform:
   * Author: Brandon Laing
   * This script has it so when a player hits this platform it will shoot them forward into the air using 
   * the Rigidbody force.
   */

  // You may have noticed that this is the bounce script with one difference.
  #region Variables
  public float forwardVelocity = 50;

  public string playerTag = "Player";

  public Material speedPlatformMaterial;

  #endregion

  // resets the material just in case something happens
  private void Awake()
  {
    GetComponent<MeshRenderer>().material = speedPlatformMaterial;

  } // end Awake

  // when the player hits the platform it adds upward velocity
  /** Option Here:
   * Right now it only adds speed when the player hits the pad but if you change OnCollisionEnter to OnCollision Stay 
   * it will add force while it is touching the platform instead at first hit.
   */
  private void OnCollisionStay(Collision collision)
  {
    if (collision.transform.tag == playerTag)
    {
      Rigidbody playerRigidbody = collision.gameObject.GetComponent<Rigidbody>();

      // you can remove Time.DeltaTime if you want. I'm not all that attached to it.
      playerRigidbody.velocity += transform.forward * forwardVelocity * Time.deltaTime;

    }

  } // end OnCollisionEnter

} // end SpeedPlatform
