using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingHazard : MonoBehaviour
{
    /** Moving Platform:
     * Author: Brandon Laing
     * This script will move a platform on the x axis a certain number of units then have it move back the other way the same amount of units
     */

    // public shit you can change in the inspector so get out of my fucking code.
    // this really doesn't need to be a float but i made it that just in case your feeling extra.

    #region Variables
    public float moveSpeed = 500;
    public float moveInterval;

    public Material movingPlatformMaterial;

    public string playerTag = "Player";

    public bool havingTroubleWithTheScale;

    private float forwardCooldown;
    private bool movingForward = false;

    public float waitTime;

    private bool doThing = false;

    #endregion


    // this is honestly if your using pro builder for some reason when pro builder cubes load in they reset its material
    private void Awake()
    {
        GetComponent<MeshRenderer>().material = movingPlatformMaterial;

    } // end Awake

    private void Start()
    {
        StartCoroutine(WaitTime());


    }
    private void Update()
    {
        if (doThing)
        {
            // if the object should be moving forward more it forward * the move speed
            if (movingForward)
            {
                gameObject.transform.position += transform.forward * moveSpeed * Time.deltaTime;

            }

            // if its not moving forward move it backward by the same move speed
            else
            {
                gameObject.transform.localPosition += -transform.forward * moveSpeed * Time.deltaTime;

            }

            // This will spwap the movingForward variable if time is bigger than the cooldown
            if (Time.time > forwardCooldown)
            {
                movingForward = !movingForward;
                forwardCooldown = Time.time + moveInterval;

            }

        }

        #region Debugging
        // debugging to see if its hitting those edges in the right position
        if (havingTroubleWithTheScale)
        {
            Debug.Log("So basically if you having trouble with the scale messing up its because you need the parent object" +
              " AKA the current moving platforms scale to be (1,1,1) if its anything else the player object will freak out" +
              " and start really messing up. Currently I don't know how to fix this but if we need it changed ill find out" +
              " how to fix it");

            havingTroubleWithTheScale = false;
        }

        #endregion

    } // end Update

    IEnumerator WaitTime()
    {
        yield return new WaitForSecondsRealtime(waitTime);
        doThing = true;

    }
} // end MovingPlatform