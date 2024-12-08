using System;
using UnityEngine;

public class Metronome : MonoBehaviour
{
    [Header("Metronome Settings")]
    public int bpm = 105; // BPM
    public double offset = 0.0; // ���� �ð� ������ (��)

    [Header("Audio Settings")]
    public AudioSource audioSource; // ȿ���� ����� AudioSource
    public AudioClip beatSound; // ��Ʈ�γ� �Ҹ�

    private double nextBeatTime; // ���� ��ȣ �ð� (8����ǥ ����)
    private double secondsPerEighthBeat; // 8����ǥ ���� (��)

    public event Action OnEighthBeat; // 8����ǥ ��ȣ �̺�Ʈ

    void Start()
    {
        // 8����ǥ�� ���� ���
        secondsPerEighthBeat = 60.0 / bpm / 2;

        // ù ��ȣ �ð� ����
        nextBeatTime = AudioSettings.dspTime + offset;
    }

    void Update()
    {
        double currentTime = AudioSettings.dspTime;

        // ���� �ð��� ���� ��ȣ ������ �����ٸ�
        if (currentTime >= nextBeatTime)
        {
            // 8����ǥ ��ȣ �߻�
            OnEighthBeat?.Invoke();

            // ȿ���� ���
            if (audioSource != null && beatSound != null)
            {
                audioSource.PlayOneShot(beatSound);
            }

            // ���� ��ȣ ���� ����
            nextBeatTime += secondsPerEighthBeat;
        }
    }
}
