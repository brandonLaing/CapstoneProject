using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnZone : MonoBehaviour
{
  public Transform respawnPosition;
  private bool movePlayer;
  private GameObject playerObject;

  private void OnTriggerEnter(Collider other)
  {
    if (other.CompareTag("Player"))
    {
      Debug.Log($"Hit {other.name}");

      movePlayer = true;
      playerObject = other.gameObject;
      other.GetComponent<PlayerMovement>().enabled = false;
    }
  }

  private void FixedUpdate()
  {
    if (movePlayer)
    {
      movePlayer = false;
      Debug.Log("Moving Player");
      playerObject.GetComponent<Rigidbody>().MovePosition(respawnPosition.position);
      playerObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
      playerObject.GetComponent<PlayerMovement>().enabled = true;
      playerObject = null;
      
    }
  }

}
