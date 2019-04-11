﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2Animation : MonoBehaviour
{
    [SerializeField]
    private GameObject platform1;
    [SerializeField]
    private GameObject platform2;
    [SerializeField]
    private GameObject platform3;
    [SerializeField]
    private GameObject platform4;
    [SerializeField]
    private GameObject platform5;
    [SerializeField]
    private GameObject platform6;
    List<GameObject> platforms;

    void Start()
    {
        platforms = new List<GameObject>();
        platforms.Add(platform1);
        platforms.Add(platform2);
        platforms.Add(platform3);
        platforms.Add(platform4);
        platforms.Add(platform5);
        platforms.Add(platform6);
        StartCoroutine("PlatformAnimation");
    }

    private IEnumerator PlatformAnimation()
    {
        int activeStart = 0;
        int activeEnd = 3;

        while (true)
        {
            platforms[activeStart].SetActive(true);
            platforms[activeEnd].SetActive(false);
            activeStart += 1;
            activeEnd += 1;
            if (activeStart== 6)
            {
                activeStart = 0;
            }
            if (activeEnd == 6)
            {
                activeEnd = 0;
            }
            yield return new WaitForSeconds(5f);
        }
    }
}