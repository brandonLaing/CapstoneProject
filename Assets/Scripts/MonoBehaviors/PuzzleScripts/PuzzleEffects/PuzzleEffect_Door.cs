﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleEffect_Door : PuzzleEffect
{
  public GameObject[] doorsToEffect;
  public override void Solved(bool statusOfDoor)
  {
    for (int i = 0; i < doorsToEffect.Length; i++)
    {
      doorsToEffect[i].SetActive(statusOfDoor);
    }
  }
}
