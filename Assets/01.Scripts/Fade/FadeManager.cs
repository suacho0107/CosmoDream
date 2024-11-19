using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic; 
using UnityEngine.SceneManagement;

public class FadeManager : MonoBehaviour
{
    public static FadeManager instance;
    public CanvasGroup fadeCanvasGroup;
    public Text stageTextUI;
    float fadeDuration = 1f;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        fadeCanvasGroup = GameObject.FindGameObjectWithTag("FadeCanvas")
        ?.GetComponent<CanvasGroup>();

        if (fadeCanvasGroup != null)
        {
            stageTextUI = fadeCanvasGroup.GetComponentInChildren<Text>();
        }
        if (stageTextUI != null && GameManager.instance.isSecondLoad != true)
        {
            StartCoroutine(FadeInAndOutText());
        }
        else
        {
            // 텍스트가 없을 경우에만 페이드 아웃 실행
            StartCoroutine(FadeOut());
            Debug.Log($"씬 시작할때 로드되는 페이드: {scene.name}");
        }
    }

    // 씬 이름으로 씬 전환
    public void ChangeScene(string sceneName)
    {
        StartCoroutine(FadeAndLoadScene(sceneName));
    }

    // 씬 인덱스로 씬 전환
    public void ChangeScene(int sceneIndex)
    {
        StartCoroutine(FadeAndLoadScene(sceneIndex));
    }

    private IEnumerator FadeAndLoadScene(string sceneName)
    {
        yield return StartCoroutine(FadeIn());
        SceneManager.LoadScene(sceneName);
        yield return StartCoroutine(FadeOut());
    }

    private IEnumerator FadeAndLoadScene(int sceneIndex)
    {
        yield return StartCoroutine(FadeIn());
        SceneManager.LoadScene(sceneIndex);
        yield return StartCoroutine(FadeOut());
    }

    private IEnumerator FadeIn()
    {
        float time = 0f;
        fadeCanvasGroup.alpha = 0f;
        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            fadeCanvasGroup.alpha = Mathf.Clamp01(time / fadeDuration);
            yield return null;
        }
        fadeCanvasGroup.alpha = 1f;
    }

    private IEnumerator FadeOut()
    {
        float time = 0f;
        fadeCanvasGroup.alpha = 1f;
        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            fadeCanvasGroup.alpha = 1f - Mathf.Clamp01(time / fadeDuration);
            yield return null;
        }
        fadeCanvasGroup.alpha = 0f;
    }

    private IEnumerator FadeInAndOutText()
    {
        Debug.Log("스테이지 텍스트가 있을 때의 페이드");

        if (stageTextUI == null)
        {
            yield break;
        }

        // 텍스트 페이드 인
        float time = 0f;
        Color color = stageTextUI.color;
        color.a = 0f;
        stageTextUI.color = color;

        while (time < fadeDuration)
        {
            if (stageTextUI == null) yield break;
            time += Time.deltaTime;
            color.a = Mathf.Clamp01(time / fadeDuration);
            stageTextUI.color = color;
            fadeCanvasGroup.alpha = Mathf.Clamp01(time / fadeDuration);
            yield return null;
        }
        stageTextUI.color = new Color(color.r, color.g, color.b, 1f);
        fadeCanvasGroup.alpha = 1f;

        // 텍스트 유지 시간
        yield return new WaitForSeconds(fadeDuration + 0.5f);
        

        // 텍스트 페이드 아웃
        time = 0f;
        while (time < fadeDuration)
            {
                if (stageTextUI == null) yield break;
                time += Time.deltaTime;
                color.a = 1f - Mathf.Clamp01(time / fadeDuration);
                stageTextUI.color = color;
                fadeCanvasGroup.alpha = 1f - Mathf.Clamp01(time / fadeDuration);  // 배경도 동시에 페이드 아웃
                yield return null;
            }

        stageTextUI.color = new Color(color.r, color.g, color.b, 0f);
        fadeCanvasGroup.alpha = 0f;
    }
}