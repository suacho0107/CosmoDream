using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextFade : MonoBehaviour
{
    public CanvasGroup fadeTarget;
    public float fadeDuration = 1.0f;  // 페이드인 및 페이드아웃의 지속 시간

    private FadeController fadeController;

    void Start()
    {
        fadeController = FindObjectOfType<FadeController>();

        StartCoroutine(FadeInOut());
    }

    IEnumerator FadeInOut()
    {
        yield return StartCoroutine(fadeController.FadeIn(fadeTarget));

        yield return new WaitForSeconds(1);

        yield return StartCoroutine(fadeController.FadeOut(fadeTarget));
        
    }
}
