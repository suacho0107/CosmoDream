using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncMovement : MonoBehaviour
{
    [SerializeField]
    private Puzzle2 puzzleObject;  // Puzzle2 ��ũ��Ʈ�� �ִ� ������Ʈ�� ����

    private RectTransform rectTransform;
    private Vector2 initialOffset;  // �ʱ� ��ġ ������ ���� ����

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        // SyncMovement ������Ʈ�� Puzzle2 ������Ʈ ���� �ʱ� ������ ���
        if (puzzleObject != null)
        {
            initialOffset = rectTransform.anchoredPosition - puzzleObject.GetComponent<RectTransform>().anchoredPosition;
        }
    }

    void Update()
    {
        if (puzzleObject != null)
        {
            // Puzzle2 ������Ʈ�� ��ġ�� �ʱ� �������� ���Ͽ� ���� ��ġ ���
            rectTransform.anchoredPosition = puzzleObject.GetComponent<RectTransform>().anchoredPosition + initialOffset;
        }
    }
}
