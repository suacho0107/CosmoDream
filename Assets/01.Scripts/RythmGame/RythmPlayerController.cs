using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RythmPlayerController : MonoBehaviour
{
    [SerializeField] private TimingManager timingManager;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip keySound;

    [SerializeField] private KeyCode upperLaneKey1 = KeyCode.D;
    [SerializeField] private KeyCode upperLaneKey2 = KeyCode.F;
    [SerializeField] private KeyCode lowerLaneKey1 = KeyCode.J;
    [SerializeField] private KeyCode lowerLaneKey2 = KeyCode.K;

    private void Update()
    {
        // Upper lane 입력 체크
        if (Input.GetKeyDown(upperLaneKey1) || Input.GetKeyDown(upperLaneKey2))
        {
            timingManager.CheckTiming(LaneType.Upper);
            audioSource.PlayOneShot(keySound);
        }

        // Lower lane 입력 체크
        if (Input.GetKeyDown(lowerLaneKey1) || Input.GetKeyDown(lowerLaneKey2))
        {
            timingManager.CheckTiming(LaneType.Lower);
            audioSource.PlayOneShot(keySound);
        }
    }
}