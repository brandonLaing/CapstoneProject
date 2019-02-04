using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CheckpointManager : MonoBehaviour
{
  /** CheckpointManager
   * Author: Brandon Laing
   * This will keep track of all the checkpoints in level and allow for teleporting between them for the use of
   * debugging. They also are used for when the player fall and gives them a place to respawn.
   */

  public GameObject[] checkpoints;
  public GameObject currentCheckpoint;
  public GameObject player;
  public Vector3 spawnOffset;
  public bool checkColors;
  public bool debugging;
  public string checkpointTag = "Checkpoint";
  public string playerTag = "Player";
  public int checkpointInt;

  #region EnableDebugging variables
  private string debuggingLock = "19573";
  public string debuggingKey = null;

  #endregion

  private void Awake()
  {
    // find the checkpoint and player objects with whatever they can find with the tag
    checkpoints = GameObject.FindGameObjectsWithTag(checkpointTag).OrderBy(go => go.name).ToArray();
    player = GameObject.FindGameObjectWithTag(playerTag);


    currentCheckpoint = null;

  }

  private void Update()
  {
    // if there is no current checkpoint it makes the first one in the array the current
    if (currentCheckpoint == null)
    {
      currentCheckpoint = checkpoints[0];
      checkpointInt = 0;
      checkpoints[0].GetComponent<CheckpointPlatform>().CheckColor();

    }

    // when it gets the order to check colors it goes though each platform and does a color check
    if (checkColors)
    {
      checkColors = false;

      for (int i = 0; i < checkpoints.Length; i++)
      {
        checkpoints[i].GetComponent<CheckpointPlatform>().CheckColor();

      }

    }

    // DO NOT MESS WITH EITHER OF THESE, PLEASE.
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

    #endregion
    #region Debugging
    if (debugging)
    {
      // go to next previous checkpoint
      if (Input.GetKeyDown(KeyCode.Keypad4))
      {
        if (checkpointInt - 1 >= 0)
        {
          currentCheckpoint = checkpoints[--checkpointInt];

          for (int i = 0; i < checkpoints.Length; i++)
          {
            checkpoints[i].GetComponent<CheckpointPlatform>().CheckColor();

          }

        } // end Keypad4

      }

      if (Input.GetKeyDown(KeyCode.Keypad6))
      {
        if (checkpointInt + 1 <= checkpoints.Length - 1)
        {
          currentCheckpoint = checkpoints[++checkpointInt];

          for (int i = 0; i < checkpoints.Length; i++)
          {
            checkpoints[i].GetComponent<CheckpointPlatform>().CheckColor();

          }

        } // end Keypad6

      }

      if (Input.GetKeyDown(KeyCode.KeypadEnter))
      {
        SpawnPlayer();

      }
    } // end Debugging
    #endregion

  }

  // when this is called it takes position of the currentCheckpoint and adds the offset to it.
  public void SpawnPlayer()
  {
    player.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);

    player.transform.position = currentCheckpoint.transform.position + spawnOffset;

  } // end SpawnPlayer

} // end checkpointManager
