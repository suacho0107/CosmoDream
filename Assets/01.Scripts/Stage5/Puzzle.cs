using UnityEngine;
using UnityEngine.EventSystems;

public class Puzzle : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
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
    private PuzzleManager puzzleManager; // 퍼즐 매니저 참조

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
            // 자식 오브젝트의 절대 위치를 가져오기
            Vector3 targetPosition = targetObject.GetComponent<RectTransform>().position;

            // 퍼즐 조각의 절대 위치 가져오기
            Vector3 puzzlePosition = rectTransform.position;

            // 절대 위치를 사용하여 거리 계산
            float distance = Vector2.Distance(puzzlePosition, targetPosition);

            // 거리가 30 이하일 때 타겟 위치로 붙도록 합니다.
            if (distance <= 30f)
            {
                // 오브젝트를 타겟의 위치로 맞춥니다.
                rectTransform.position = targetPosition;

                // 고정 상태로 설정합니다.
                isLocked = true;
                locked = true;

                // 퍼즐 매니저에 알림
                if (puzzleManager != null)
                {
                    puzzleManager.CheckPuzzleCompletion();
                }

                // 고정된 후 한 번만 "고정" 디버그 로그 출력
                Debug.Log("고정");
            }
        }
    }

    void Update()
    {
        if (isLocked && targetObject != null)
        {
            // 목표 오브젝트의 위치가 변경된 경우, 퍼즐 조각을 목표 오브젝트의 위치에 맞춥니다.
            Vector3 targetPosition = targetObject.GetComponent<RectTransform>().position;
            rectTransform.position = targetPosition;
        }
    }
}
