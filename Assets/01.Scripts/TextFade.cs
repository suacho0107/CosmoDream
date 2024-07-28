using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextFade : MonoBehaviour
{
    public Text stageText;
    public Image backgroundImage;  // 배경 이미지를 위한 변수
    public float fadeDuration = 1.0f;  // 페이드인 및 페이드아웃의 지속 시간

    void Start()
    {
        StartCoroutine(FadeInOut());
    }

    IEnumerator FadeInOut()
    {
        // 페이드인
        for (float t = 0.01f; t < fadeDuration; t += Time.deltaTime)
        {
            float normalizedTime = t / fadeDuration;
            stageText.color = Color.Lerp(Color.clear, Color.white, normalizedTime);
            backgroundImage.color = Color.Lerp(new Color(backgroundImage.color.r, backgroundImage.color.g, backgroundImage.color.b, 0),
                                                new Color(backgroundImage.color.r, backgroundImage.color.g, backgroundImage.color.b, 1),
                                                normalizedTime);
            yield return null;
        }

        // 잠시 대기
        yield return new WaitForSeconds(1);

        // 페이드아웃
        for (float t = 0.01f; t < fadeDuration; t += Time.deltaTime)
        {
            float normalizedTime = t / fadeDuration;
            stageText.color = Color.Lerp(Color.white, Color.clear, normalizedTime);
            backgroundImage.color = Color.Lerp(new Color(backgroundImage.color.r, backgroundImage.color.g, backgroundImage.color.b, 1),
                                                new Color(backgroundImage.color.r, backgroundImage.color.g, backgroundImage.color.b, 0),
                                                normalizedTime);
            yield return null;
        }

        // 텍스트와 배경 비활성화
        stageText.gameObject.SetActive(false);
        backgroundImage.gameObject.SetActive(false);
    }
}
