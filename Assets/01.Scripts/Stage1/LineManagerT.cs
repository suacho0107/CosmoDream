using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LineManagerT : MonoBehaviour
{
    public GameObject linePrefab; // LineRenderer 프리팹
    public Button resetButton; // 초기화 버튼
    public GameObject endMessage; // 게임 완료 캔버스
    public Button nextSceneButton; // 다음 씬으로 가는 버튼

    private LineRenderer currentLineRenderer;
    private List<LineRenderer> lineRenderers = new List<LineRenderer>();
    private HashSet<(Vector3, Vector3)> connectedPairs = new HashSet<(Vector3, Vector3)>();
    private bool isDrawing = false;
    private GameObject lastConnectedObject = null;
    private int maxConnections = 6;

    void Start()
    {
        // 초기화 버튼 클릭 이벤트 설정
        resetButton.onClick.AddListener(ResetLines);

        // 캔버스와 버튼 초기 설정
        if (endMessage != null)
        {
            endMessage.SetActive(false); // 기본적으로 캔버스 비활성화
        }

        if (nextSceneButton != null)
        {
            nextSceneButton.onClick.AddListener(LoadNextScene);
        }
    }

    void Update()
    {
        if (isDrawing && currentLineRenderer != null)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(
                Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
            currentLineRenderer.SetPosition(1, new Vector3(mousePosition.x, mousePosition.y, 0));
        }
    }

    public void StartNewLine(Vector3 startPos)
    {
        GameObject lineObject = Instantiate(linePrefab);
        currentLineRenderer = lineObject.GetComponent<LineRenderer>();
        currentLineRenderer.startWidth = 0.1f;
        currentLineRenderer.endWidth = 0.1f;
        currentLineRenderer.SetPosition(0, startPos);
        currentLineRenderer.SetPosition(1, startPos);
        lineRenderers.Add(currentLineRenderer);
        isDrawing = true;
    }

    public void EndCurrentLine(Vector3 endPos, GameObject connectedObject)
    {
        if (currentLineRenderer != null)
        {
            if (connectedObject != null && !IsConnected(currentLineRenderer.GetPosition(0), endPos))
            {
                currentLineRenderer.SetPosition(1, endPos);
                connectedPairs.Add((currentLineRenderer.GetPosition(0), endPos));
                connectedPairs.Add((endPos, currentLineRenderer.GetPosition(0))); // 양방향 연결

                lastConnectedObject = connectedObject;
                // lastConnectedObject.GetComponent<LinkedObject>().SetSelected(true); // 선택 표시
                // Debug.Log(currentLineRenderer.GetPosition(0) + "이(가) " + endPos + "에 연결되었습니다!");
                Debug.Log("현재 연결된 선의 개수: " + connectedPairs.Count / 2);


                // 게임 성공: 최대 연결 수에 도달했을 때
                if (connectedPairs.Count / 2 == maxConnections)
                {
                    Debug.Log("게임 완료!");
                    endMessage.SetActive(true);
                }
            }
            else
            {
                Destroy(currentLineRenderer.gameObject); // 이미 연결된 경우 삭제
                Debug.Log(currentLineRenderer.GetPosition(0) + "이(가) " + endPos + "에 이미 연결되었습니다.");
            }


            currentLineRenderer = null;
            isDrawing = false;
        }
    }

    public bool IsConnected(Vector3 start, Vector3 end)
    {
        return connectedPairs.Contains((start, end));
    }

    public bool CanSelect(GameObject obj)
    {
        return lastConnectedObject == null || lastConnectedObject == obj;
    }

    // 버튼 클릭 시 호출되는 메서드
    private void ResetLines()
    {
        // 현재 그려진 모든 라인 제거
        foreach (var lineRenderer in lineRenderers)
        {
            if (lineRenderer != null)
            {
                Destroy(lineRenderer.gameObject);
            }
        }

        // 상태 초기화
        lineRenderers.Clear();
        connectedPairs.Clear();
        lastConnectedObject = null;
        isDrawing = false;

        Debug.Log("라인 초기화 완료.");
    }

    private void LoadNextScene()
    {
        PuzzleClear puzzleClear = FindObjectOfType<PuzzleClear>();
        puzzleClear.CompletePuzzle();
    }
}
