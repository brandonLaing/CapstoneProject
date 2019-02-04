using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchingGround : MonoBehaviour 
{
  /** TouchingGround Does:
   * Author: Brandon Laing
   * This script checks if the player is touching platform
   */

  #region Variables
  public bool touchingGround;

  private string[] platformTags = new string[6];

  #endregion

  // set the tags of each platform
  private void Awake()
  {
    platformTags[0] = "StaticPlatform";
    platformTags[1] = "MovingPlatform";
    platformTags[2] = "BouncePlatform";
    platformTags[3] = "SpeedPlatform";
    platformTags[4] = "Checkpoint";
    platformTags[5] = "ElevatorPlatform";

  } // end Awake

  // this will check if the player is touching anything with one of the platform tags and if they stop touching a platform
  #region Collisions
  // this will check if something stops colliding with the player
  private void OnCollisionExit(Collision collision)
  {
    // it will then go though each tag
    for (int i = 0; i < platformTags.Length; i++)
    {
      // and check if the tag of the thing its colliding with is equal to each tag
      if (collision.transform.tag == platformTags[i])
      {
        // and if it is then it will set the bool to false
        /** More info on how this works for you simple designers:
         * So you may be think that just cause it stops touching one it may still be touching another and to that i say yes
         * thats why this is above the check if something is touching it. So say a player is moving between two platforms.
         * You will leave one platform and touchingGround will be equal to false but you move forward onto the other platform
         * so the OnCollision Stay will still end up true because your entering a new platform so it will end being true
         */
        touchingGround = false;
        
      }

    }

  } // end OnCollisionExit

  // Basically the same as above
  private void OnCollisionStay(Collision collision)
  {
    for (int i = 0; i < platformTags.Length; i++)
    {
      if (collision.transform.tag == platformTags[i])
      {
        touchingGround = true;

      }

    }

  } // end OnCollision Stay
  #endregion

} // end TouchingGround
