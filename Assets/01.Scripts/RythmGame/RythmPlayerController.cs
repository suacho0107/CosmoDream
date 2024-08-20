using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RythmPlayerController : MonoBehaviour
{
    private TimingManager timingManager;

    private void Start()
    {
        timingManager = FindObjectOfType<TimingManager>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            timingManager.CheckTiming();
        }
    }
}
