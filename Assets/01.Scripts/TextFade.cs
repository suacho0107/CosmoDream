using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextFade : MonoBehaviour
{
    public CanvasGroup fadeTarget;

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
