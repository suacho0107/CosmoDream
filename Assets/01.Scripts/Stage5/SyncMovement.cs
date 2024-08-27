using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncMovement : MonoBehaviour
{
    [SerializeField]
    private Puzzle2 puzzleObject;  // Puzzle2 스크립트가 있는 오브젝트를 참조

    private RectTransform rectTransform;
    private Vector2 initialOffset;  // 초기 위치 오프셋 저장 변수

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        // SyncMovement 오브젝트와 Puzzle2 오브젝트 간의 초기 오프셋 계산
        if (puzzleObject != null)
        {
            initialOffset = rectTransform.anchoredPosition - puzzleObject.GetComponent<RectTransform>().anchoredPosition;
        }
    }

    void Update()
    {
        if (puzzleObject != null)
        {
            // Puzzle2 오브젝트의 위치와 초기 오프셋을 더하여 현재 위치 계산
            rectTransform.anchoredPosition = puzzleObject.GetComponent<RectTransform>().anchoredPosition + initialOffset;
        }
    }
}
