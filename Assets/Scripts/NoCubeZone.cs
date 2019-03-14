using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoCubeZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerPlatformGun>().enabled = false;
            other.GetComponent<PlayerPlatformGun>().CurrentGunState = GunState.Standby;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            other.GetComponent<PlayerPlatformGun>().enabled = true;
    }
}
