using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LineManagerT : MonoBehaviour
{
    public GameObject linePrefab; // LineRenderer 프리팹
    public Button resetButton; // 초기화 버튼
                               // public string sceneType; // 씬 구분을 위한 변수 ("Flower" 또는 "Building")

    private LineRenderer currentLineRenderer;
    private List<LineRenderer> lineRenderers = new List<LineRenderer>();
    private HashSet<(Vector3, Vector3)> connectedPairs = new HashSet<(Vector3, Vector3)>();
    private bool isDrawing = false;
    private GameObject lastConnectedObject = null;
    private int maxConnections;

    void Start()
    {
        // 현재 씬의 이름에 따라 maxConnections 설정
        string sceneName = SceneManager.GetActiveScene().name;

        if (sceneName == "1-6 Puzzle2")
        {
            maxConnections = 6;
        }
        // 초기화 버튼 클릭 이벤트 설정
        resetButton.onClick.AddListener(ResetLines);
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

                if (lastConnectedObject != null && lastConnectedObject != connectedObject)
                {
                    // lastConnectedObject.GetComponent<LinkedObject>().SetSelected(false); // 선택 해제
                }

                lastConnectedObject = connectedObject;
                // lastConnectedObject.GetComponent<LinkedObject>().SetSelected(true); // 선택 표시

                Debug.Log(currentLineRenderer.GetPosition(0) + "이(가) " + endPos + "에 연결되었습니다!");

                // 게임 성공: 최대 연결 수에 도달했을 때
                if (connectedPairs.Count / 2 == maxConnections)
                {
                    Debug.Log("게임 완료!");
                    SceneManager.LoadScene("Stage3"); //씬 변경
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
            Destroy(lineRenderer.gameObject);
        }

        // 상태 초기화
        lineRenderers.Clear();
        connectedPairs.Clear();
        lastConnectedObject = null;
        isDrawing = false;

        Debug.Log("라인 초기화 완료.");
    }
}
