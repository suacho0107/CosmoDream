using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;

    private AudioSource audioSource;
    public AudioClip defaultMusic; // 기본 음악
    public AudioClip specialSceneMusic; // 특정 씬에서 재생할 음악

    private void Awake()
    {
        // Singleton 패턴 적용
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // 씬 전환 시 파괴되지 않음
            audioSource = GetComponent<AudioSource>();
        }
        else
        {
            Destroy(gameObject); // 중복된 MusicManager 파괴
        }
    }

    private void Start()
    {
        PlayMusic(defaultMusic); // 기본 음악 재생
    }

    // 음악 재생 함수
    public void PlayMusic(AudioClip clip)
    {
        if (audioSource.clip == clip) return; // 같은 음악이면 실행하지 않음
        audioSource.clip = clip;
        audioSource.loop = true; // 루프 설정
        audioSource.Play();
    }

    // 음악 정지 함수
    public void StopMusic()
    {
        audioSource.Stop();
    }
}
