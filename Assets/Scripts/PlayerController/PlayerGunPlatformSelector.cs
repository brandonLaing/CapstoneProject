using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGunPlatformSelector : MonoBehaviour
{
  public bool staticAvailable, movingAvailable, speedAvailable, bounceAvailable;
  public GameObject staticPrefab, movingPrefab, speedPrefab, bouncePrefab;

  private bool ableToChoose = false;
  [SerializeField]
  private ProjectionType _projectionType = ProjectionType.Length;

  public event System.Action<GameObject, ProjectionType> OnPlatformSelected = delegate { };

  private void Start()
  {
    PlayerInputManager _playerInputManager = GetComponent<PlayerInputManager>();
    _playerInputManager.OnNextPlatform += SelectNextPlatform;
    _playerInputManager.OnPreviousPlatform += SelectPreviousPlatform;
    _playerInputManager.OnPlatformOneSelected += SelectFirstPlatform;
    _playerInputManager.OnPlatformTwoSelected += SelectSecondPlatform;
    _playerInputManager.OnPlatformThreeSelected += SelectThirdPlatform;
    _playerInputManager.OnPlatformFourSelected +=SelectFourthPlatform;
    GetComponent<PlayerGunShooter>().OnGunTriggered += AcceptChoosing;
    GetComponent<PlayerGunShooter>().OnGunStandby += DenyChoosing;
  }

  private void OnDestroy()
  {
    PlayerInputManager _playerInputManager = GetComponent<PlayerInputManager>();
    _playerInputManager.OnNextPlatform -= SelectNextPlatform;
    _playerInputManager.OnPreviousPlatform -= SelectPreviousPlatform;
    _playerInputManager.OnPlatformOneSelected -= SelectFirstPlatform;
    _playerInputManager.OnPlatformTwoSelected -= SelectSecondPlatform;
    _playerInputManager.OnPlatformThreeSelected -= SelectThirdPlatform;
    _playerInputManager.OnPlatformFourSelected -= SelectFourthPlatform;
    GetComponent<PlayerGunShooter>().OnGunTriggered -= AcceptChoosing;
    GetComponent<PlayerGunShooter>().OnGunStandby -= DenyChoosing;
  }

  private void AcceptChoosing() => ableToChoose = true;
  private void DenyChoosing() => ableToChoose = false;

  /// <summary>
  /// Selects the next platform
  /// </summary>
  public void SelectNextPlatform()
  {
    if (!ableToChoose)
      return;

    switch(_projectionType)
    {
      case ProjectionType.StaticPlatform:
        SelectSecondPlatform();
        break;
      case ProjectionType.MovingPlatform:
        SelectThirdPlatform();
        break;
      case ProjectionType.SpeedPlatform:
        SelectFourthPlatform();
        break;
      case ProjectionType.BouncePlatform:
        SelectFirstPlatform();
        break;
      default:
        SelectFirstPlatform();
        break;
    }
  }

  /// <summary>
  /// Selects the previous platform
  /// </summary>
  public void SelectPreviousPlatform()
  {
    if (!ableToChoose)
      return;

    switch (_projectionType)
    {
      case ProjectionType.StaticPlatform:
        SelectFourthPlatform();
        break;
      case ProjectionType.MovingPlatform:
        SelectFirstPlatform();
        break;
      case ProjectionType.SpeedPlatform:
        SelectSecondPlatform();
        break;
      case ProjectionType.BouncePlatform:
        SelectThirdPlatform();
        break;
      default:
        SelectFirstPlatform();
        break;
    }
  }

  /// <summary>
  /// Selects the static platform
  /// </summary>
  public void SelectFirstPlatform()
  {
    if (staticAvailable && ableToChoose)
    {
      OnPlatformSelected(staticPrefab, ProjectionType.StaticPlatform);
      _projectionType = ProjectionType.StaticPlatform;
    }
  }

  /// <summary>
  /// Selects the moving platform
  /// </summary>
  public void SelectSecondPlatform()
  {
    if (movingAvailable && ableToChoose)
    {
      OnPlatformSelected(movingPrefab, ProjectionType.MovingPlatform);
      _projectionType = ProjectionType.MovingPlatform;
    }
  }

  /// <summary>
  /// Selects the speed platform
  /// </summary>
  public void SelectThirdPlatform()
  {
    if (speedAvailable && ableToChoose)
    {
      OnPlatformSelected(speedPrefab, ProjectionType.SpeedPlatform);
      _projectionType = ProjectionType.SpeedPlatform;
    }
  }

  /// <summary>
  /// Selects the bounce platform
  /// </summary>
  public void SelectFourthPlatform()
  {
    if (bounceAvailable && ableToChoose)
    {
      OnPlatformSelected(bouncePrefab, ProjectionType.BouncePlatform);
      _projectionType = ProjectionType.BouncePlatform;
    }
  }
}
