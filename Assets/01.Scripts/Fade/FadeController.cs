using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeController : MonoBehaviour
{
    public float fadeDuration = 0.5f;

    public bool isFade = false;
    public IEnumerator FadeIn(CanvasGroup fadeTarget)
    {
        isFade = true;

        float time = 0f;
        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            fadeTarget.alpha = Mathf.Clamp01(time / fadeDuration);
            yield return null;
        }
        fadeTarget.alpha = 1.0f;
    }

    public IEnumerator FadeOut(CanvasGroup fadeTarget)
    {
        float time = 0f;
        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            fadeTarget.alpha = 1.0f - Mathf.Clamp01(time / fadeDuration);
            yield return null;
        }
        fadeTarget.alpha = 0f;

        isFade = false;
    }
}
