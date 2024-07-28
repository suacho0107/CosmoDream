using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextFade : MonoBehaviour
{
    public Text stageText;
    public Image backgroundImage;  // ��� �̹����� ���� ����
    public float fadeDuration = 1.0f;  // ���̵��� �� ���̵�ƿ��� ���� �ð�

    void Start()
    {
        StartCoroutine(FadeInOut());
    }

    IEnumerator FadeInOut()
    {
        // ���̵���
        for (float t = 0.01f; t < fadeDuration; t += Time.deltaTime)
        {
            float normalizedTime = t / fadeDuration;
            stageText.color = Color.Lerp(Color.clear, Color.white, normalizedTime);
            backgroundImage.color = Color.Lerp(new Color(backgroundImage.color.r, backgroundImage.color.g, backgroundImage.color.b, 0),
                                                new Color(backgroundImage.color.r, backgroundImage.color.g, backgroundImage.color.b, 1),
                                                normalizedTime);
            yield return null;
        }

        // ��� ���
        yield return new WaitForSeconds(1);

        // ���̵�ƿ�
        for (float t = 0.01f; t < fadeDuration; t += Time.deltaTime)
        {
            float normalizedTime = t / fadeDuration;
            stageText.color = Color.Lerp(Color.white, Color.clear, normalizedTime);
            backgroundImage.color = Color.Lerp(new Color(backgroundImage.color.r, backgroundImage.color.g, backgroundImage.color.b, 1),
                                                new Color(backgroundImage.color.r, backgroundImage.color.g, backgroundImage.color.b, 0),
                                                normalizedTime);
            yield return null;
        }

        // �ؽ�Ʈ�� ��� ��Ȱ��ȭ
        stageText.gameObject.SetActive(false);
        backgroundImage.gameObject.SetActive(false);
    }
}
