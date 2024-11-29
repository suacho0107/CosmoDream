using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ReadyGoCtrl : MonoBehaviour
{
    public TextMeshProUGUI startText;

    float fadeDuration = 0.5f;
    NoteManager notemanager;

    private void Start()
    {
        notemanager = FindObjectOfType<NoteManager>();
        StartCoroutine(ShowReadyGo());
    }

    IEnumerator ShowReadyGo()
    {
        //Ready ���̵� ��
        startText.text = "Ready";
        yield return FadeTextIn(startText);
        yield return new WaitForSeconds(0.5f);

        //Ready ���̵� �ƿ�
        yield return FadeTextOut(startText);

        //Go ���̵� ��
        startText.text = "Go";
        yield return FadeTextIn(startText);
        yield return new WaitForSeconds(0.5f);

        //Go ���̵� �ƿ�
        yield return FadeTextOut(startText);

        notemanager.isStart = true;
        yield return new WaitForSeconds(2f);

    }

    IEnumerator FadeTextIn(TextMeshProUGUI text)
    {
        Color color = text.color;
        color.a = 0;
        text.color = color;

        float time = 0;

        while (time < fadeDuration)
        {
            color.a = Mathf.Lerp(0, 1, time / fadeDuration);
            text.color = color;
            time += Time.deltaTime;
            yield return null;
        }
        color.a = 1;
        text.color = color;
    }

    IEnumerator FadeTextOut(TextMeshProUGUI text)
    {
        Color color = text.color;
        color.a = 1;
        text.color = color;

        float time = 0;
        while (time < fadeDuration)
        {
            color.a = Mathf.Lerp(1, 0, time / fadeDuration);
            text.color = color;
            time += Time.deltaTime;
            yield return null;
        }
        color.a = 0;
        text.color = color;
    }
}
