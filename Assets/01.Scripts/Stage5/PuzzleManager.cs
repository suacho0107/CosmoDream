using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

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

    void Start()
    {
        
        foreach (var puzzle in puzzles)
        {
            puzzle.SetPuzzleManager(this); // �� ���� ������ PuzzleManager�� ����
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
            PuzzleClear puzzleClear = FindObjectOfType<PuzzleClear>();
            puzzleClear.CompletePuzzle();
        }
        else if(SceneManager.GetActiveScene().name == "car")
        {
            PuzzleClear puzzleClear = FindObjectOfType<PuzzleClear>();
            puzzleClear.CompletePuzzle();
            //SceneManager.LoadScene("stage5");
        }
        else if (SceneManager.GetActiveScene().name == "airplane")
        {
            PuzzleClear puzzleClear = FindObjectOfType<PuzzleClear>();
            puzzleClear.CompletePuzzle();
            //SceneManager.LoadScene("stage5");
        }
        else if (SceneManager.GetActiveScene().name == "chicken")
        {
            PuzzleClear puzzleClear = FindObjectOfType<PuzzleClear>();
            puzzleClear.CompletePuzzle();
            //SceneManager.LoadScene("stage5");
        }

    }
    
}
