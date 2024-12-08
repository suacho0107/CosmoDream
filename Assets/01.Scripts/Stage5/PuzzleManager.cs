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
    public List<Puzzle> puzzles; // 퍼즐 조각들의 리스트
    public Transform playerTransform; // Stage5에서의 플레이어 Transform
                                      // private string saveFilePath;

    public GameObject endMessage; // 성공 메시지 오브젝트
    public Button endMessageButton; // 종료 버튼

    void Start()
    {
        foreach (var puzzle in puzzles)
        {
            puzzle.SetPuzzleManager(this); // 각 퍼즐 조각에 PuzzleManager를 설정
        }

        if (endMessage != null)
        {
            endMessage.SetActive(false); // 시작 시 endMessage 비활성화
        }

        if (endMessageButton != null)
        {
            endMessageButton.onClick.AddListener(OnEndMessageButtonClicked); // 버튼 클릭 이벤트 추가
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
            ShowEndMessage(); // 메시지 표시
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
            endMessage.SetActive(true); // 메시지 활성화
        }
    }
    private void OnEndMessageButtonClicked()
    {
        if (endMessage != null)
        {
            endMessage.SetActive(false); // 메시지 비활성화
        }

        CallPuzzleClear(); // CallPuzzleClear 호출
    }

    private void CallPuzzleClear()
    {
        PuzzleClear puzzleClear = FindObjectOfType<PuzzleClear>();
        puzzleClear.CompletePuzzle();
    }
}
