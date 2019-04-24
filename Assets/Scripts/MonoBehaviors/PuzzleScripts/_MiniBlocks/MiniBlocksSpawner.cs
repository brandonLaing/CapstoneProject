using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBlocksSpawner : MonoBehaviour, IActivateable
{
  public Transform spawnPosition;
  public Vector2 spawnRangeMin;
  public Vector2 spawnRangeMax;

  public int numberOfCubesPerSpawn;
  public GameObject cubePrefab;

  public float timeToDestroy;

  public float spawnForce;

  public List<GameObject> lastBatch;
  public void Activate()
  {
    foreach (GameObject obj in lastBatch)
    {
      Destroy(obj);
    }

    for (int i = 0; i < numberOfCubesPerSpawn; i++)
    {
      GameObject newCube =
        Instantiate(cubePrefab,
        spawnPosition.position + new Vector3(transform.right.x * Random.Range(spawnRangeMin.x, spawnRangeMax.x), 0, transform.up.y * Random.Range(spawnRangeMin.y, spawnRangeMax.y)),
        Quaternion.identity
        );

      newCube.GetComponent<Rigidbody>().AddForce(spawnPosition.up * spawnForce);

      lastBatch.Add(newCube);

      Destroy(newCube, timeToDestroy);
    }
  }
}
