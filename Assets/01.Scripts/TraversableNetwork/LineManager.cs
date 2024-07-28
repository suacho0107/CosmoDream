using System.Collections.Generic;
using UnityEngine;

public class LineManager : MonoBehaviour
{
    public GameObject linePrefab; // LineRenderer prefab

    private LineRenderer currentLineRenderer;
    private List<LineRenderer> lineRenderers = new List<LineRenderer>();
    private HashSet<(Vector3, Vector3)> connectedPairs = new HashSet<(Vector3, Vector3)>();
    private bool isDrawing = false;
    private GameObject lastConnectedObject = null;
    private const int maxConnections = 7;

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
                connectedPairs.Add((endPos, currentLineRenderer.GetPosition(0))); // Bidirectional connection

                if (lastConnectedObject != null && lastConnectedObject != connectedObject)
                {
                    //  lastConnectedObject.GetComponent<LinkedObject>().SetSelected(false);
                }

                lastConnectedObject = connectedObject;
                //lastConnectedObject.GetComponent<LinkedObject>().SetSelected(true);

                Debug.Log(currentLineRenderer.GetPosition(0) + " is connected to " + endPos);

                // 게임 성공
                if (connectedPairs.Count / 2 == maxConnections)
                {
                    Debug.Log("Game Complete!");
                    // 다음 맵으로 가는 코드 넣어야 함.
                }
            }
            else
            {
                Destroy(currentLineRenderer.gameObject); // 이미 연결되어있다면 지워짐
                Debug.Log(currentLineRenderer.GetPosition(0) + " is already connected to " + endPos);
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
}
