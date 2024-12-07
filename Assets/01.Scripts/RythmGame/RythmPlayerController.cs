using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RythmPlayerController : MonoBehaviour
{
    private TimingManager timingManager;
    public AudioSource audioSource;
    public AudioClip KeySound;

    [SerializeField] private KeyCode upperLaneKey1 = KeyCode.D;
    [SerializeField] private KeyCode upperLaneKey2 = KeyCode.F;
    [SerializeField] private KeyCode lowerLaneKey1 = KeyCode.J;
    [SerializeField] private KeyCode lowerLaneKey2 = KeyCode.K;

    private void Start()
    {
        timingManager = FindObjectOfType<TimingManager>();
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        if(Input.GetKeyDown(upperLaneKey1) || Input.GetKeyDown(upperLaneKey2))
        {
            Debug.Log("key enter");
            timingManager.CheckTiming("upper");
            audioSource.PlayOneShot(KeySound);
        }
        if (Input.GetKeyDown(lowerLaneKey1) || Input.GetKeyDown(lowerLaneKey2))
        {
            Debug.Log("key enter");
            timingManager.CheckTiming("down");
            audioSource.PlayOneShot(KeySound);
        }

    }
}
