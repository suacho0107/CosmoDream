using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasShake : MonoBehaviour
{
    public RectTransform canvasTransform;
    public float shakeAmount = 0.5f;
    float shakeDuration;

    Vector2 initPosition; //캔버스 초기 위치

    public void VibrateForTime(float time)
    {
        shakeDuration = time;
    }

    private void Start()
    {
        canvasTransform = GetComponent<RectTransform>();
        initPosition = canvasTransform.anchoredPosition;

        if (canvasTransform == null)
        {
            Debug.LogError("RectTransform is not assigned!");
        }
        else
        {
            Debug.Log("RectTransform assigned successfully.");
        }
    }

    private void Update()
    {
        if (shakeDuration > 0)
        { 

            Vector2 randomOffset = Random.insideUnitCircle * shakeAmount;
            canvasTransform.anchoredPosition = initPosition + randomOffset;
            Debug.Log($"Shaking: {canvasTransform.anchoredPosition}");

            shakeDuration -= Time.unscaledDeltaTime;
        }
        else
        {
            shakeDuration = 0f;
            canvasTransform.anchoredPosition = initPosition;
        }
    }
}
