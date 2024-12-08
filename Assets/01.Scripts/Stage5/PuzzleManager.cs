using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;

[System.Serializable]

public class GameSaveData
{
    public string name;
    public string lastScene;

}

public class PuzzleManager : MonoBehaviour
{
    public List<Puzzle> puzzles; // ���� �������� ����Ʈ
    public Transform playerTransform; // Stage5������ �÷��̾� Transform
                                      // private string saveFilePath;

    public GameObject endMessage; // ���� �޽��� ������Ʈ
    public Button endMessageButton; // ���� ��ư

    void Start()
    {
        foreach (var puzzle in puzzles)
        {
            puzzle.SetPuzzleManager(this); // �� ���� ������ PuzzleManager�� ����
        }

        if (endMessage != null)
        {
            endMessage.SetActive(false); // ���� �� endMessage ��Ȱ��ȭ
        }

        if (endMessageButton != null)
        {
            endMessageButton.onClick.AddListener(OnEndMessageButtonClicked); // ��ư Ŭ�� �̺�Ʈ �߰�
        }
    }

    public void CheckPuzzleCompletion()
    {
        foreach (var puzzle in puzzles)
        {
            if (!puzzle.IsLocked()) // ���� ������ �������� ���� ���
            {
                return; // ������ ���� �Ϸ���� ����
            }
        }

        Debug.Log("���� ����!"); // ��� ���� ������ ������ ���


        if (SceneManager.GetActiveScene().name == "1-6 Puzzle4")
        {
            ShowEndMessage(); // �޽��� ǥ��
        }
        else if (SceneManager.GetActiveScene().name == "car" ||
                 SceneManager.GetActiveScene().name == "airplane" ||
                 SceneManager.GetActiveScene().name == "Chicken")
        {
            CallPuzzleClear();
        }
        else if (SceneManager.GetActiveScene().name == "6-4")
        {
            BreakStage breakstage = FindObjectOfType<BreakStage>();
            breakstage.DontBreak();
        }

    }

    private void ShowEndMessage()
    {
        if (endMessage != null)
        {
            endMessage.SetActive(true); // �޽��� Ȱ��ȭ
        }
    }
    private void OnEndMessageButtonClicked()
    {
        if (endMessage != null)
        {
            endMessage.SetActive(false); // �޽��� ��Ȱ��ȭ
        }

        CallPuzzleClear(); // CallPuzzleClear ȣ��
    }

    private void CallPuzzleClear()
    {
        PuzzleClear puzzleClear = FindObjectOfType<PuzzleClear>();
        puzzleClear.CompletePuzzle();
    }
}
