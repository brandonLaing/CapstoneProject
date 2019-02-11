using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMoving : MonoBehaviour
{
  public float moveTime = 5F;
  public float currentMoveTime = 0F;

  public float moveSpeed = 1F;
  public bool moveForward = true;

  public Vector3 startPosition;
  public Vector3 endPosition;

  public bool movingToEnd;
  public float swapCooldown = 1F;
  public float swapTimer = 0;
  public bool startMoving;

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
    //TimeBased();
  }

  private void PositionsBased()
  {
    swapTimer += Time.fixedDeltaTime;

    if (movingToEnd)
    {
      transform.position = Vector3.MoveTowards(transform.position, endPosition, moveSpeed * Time.fixedDeltaTime);
    }
    else
    {
      transform.position = Vector3.MoveTowards(transform.position, startPosition, moveSpeed * Time.fixedDeltaTime);
    }

    if (Vector3.Distance(transform.position, startPosition) < .1F || Vector3.Distance(transform.position, endPosition) < .1F)
      if (swapTimer > swapCooldown)
      {
        movingToEnd = !movingToEnd;
        swapTimer = 0;
      }
  }

  private void TimeBased()
  {
    currentMoveTime += Time.fixedDeltaTime;
    if (currentMoveTime >= 5)
    {
      moveForward = !moveForward;
      currentMoveTime = 0;
    }

    if (moveForward)
      transform.position += transform.forward * (moveSpeed * Time.fixedDeltaTime);
    else
      transform.position -= transform.forward * (moveSpeed * Time.fixedDeltaTime);
  }

  private void OnCollisionEnter(Collision collision)
  {
    if (collision.gameObject.GetComponent<Rigidbody>() && !collision.gameObject.GetComponent<Rigidbody>().isKinematic)
    {
      if (collision.gameObject.CompareTag("Player"))
        collision.transform.parent.parent = this.transform;
      else
        collision.transform.parent = this.transform;
    }
  }

  private void OnCollisionExit(Collision collision)
  {
    if (collision.transform.parent == this.transform)
      collision.transform.parent = null;
    else if (collision.transform.parent != null && collision.transform.parent.parent == this.transform)
      collision.transform.parent.parent = null;
  }
}
