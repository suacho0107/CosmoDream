using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public List<Puzzle> puzzles; // 퍼즐 조각들의 리스트

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
    }
}
