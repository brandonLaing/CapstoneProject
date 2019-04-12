using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeSize : MonoBehaviour
{
  [SerializeField]
  private float minSize, maxSize;
  [SerializeField]
  private float minHeight, maxHeight;

  private float RndSize
  {
    get
    {
      return Random.Range(minSize, maxSize);
    }
  }
  private float RndHeight
  {
    get
    {
      return Random.Range(minHeight, maxHeight);
    }
  }

  private void Start()
  {
    transform.position = new Vector3(transform.position.x, RndHeight, transform.position.z);

    float yScale = transform.position.y * 2;
    transform.localScale = new Vector3(RndSize, yScale, RndSize);
  }
}
