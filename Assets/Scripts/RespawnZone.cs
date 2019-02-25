using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnZone : MonoBehaviour
{
  public Transform respawnPosition;

  private void OnTriggerEnter(Collider other)
  {
    Debug.Log("Something hit Trigger");

    if (other.CompareTag("Player"))
    {
      other.GetComponent<Rigidbody>().MovePosition(respawnPosition.position);
      other.GetComponent<Rigidbody>().velocity = new Vector3();
    }
  }

  private void OnCollisionEnter(Collision collision)
  {
    Debug.Log("Something hit Collision");
  }
}
