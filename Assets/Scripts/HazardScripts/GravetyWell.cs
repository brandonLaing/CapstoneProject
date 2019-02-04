using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravetyWell : MonoBehaviour
{
  /** GravetyWell Does:
   * Author: Brandon Laing
   * This will pull the player to it if the player isn't touching a platform and is in its collider
   */

  #region Variables
  public GameObject gravetyWellCollider;

  public float forceAdded;

  public string playerTag = "Player";

  private GravetyWellCollider gravetyWellColliderScipt;

  private GameObject player;

  private TouchingGround playerTouchingScript;

  #endregion


  private void Awake()
  {
    gravetyWellColliderScipt = gravetyWellCollider.GetComponent<GravetyWellCollider>();

    player = GameObject.FindGameObjectWithTag(playerTag);
    playerTouchingScript = player.GetComponent<TouchingGround>();

  } // end Awake

  private void Update()
  {
    // if player isn't touching ground and is in collider 
    if (!playerTouchingScript.touchingGround && gravetyWellColliderScipt.playerColliding)
    {
      player.GetComponent<Rigidbody>().AddForce((transform.position - player.transform.position) * forceAdded);

    }

  } // end Update

} // end GravetyWell
