using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LineManagerT : MonoBehaviour
{
    public GameObject linePrefab; // LineRenderer ������
    public Button resetButton; // �ʱ�ȭ ��ư
                               // public string sceneType; // �� ������ ���� ���� ("Flower" �Ǵ� "Building")

    private LineRenderer currentLineRenderer;
    private List<LineRenderer> lineRenderers = new List<LineRenderer>();
    private HashSet<(Vector3, Vector3)> connectedPairs = new HashSet<(Vector3, Vector3)>();
    private bool isDrawing = false;
    private GameObject lastConnectedObject = null;
    private int maxConnections;

    void Start()
    {
        // ���� ���� �̸��� ���� maxConnections ����
        string sceneName = SceneManager.GetActiveScene().name;

        if (sceneName == "1-6 Puzzle2")
        {
            maxConnections = 6;
        }
        // �ʱ�ȭ ��ư Ŭ�� �̺�Ʈ ����
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
                connectedPairs.Add((endPos, currentLineRenderer.GetPosition(0))); // ����� ����

                if (lastConnectedObject != null && lastConnectedObject != connectedObject)
                {
                    // lastConnectedObject.GetComponent<LinkedObject>().SetSelected(false); // ���� ����
                }

                lastConnectedObject = connectedObject;
                // lastConnectedObject.GetComponent<LinkedObject>().SetSelected(true); // ���� ǥ��

                Debug.Log(currentLineRenderer.GetPosition(0) + "��(��) " + endPos + "�� ����Ǿ����ϴ�!");

                // ���� ����: �ִ� ���� ���� �������� ��
                if (connectedPairs.Count / 2 == maxConnections)
                {
                    Debug.Log("���� �Ϸ�!");
                    SceneManager.LoadScene("Stage3"); //�� ����
                }
            }
            else
            {
                Destroy(currentLineRenderer.gameObject); // �̹� ����� ��� ����
                Debug.Log(currentLineRenderer.GetPosition(0) + "��(��) " + endPos + "�� �̹� ����Ǿ����ϴ�.");
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

    // ��ư Ŭ�� �� ȣ��Ǵ� �޼���
    private void ResetLines()
    {
        // ���� �׷��� ��� ���� ����
        foreach (var lineRenderer in lineRenderers)
        {
            Destroy(lineRenderer.gameObject);
        }

        // ���� �ʱ�ȭ
        lineRenderers.Clear();
        connectedPairs.Clear();
        lastConnectedObject = null;
        isDrawing = false;

        Debug.Log("���� �ʱ�ȭ �Ϸ�.");
    }
}
