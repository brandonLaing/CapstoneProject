using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGunProjectionDisplay : MonoBehaviour
{
  public GameObject projectionLocation;

  private void Awake()
  {
    GetComponent<PlayerGunPlatformSelector>().OnPlatformSelected += ChangeDisplay;
    GetComponent<PlayerGunLocationSetter>().OnPlatformLocationChanged += SetEndPoint;
    GetComponent<PlayerGunShooter>().OnGunTriggered += ShowProjection;
    GetComponent<PlayerGunShooter>().OnGunStandby += HideProjection;
  }

  private void OnDestroy()
  {
    GetComponent<PlayerGunPlatformSelector>().OnPlatformSelected -= ChangeDisplay;
    GetComponent<PlayerGunLocationSetter>().OnPlatformLocationChanged -= SetEndPoint;
  }

  private void ChangeDisplay(GameObject platformPrefab, ProjectionType _projectionType)
  {
    projectionLocation.transform.localScale = platformPrefab.transform.GetChild(0).localScale;
    Color _matColor = platformPrefab.GetComponentInChildren<MeshRenderer>().sharedMaterial.color;
    _matColor.a = 0.5F;
    projectionLocation.GetComponent<MeshRenderer>().material.color = _matColor;
  }

  private void SetEndPoint(Vector3 position, Vector3 rotation)
  {
    projectionLocation.transform.position = position;
    projectionLocation.transform.rotation = Quaternion.Euler(rotation);
  }

  private void ShowProjection()
  {
    projectionLocation.SetActive(true);
  }

  private void HideProjection()
  {
    projectionLocation.SetActive(false);
  }
}
