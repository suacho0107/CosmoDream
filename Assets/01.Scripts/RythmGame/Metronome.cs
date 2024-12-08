using System;
using UnityEngine;

public class Metronome : MonoBehaviour
{
    [Header("Metronome Settings")]
    public int bpm = 105; // BPM
    public double offset = 0.0; // 시작 시간 오프셋 (초)

    [Header("Audio Settings")]
    public AudioSource audioSource; // 효과음 재생용 AudioSource
    public AudioClip beatSound; // 메트로놈 소리

    private double nextBeatTime; // 다음 신호 시간 (8분음표 기준)
    private double secondsPerEighthBeat; // 8분음표 간격 (초)

    public event Action OnEighthBeat; // 8분음표 신호 이벤트

    void Start()
    {
        // 8분음표의 길이 계산
        secondsPerEighthBeat = 60.0 / bpm / 2;

        // 첫 신호 시간 설정
        nextBeatTime = AudioSettings.dspTime + offset;
    }

    void Update()
    {
        double currentTime = AudioSettings.dspTime;

        // 현재 시간이 다음 신호 시점을 지났다면
        if (currentTime >= nextBeatTime)
        {
            // 8분음표 신호 발생
            OnEighthBeat?.Invoke();

            // 효과음 재생
            if (audioSource != null && beatSound != null)
            {
                audioSource.PlayOneShot(beatSound);
            }

            // 다음 신호 시점 설정
            nextBeatTime += secondsPerEighthBeat;
        }
    }
}
