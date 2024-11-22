using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JigsawPuzzle : MonoBehaviour
{
    PuzzleClear puzzleClear;

    public Transform puzzlePosSet;
    public Transform puzzlePieceSet;
    public int puzzleNumber; // 현재 퍼즐이 몇번째 퍼즐인지

    HashSet<int> completedPieces = new HashSet<int>(); // 맞춘 퍼즐 조각의 번호를 저장
    
    public GameObject startMessage;
    public GameObject clearMessage;

    void Start()
    {
        Debug.Log(puzzlePosSet.childCount);
        puzzleClear = FindObjectOfType<PuzzleClear>();
        if (startMessage != null) startMessage.SetActive(true);
        if (clearMessage != null) clearMessage.SetActive(false);
    }

    public void UpdatePuzzlePieceStatus(int pieceNumber)
    {
        // 퍼즐 조각이 맞춰진 상태를 추가
        if (!completedPieces.Contains(pieceNumber))
        {
            completedPieces.Add(pieceNumber); 
            Debug.Log($"{pieceNumber}번 퍼즐 맞춤");
            Debug.Log(completedPieces.Count);
        }

        // 퍼즐 완료 여부를 확인
        IsClear();
    }

    public bool IsClear()
    {
        if (completedPieces.Count == puzzlePosSet.childCount)
        {
            // 모든 퍼즐 조각이 맞춰졌다면 End UI 활성화
            if (clearMessage != null)
            {
                clearMessage.SetActive(true);
                Debug.Log("클리어");
            }
            return true;
        }
        return false;
    }

    public void HideStartUI()
    {
        if (startMessage != null) startMessage.SetActive(false);
        if (clearMessage != null) clearMessage.SetActive(false);
    }

    public void NextStageButton()
    {
        puzzleClear.CompletePuzzle();
    }

    public void RetryGame()
    {
        completedPieces.Clear(); // 맞춘 퍼즐 상태 초기화

        // 각 퍼즐 조각을 원래 위치로 리셋
        foreach (Transform piece in puzzlePieceSet)
        {
            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.buildIndex); // 현재 씬 다시 로드
        }
    }
}