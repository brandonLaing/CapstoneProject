using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMoving : MonoBehaviour
{
  public Vector3 startPosition;
  public Vector3 endPosition;

  public float moveSpeed = 1F;
  public float swapCooldown = 1F;
  public bool startMoving;
  [Range(0.01F, 1F)]
  public float distanceOfSatisfaction = 0.1F;


  private bool moveForward;
  private float swapTimer = 0F;

  private void Start()
  {
    startPosition = transform.position;
  }

  public void AddEndPoint(Vector3 endPos)
  {
    endPosition = endPos;
    startMoving = true;
  }

  private void FixedUpdate()
  {
    if (startMoving)
    {
      PositionsBased();
    }
  }

  private void PositionsBased()
  {
    swapTimer += Time.fixedDeltaTime;

    if (moveForward)
    {
      transform.position = Vector3.MoveTowards(transform.position, endPosition, moveSpeed * Time.fixedDeltaTime);
    }
    else
    {
      transform.position = Vector3.MoveTowards(transform.position, startPosition, moveSpeed * Time.fixedDeltaTime);
    }

    if (Vector3.Distance(transform.position, startPosition) < distanceOfSatisfaction || Vector3.Distance(transform.position, endPosition) < distanceOfSatisfaction)
      if (swapTimer > swapCooldown)
      {
        moveForward = !moveForward;
        swapTimer = 0;
      }
  }

  private void OnCollisionEnter(Collision collision)
  {
    if (collision.gameObject.GetComponent<Rigidbody>() && !collision.gameObject.GetComponent<Rigidbody>().isKinematic)
    {
      for (Transform tf = collision.transform; tf != null; tf = tf.parent)
        if (tf.parent == null)
          tf.parent = this.transform;
    }
  }

  private void OnCollisionExit(Collision collision)
  {
    for (Transform tf = collision.transform; tf != null; tf = tf.parent)
      if (tf.parent == this.transform)
        tf.parent = null;
  }
}
