using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Puzzle : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    private Transform targetObject; // 목표 오브젝트
    public static bool locked;

    public RectTransform boundaryObject; // 이동 가능한 범위를 제한할 오브젝트
    private Vector2 boundarySize; // 이동 가능한 범위 크기

    private RectTransform rectTransform;
    private Canvas canvas;
    private bool isLocked = false; // 고정 여부
    private float pressTime = 0f; // 버튼을 누르고 있는 시간을 저장하는 변수

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();

        // boundaryObject의 크기를 사용하여 이동 범위 설정
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

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!isLocked)
        {
            float distance = Vector2.Distance(rectTransform.anchoredPosition, targetObject.GetComponent<RectTransform>().anchoredPosition);

            // 퍼즐 조각의 회전 각도와 목표 오브젝트의 회전 각도 차이
            float rotationDifference = Mathf.Abs(rectTransform.rotation.eulerAngles.z - targetObject.rotation.eulerAngles.z);

            // 거리가 30 이하이고 회전 차이가 1도 이하일 때 타겟 위치로 붙도록 합니다.
            if (distance <= 30f && rotationDifference <= 1f)
            {
                // 오브젝트를 타겟의 위치와 회전에 맞춥니다.
                rectTransform.anchoredPosition = targetObject.GetComponent<RectTransform>().anchoredPosition;
                rectTransform.rotation = targetObject.rotation;

                // 고정 상태로 설정합니다.
                isLocked = true;
                locked = true;

                // 고정된 후 한 번만 "고정" 디버그 로그 출력
                Debug.Log("고정");
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        pressTime = Time.time; // 버튼을 누른 시간 기록
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (Time.time - pressTime < 0.3f) // 짧은 클릭으로 회전
        {
            RotateImage();
        }
    }

    public void RotateImage()
    {
        if (!isLocked)
        {
            // 이미지의 현재 Z축 회전 각도
            float currentRotation = rectTransform.rotation.eulerAngles.z;

            // 90도씩 회전
            float newRotation = currentRotation + 90f;

            // 회전 적용
            rectTransform.rotation = Quaternion.Euler(0, 0, newRotation);
        }
    }
}