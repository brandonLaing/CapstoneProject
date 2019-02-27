using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnZone : MonoBehaviour
{
  public Transform respawnPosition;

  private void OnTriggerEnter(Collider other)
  {
    if (other.CompareTag("Player"))
    {
      other.GetComponent<Rigidbody>().MovePosition(respawnPosition.position);
      other.GetComponent<Rigidbody>().velocity = new Vector3();
    }
  }
}
