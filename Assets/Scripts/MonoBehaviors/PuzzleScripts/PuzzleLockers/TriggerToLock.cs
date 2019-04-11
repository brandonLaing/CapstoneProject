using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerToLock : MonoBehaviour
{
  public GameObject toLock;

  private void OnTriggerEnter(Collider other)
  {
    if (other.CompareTag("Player"))
    {
      toLock.GetComponent<ILockable>().Lock();
    }
  }
}
