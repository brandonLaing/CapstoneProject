using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoCubeZone : MonoBehaviour
{
  private void OnTriggerEnter(Collider other)
  {
    Debug.Log("Hit something");
    if (other.CompareTag("Player"))
    {
      Debug.Log("Hit Player");

    }
  }

  private void OnTriggerExit(Collider other)
  {
  }
}
