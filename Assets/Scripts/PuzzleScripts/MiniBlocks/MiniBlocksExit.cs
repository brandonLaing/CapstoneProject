using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBlocksExit : MonoBehaviour, IActivateable
{
  public float blocksCollected;
  public float numberOfBlocksForSucess;

  public MiniBlocksManager manager;

  public void Activate()
  {
    Debug.Log("activate Called on exit");

    blocksCollected = 0;
  }

  private void OnTriggerEnter(Collider other)
  {
    if (other.CompareTag("MiniBlock"))
    {
      blocksCollected++;
      Destroy(other.gameObject);
      ValidateSuccess();
    }
  }

  private void ValidateSuccess()
  {
    if (numberOfBlocksForSucess == blocksCollected)
    {
      manager.CheckPassed();
    }
  }
}
