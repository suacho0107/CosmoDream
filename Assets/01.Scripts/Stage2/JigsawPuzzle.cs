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
        }

        // 퍼즐 완료 여부를 확인
        IsClear();
    }

    public bool IsClear()
    {
        if (completedPieces.Count == puzzlePosSet.childCount)
        {
            // 모든 퍼즐 조각이 맞춰졌다면 End UI 활성화
            if (clearMessage != null) clearMessage.SetActive(true);
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
}