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
    public List<Puzzle> puzzles; // 퍼즐 조각들의 리스트
    public Transform playerTransform; // Stage5에서의 플레이어 Transform
   // private string saveFilePath;

    void Start()
    {
        
        foreach (var puzzle in puzzles)
        {
            puzzle.SetPuzzleManager(this); // 각 퍼즐 조각에 PuzzleManager를 설정
        }
    }

    public void CheckPuzzleCompletion()
    {
        foreach (var puzzle in puzzles)
        {
            if (!puzzle.IsLocked()) // 퍼즐 조각이 고정되지 않은 경우
            {
                return; // 게임이 아직 완료되지 않음
            }
        }

        Debug.Log("게임 성공!"); // 모든 퍼즐 조각이 고정된 경우


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
