using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeController : MonoBehaviour
{
    public CanvasGroup fadePanel;
    public float fadeDuration = 1f;

    public bool isFade = false;
    public IEnumerator FadeIn()
    {
        isFade = true;

        float time = 0f;
        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            fadePanel.alpha = Mathf.Clamp01(time / fadeDuration);
            yield return null;
        }
        fadePanel.alpha = 1.0f;
    }

    public IEnumerator FadeOut()
    {
        float time = 0f;
        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            fadePanel.alpha = 1.0f - Mathf.Clamp01(time / fadeDuration);
            yield return null;
        }
        fadePanel.alpha = 0f;

        isFade = false;
    }
}
