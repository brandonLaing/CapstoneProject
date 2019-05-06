using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSingleton : MonoBehaviour
{
  private static PlayerSingleton main;
  [SerializeField]
  private GameObject[] playerObjects;

  private void Awake()
  {
    if (main == null)
    {
      main = this;
      for (int i = 0; i < playerObjects.Length; i++)
        playerObjects[i].SetActive(true);
    }
    else
    {
      Destroy(this.gameObject);
    }
  }
}
