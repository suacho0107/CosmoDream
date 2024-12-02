using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Puzzle2 : MonoBehaviour, IBeginDragHandler, IDragHandler//, IEndDragHandler
{
    [SerializeField]
    public static bool locked;

    public RectTransform boundaryObject;
    private Vector2 boundarySize;

    private RectTransform rectTransform;
    private bool isLocked = false;
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        Canvas canvas = GetComponent<Canvas>();

        if (boundaryObject != null)
        {
            SetBoundarySize();
        }
    }
    void SetBoundarySize()
    {
        // boundaryObject�� ũ�⸦ �����ͼ� �̵� ������ ������ ����
        boundarySize = boundaryObject.rect.size / 2f;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        // �巡�� ���� �� �ƹ� ������ ���� �ʽ��ϴ�.
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isLocked)
        {
            // ĵ�������� ���콺 ������ ��ġ�� ���� ��ǥ�� ��ȯ
            Vector2 localPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(boundaryObject, eventData.position, eventData.pressEventCamera, out localPoint);

            // �̵� ������ ���� �������� �̵��ϵ��� �����մϴ�.
            float clampedX = Mathf.Clamp(localPoint.x, -boundarySize.x, boundarySize.x);
            float clampedY = Mathf.Clamp(localPoint.y, -boundarySize.y, boundarySize.y);

            // ��ġ�� ������Ʈ�մϴ�.
            rectTransform.anchoredPosition = new Vector2(clampedX, clampedY);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}