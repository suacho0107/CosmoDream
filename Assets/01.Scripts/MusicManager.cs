using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public struct SceneMusicMapping
{
    public int MusicIndex;          // 음악 인덱스 (AudioClip 배열 인덱스)
    public List<int> SceneIndexes;  // 매칭되는 씬 인덱스 리스트
}

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;
    public AudioSource AudioSource;
    public AudioClip[] MusicClips;            // 모든 음악 파일
    public SceneMusicMapping[] SceneMappings; // 씬-음악 매핑 배열

    private int currentMusicIndex = -1;
    private Coroutine currentFadeCoroutine;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            if (AudioSource == null)
            {
                AudioSource = GetComponent<AudioSource>();
            }

            if (AudioSource != null)
            {
                DontDestroyOnLoad(AudioSource.gameObject);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        int sceneIndex = scene.buildIndex;
        int musicIndex = GetMusicIndexForScene(sceneIndex);

        if (musicIndex == -1) // 음악 없음
        {
            if (AudioSource.isPlaying)
            {
                StopCurrentFade();
                currentFadeCoroutine = StartCoroutine(FadeOutAndSwitchMusic(-1)); // 음악 중지
            }
        }
        else if (musicIndex != currentMusicIndex) // 새로운 음악 재생
        {
            StopCurrentFade();
            currentFadeCoroutine = StartCoroutine(FadeOutAndSwitchMusic(musicIndex));
        }
    }

    int GetMusicIndexForScene(int sceneIndex)
    {
        foreach (var mapping in SceneMappings)
        {
            if (mapping.SceneIndexes.Contains(sceneIndex))
            {
                return mapping.MusicIndex;
            }
        }
        return -1; // 해당하는 음악이 없을 경우
    }

    void PlayMusic(int musicIndex)
    {
        if (AudioSource == null || musicIndex < 0 || musicIndex >= MusicClips.Length || MusicClips[musicIndex] == null)
        {
            Debug.LogWarning($"음악 인덱스 {musicIndex}가 유효하지 않습니다.");
            return;
        }

        if (AudioSource.clip != MusicClips[musicIndex])
        {
            AudioSource.clip = MusicClips[musicIndex];
            AudioSource.loop = true; // 반복 재생 설정
            AudioSource.Play();
            currentMusicIndex = musicIndex;
        }
    }

    void StopCurrentFade()
    {
        if (currentFadeCoroutine != null)
        {
            StopCoroutine(currentFadeCoroutine);
            currentFadeCoroutine = null;
        }
    }

    IEnumerator FadeOutAndSwitchMusic(int musicIndex)
    {
        // 페이드 아웃
        if (AudioSource.isPlaying)
        {
            float startVolume = AudioSource.volume;
            while (AudioSource.volume > 0)
            {
                AudioSource.volume -= startVolume * Time.deltaTime / 1.0f; // 1초 동안 페이드 아웃
                yield return null;
            }
            AudioSource.Stop();
            AudioSource.volume = startVolume; // 볼륨 복원
        }

        if (musicIndex == -1)
        {
            currentMusicIndex = -1;
            yield break;
        }

        // 음악 전환 후 페이드 인
        PlayMusic(musicIndex);
        AudioSource.volume = 0f;

        while (AudioSource.volume < 1.0f)
        {
            AudioSource.volume += Time.deltaTime / 1.0f; // 1초 동안 페이드 인
            yield return null;
        }
    }
}