using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public List<Puzzle> puzzles; // ���� �������� ����Ʈ

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
    }
}
