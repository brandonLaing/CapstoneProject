using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomColor : MonoBehaviour
{
    void Start()
    {
    GetComponent<MeshRenderer>().material.color = new Color(Random.value, Random.value, Random.value);
    }
}
