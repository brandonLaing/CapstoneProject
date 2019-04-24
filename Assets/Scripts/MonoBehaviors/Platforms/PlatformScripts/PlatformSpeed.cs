using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpeed : MonoBehaviour
{
  private List<Rigidbody> objectsToBoost = new List<Rigidbody>();

  private readonly int numberOfTestObjects = 10;
  public float speedForce = 10;

  public GameObject testCubePrefab;

  public void TestOnObjects()
  {
    float
      xMinRange = -transform.localScale.x + transform.localScale.x / 2, 
      xMaxRange = transform.localScale.x - transform.localScale.x / 2,
      yMinRange = 2F, yMaxRange = 5F,
      zMinRange = -transform.localScale.z + transform.localScale.z / 2, 
      zMaxRange = transform.localScale.z - transform.localScale.z / 2;

    for (int i = 0; i < numberOfTestObjects; i++)
    {
      float x = Random.Range(xMinRange, xMaxRange);
      float y = Random.Range(yMinRange, yMaxRange);
      float z = Random.Range(zMinRange, zMaxRange);

      Vector3 position = new Vector3(x, y, z);

      GameObject clone = Instantiate(testCubePrefab, transform.position + position, Quaternion.identity);
      Destroy(clone, 10);
    }
  }

  private void FixedUpdate()
  {
    foreach (Rigidbody obj in objectsToBoost)
    {
      if (obj != null)
        obj.AddForce(transform.forward * speedForce, ForceMode.Impulse);
    }

    objectsToBoost.Clear();
  }

  private void OnCollisionStay(Collision collision)
  {
    if (collision.gameObject.GetComponent<Rigidbody>())
    {
      if (!objectsToBoost.Contains(collision.gameObject.GetComponent<Rigidbody>()))
        objectsToBoost.Add(collision.gameObject.GetComponent<Rigidbody>());
    }
  }
}
