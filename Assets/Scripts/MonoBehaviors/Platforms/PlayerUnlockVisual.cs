using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnlockVisual : MonoBehaviour
{
  private GameObject visual;

  [SerializeField]
  private float rotationSpeed = 5, bounceSpeed = 2;

  private Vector3 startingPosition;
  [SerializeField]
  private float minBounce = -2, maxBounce = 2;
  private bool IsBouncingUp = true;
  [SerializeField]
  private Mesh cubeMesh = null;
  [SerializeField]
  private Material projectionMat = null;

  private void Start()
  {
    visual = new GameObject("Fake", typeof(MeshFilter), typeof(MeshRenderer));
    visual.transform.parent = this.transform;
    visual.GetComponent<MeshFilter>().mesh = cubeMesh;

    GameObject tempHolder = Instantiate(GetComponentInParent<PlayerUnlock>().newPrefab);
    Transform child = tempHolder.transform.GetChild(0);

    visual.transform.localScale = child.transform.localScale / 2;
    Color tempColor = child.GetComponent<MeshRenderer>().materials[0].color;

    Destroy(tempHolder);

    tempColor.a = 0.5F;
    visual.GetComponent<MeshRenderer>().material = projectionMat;
    visual.GetComponent<MeshRenderer>().material.color = tempColor;

    visual.transform.position = this.transform.position;
    startingPosition = visual.transform.position;
  }

  private void Update()
  {
    visual.transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

    if (IsBouncingUp)
      visual.transform.position += new Vector3(0, bounceSpeed * Time.deltaTime, 0);
    else visual.transform.position -= new Vector3(0, bounceSpeed * Time.deltaTime, 0);

    if (visual.transform.position.y >= startingPosition.y + maxBounce)
      IsBouncingUp = false;
    else if (visual.transform.position.y <= startingPosition.y + minBounce)
      IsBouncingUp = true;
  }
}
