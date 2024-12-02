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
        // boundaryObject의 크기를 가져와서 이동 가능한 범위로 설정
        boundarySize = boundaryObject.rect.size / 2f;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        // 드래그 시작 시 아무 동작을 하지 않습니다.
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isLocked)
        {
            // 캔버스에서 마우스 포인터 위치를 로컬 좌표로 변환
            Vector2 localPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(boundaryObject, eventData.position, eventData.pressEventCamera, out localPoint);

            // 이동 가능한 범위 내에서만 이동하도록 제한합니다.
            float clampedX = Mathf.Clamp(localPoint.x, -boundarySize.x, boundarySize.x);
            float clampedY = Mathf.Clamp(localPoint.y, -boundarySize.y, boundarySize.y);

            // 위치를 업데이트합니다.
            rectTransform.anchoredPosition = new Vector2(clampedX, clampedY);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}