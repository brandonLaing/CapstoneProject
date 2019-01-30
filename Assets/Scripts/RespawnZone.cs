using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnZone : MonoBehaviour
{
  /** RespawnZone Does:
   * Author: Brandon Laing
   * Its pretty obvious
   */

  #region Variables
  public string playerTag = "Player";
  public string checkpointManagerTag = "CheckpointManager";

  public GameObject checkpointManager;

  #endregion

  private void Awake()
  {
    checkpointManager = GameObject.FindGameObjectWithTag(checkpointManagerTag);

  } // end Awake

  private void OnTriggerEnter(Collider other)
  {
    if (other.gameObject.tag == playerTag)
    {
      other.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);

      checkpointManager.GetComponent<CheckpointManager>().SpawnPlayer();

      other.GetComponentInChildren<PlatformProjectorScript>().DestroyObjectsInList();

    }
  } // end OnTriggerEnter

} // end RespawnZone
