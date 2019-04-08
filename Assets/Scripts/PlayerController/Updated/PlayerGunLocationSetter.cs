using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGunLocationSetter : MonoBehaviour
{
  public LayerMask layerMask;

  [SerializeField]
  private float minPlatformRange, maxPlatformRange;
  [SerializeField]
  private float _platformRange;
  [SerializeField]
  private float scrollMultiplier;

  public float PlatformRange
  {
    get
    {
      return _platformRange;
    }
    set
    {
      if (value >= minPlatformRange && value <= maxPlatformRange)
        _platformRange = value;
    }
  }

  public event System.Action<Vector3, Vector3> OnPlatformLocationChanged = delegate { };

  private void Start() => GetComponent<PlayerInputManager>().OnGunRangeChanged += UpdateRange;
  private void OnDestroy() => GetComponent<PlayerInputManager>().OnGunRangeChanged -= UpdateRange;
  private void Update() => UpdatePlatformLocation();


  private void UpdateRange(float gunRange)
  {
    PlatformRange += (gunRange * scrollMultiplier);
  }

  private void UpdatePlatformLocation()
  {
    Vector3 position = Vector3.zero, rotation = Vector3.zero;
    RaycastHit hit;
    Transform cameraTransform = Camera.main.transform;

    if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, PlatformRange, layerMask))
      position = hit.point;
    else
      position = cameraTransform.position + (cameraTransform.forward * PlatformRange);

    rotation = transform.eulerAngles;

    OnPlatformLocationChanged(position, rotation);
  }
}
