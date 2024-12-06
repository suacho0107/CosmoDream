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
    float fadeDuration = 0.6f;
    PlayerController playerController;

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
        playerController = FindObjectOfType<PlayerController>();
        fadeCanvasGroup = GameObject.FindGameObjectWithTag("FadeCanvas")
        ?.GetComponent<CanvasGroup>();

        if (fadeCanvasGroup != null)
        {
            stageTextUI = fadeCanvasGroup.GetComponentInChildren<Text>();
        }
        if (stageTextUI != null && GameManager.instance.isSecondLoad != true)
        {
            StartCoroutine(FadeOutWithText());
        }
        else
        {
            // 텍스트가 없을 경우에만 페이드 아웃 실행
            StartCoroutine(FadeOut());
            Debug.Log($"씬 시작할때 로드되는 페이드: {scene.name}");
        }
    }

    private void SetPlayerControl(bool enabled)
    {
        if (playerController != null)
        {
            playerController.SetMove(enabled);
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
    // (지정된 페이드 지속 시간 사용)
    public void ChangeScene(int sceneIndex, float customFadeDuration)
    {
        StartCoroutine(FadeAndLoadScene(sceneIndex, customFadeDuration));
    }

    private IEnumerator FadeAndLoadScene(string sceneName)
    {
        yield return StartCoroutine(FadeIn());
        SceneManager.LoadScene(sceneName);
    }

    private IEnumerator FadeAndLoadScene(int sceneIndex)
    {
        yield return StartCoroutine(FadeIn());
        SceneManager.LoadScene(sceneIndex);
    }

    private IEnumerator FadeAndLoadScene(int sceneIndex, float duration)
    {
        yield return StartCoroutine(FadeIn(duration));
        SceneManager.LoadScene(sceneIndex);
    }

    private IEnumerator FadeIn()
    {
        yield return StartCoroutine(FadeIn(fadeDuration));
    }

    private IEnumerator FadeIn(float duration)
    {
        SetPlayerControl(false);
        float time = 0f;
        fadeCanvasGroup.alpha = 0f;
        while (time < duration)
        {
            time += Time.deltaTime;
            fadeCanvasGroup.alpha = Mathf.Clamp01(time / duration);
            yield return null;
        }
        fadeCanvasGroup.alpha = 1f;
    }

    private IEnumerator FadeOut()
    {
        SetPlayerControl(false);
        if (playerController != null)
        {
            playerController.SetMove(false); // 입력 차단
        }
        float time = 0f;
        fadeCanvasGroup.alpha = 1f;
        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            fadeCanvasGroup.alpha = 1f - Mathf.Clamp01(time / fadeDuration);
            yield return null;
        }
        fadeCanvasGroup.alpha = 0f;

        if (!GameManager.instance.isTalk)
            SetPlayerControl(true);
        else
            SetPlayerControl(false);
    }

    private IEnumerator FadeOutWithText()
    {
        Debug.Log("텍스트와함께 페이드아웃");
        SetPlayerControl(false);
       
        fadeCanvasGroup.alpha = 1f;

        // 페이드 인
        float time = 0f;
        Color textColor = stageTextUI.color;
        textColor.a = 0f;
        stageTextUI.color = textColor;

        while (time < 0.5f)
        {
            time += Time.deltaTime;
            textColor.a = Mathf.Clamp01(time / 0.5f);
            stageTextUI.color = textColor;
            yield return null;
        }

        // 텍스트 유지
        textColor.a = 1f;
        stageTextUI.color = textColor;
        yield return new WaitForSeconds(0.5f);

        // 페이드 아웃
        time = 0f;
        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            textColor.a = 1f - Mathf.Clamp01(time / fadeDuration);
            stageTextUI.color = textColor;
            fadeCanvasGroup.alpha = 1f - Mathf.Clamp01(time / fadeDuration); // 배경도 동시에 페이드 아웃
            yield return null;
        }

        stageTextUI.color = new Color(textColor.r, textColor.g, textColor.b, 0f);
        fadeCanvasGroup.alpha = 0f;
        
        if (!GameManager.instance.isTalk)
            SetPlayerControl(true);
        else
            SetPlayerControl(false);
    }
}