using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LineManagerT : MonoBehaviour
{
    public GameObject linePrefab; // LineRenderer ������
    public Button resetButton; // �ʱ�ȭ ��ư
    public GameObject endMessage; // ���� �Ϸ� ĵ����
    public Button nextSceneButton; // ���� ������ ���� ��ư

    private LineRenderer currentLineRenderer;
    private List<LineRenderer> lineRenderers = new List<LineRenderer>();
    private HashSet<(Vector3, Vector3)> connectedPairs = new HashSet<(Vector3, Vector3)>();
    private bool isDrawing = false;
    private GameObject lastConnectedObject = null;
    private int maxConnections = 6;

    void Start()
    {
        // �ʱ�ȭ ��ư Ŭ�� �̺�Ʈ ����
        resetButton.onClick.AddListener(ResetLines);

        // ĵ������ ��ư �ʱ� ����
        if (endMessage != null)
        {
            endMessage.SetActive(false); // �⺻������ ĵ���� ��Ȱ��ȭ
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
                connectedPairs.Add((endPos, currentLineRenderer.GetPosition(0))); // ����� ����

                lastConnectedObject = connectedObject;
                // lastConnectedObject.GetComponent<LinkedObject>().SetSelected(true); // ���� ǥ��
                // Debug.Log(currentLineRenderer.GetPosition(0) + "��(��) " + endPos + "�� ����Ǿ����ϴ�!");
                Debug.Log("���� ����� ���� ����: " + connectedPairs.Count / 2);


                // ���� ����: �ִ� ���� ���� �������� ��
                if (connectedPairs.Count / 2 == maxConnections)
                {
                    Debug.Log("���� �Ϸ�!");
                    endMessage.SetActive(true);
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
            if (lineRenderer != null)
            {
                Destroy(lineRenderer.gameObject);
            }
        }

        // ���� �ʱ�ȭ
        lineRenderers.Clear();
        connectedPairs.Clear();
        lastConnectedObject = null;
        isDrawing = false;

        Debug.Log("���� �ʱ�ȭ �Ϸ�.");
    }

    private void LoadNextScene()
    {
        PuzzleClear puzzleClear = FindObjectOfType<PuzzleClear>();
        puzzleClear.CompletePuzzle();
    }
}
