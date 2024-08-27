using UnityEngine;
using UnityEngine.EventSystems;

public class Puzzle : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
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
    private PuzzleManager puzzleManager; // ���� �Ŵ��� ����

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
        boundarySize = boundaryObject.rect.size / 2f;
    }

    public void SetPuzzleManager(PuzzleManager manager)
    {
        puzzleManager = manager;
    }

    public bool IsLocked()
    {
        return isLocked;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
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
            // �ڽ� ������Ʈ�� ���� ��ġ�� ��������
            Vector3 targetPosition = targetObject.GetComponent<RectTransform>().position;

            // ���� ������ ���� ��ġ ��������
            Vector3 puzzlePosition = rectTransform.position;

            // ���� ��ġ�� ����Ͽ� �Ÿ� ���
            float distance = Vector2.Distance(puzzlePosition, targetPosition);

            // �Ÿ��� 30 ������ �� Ÿ�� ��ġ�� �ٵ��� �մϴ�.
            if (distance <= 30f)
            {
                // ������Ʈ�� Ÿ���� ��ġ�� ����ϴ�.
                rectTransform.position = targetPosition;

                // ���� ���·� �����մϴ�.
                isLocked = true;
                locked = true;

                // ���� �Ŵ����� �˸�
                if (puzzleManager != null)
                {
                    puzzleManager.CheckPuzzleCompletion();
                }

                // ������ �� �� ���� "����" ����� �α� ���
                Debug.Log("����");
            }
        }
    }

    void Update()
    {
        if (isLocked && targetObject != null)
        {
            // ��ǥ ������Ʈ�� ��ġ�� ����� ���, ���� ������ ��ǥ ������Ʈ�� ��ġ�� ����ϴ�.
            Vector3 targetPosition = targetObject.GetComponent<RectTransform>().position;
            rectTransform.position = targetPosition;
        }
    }
}
