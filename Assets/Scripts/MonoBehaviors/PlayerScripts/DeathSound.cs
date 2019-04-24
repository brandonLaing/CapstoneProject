using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathSound : MonoBehaviour
{
  public AudioSource Deathsound;

  // Use this for initialization
  void Start()
  {
    Deathsound = GetComponent<AudioSource>();

  }

  // Update is called once per frame
  void Update()
  {

  }
  private void OnTriggerEnter(Collider other)
  {
    if (other.gameObject.tag == "Player")
    {
      Deathsound.Play();

    }
  }
}