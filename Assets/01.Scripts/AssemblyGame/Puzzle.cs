using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Puzzle : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    private Transform targetObject; // ��ǥ ������Ʈ
    public static bool locked;

    public RectTransform boundaryObject; // �̵� ������ ������ ������ ������Ʈ
    private Vector2 boundarySize; // �̵� ������ ���� ũ��

    private RectTransform rectTransform;
    private Canvas canvas;
    private bool isLocked = false; // ���� ����
    private float pressTime = 0f; // ��ư�� ������ �ִ� �ð��� �����ϴ� ����

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();

        // boundaryObject�� ũ�⸦ ����Ͽ� �̵� ���� ����
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

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!isLocked)
        {
            float distance = Vector2.Distance(rectTransform.anchoredPosition, targetObject.GetComponent<RectTransform>().anchoredPosition);

            // ���� ������ ȸ�� ������ ��ǥ ������Ʈ�� ȸ�� ���� ����
            float rotationDifference = Mathf.Abs(rectTransform.rotation.eulerAngles.z - targetObject.rotation.eulerAngles.z);

            // �Ÿ��� 30 �����̰� ȸ�� ���̰� 1�� ������ �� Ÿ�� ��ġ�� �ٵ��� �մϴ�.
            if (distance <= 30f && rotationDifference <= 1f)
            {
                // ������Ʈ�� Ÿ���� ��ġ�� ȸ���� ����ϴ�.
                rectTransform.anchoredPosition = targetObject.GetComponent<RectTransform>().anchoredPosition;
                rectTransform.rotation = targetObject.rotation;

                // ���� ���·� �����մϴ�.
                isLocked = true;
                locked = true;

                // ������ �� �� ���� "����" ����� �α� ���
                Debug.Log("����");
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        pressTime = Time.time; // ��ư�� ���� �ð� ���
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (Time.time - pressTime < 0.3f) // ª�� Ŭ������ ȸ��
        {
            RotateImage();
        }
    }

    public void RotateImage()
    {
        if (!isLocked)
        {
            // �̹����� ���� Z�� ȸ�� ����
            float currentRotation = rectTransform.rotation.eulerAngles.z;

            // 90���� ȸ��
            float newRotation = currentRotation + 90f;

            // ȸ�� ����
            rectTransform.rotation = Quaternion.Euler(0, 0, newRotation);
        }
    }
}