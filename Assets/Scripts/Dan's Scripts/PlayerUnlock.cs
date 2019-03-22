using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnlock : MonoBehaviour
{
    public GameObject newPlayer;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Instantiate(newPlayer, other.transform.position, other.transform.rotation);
            Destroy(other.transform.parent.gameObject);
            Destroy(this.gameObject);
        }
    }
}
