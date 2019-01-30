using UnityEngine;
using System.Collections;

public class RotateCoin : MonoBehaviour
{
    public float spinRate = 10;  //  The speed of rotation.  Can be adjusted in the inspector

	// Update is called once per frame
	void Update ()
    {
        transform.Rotate(0, 0, Time.deltaTime * spinRate);  //  Spin the coind
	}
}
