using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TextFade : MonoBehaviour
{
    public CanvasGroup fadeTarget;
    private FadeController fadeController;

    void Start()
    {
        fadeController = FindObjectOfType<FadeController>();

        if (GameManager.instance != null && !GameManager.instance.isSecondLoad)
        {
            StartCoroutine(FadeInOut()); // 처음 로드 시에만 페이드 효과 실행
        }
    }

    IEnumerator FadeInOut()
    {
        yield return StartCoroutine(fadeController.FadeIn(fadeTarget));
        yield return new WaitForSeconds(1);
        yield return StartCoroutine(fadeController.FadeOut(fadeTarget));
        
    }
}
