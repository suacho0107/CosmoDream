using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PuzzleManager : MonoBehaviour
{
    public List<Puzzle> puzzles; // ???? ???????? ??????
    public string sceneName;

    void Start()
    {
        foreach (var puzzle in puzzles)
        {
            puzzle.SetPuzzleManager(this); // ?? ???? ?????? PuzzleManager?? ????
        }
    }

    public void CheckPuzzleCompletion()
    {
        foreach (var puzzle in puzzles)
        {
            if (!puzzle.IsLocked()) // ???? ?????? ???????? ???? ????
            {
                return; // ?????? ???? ???????? ????
            }
        }

        Debug.Log("???? ????!"); // ???? ???? ?????? ?????? ????
        SceneManager.LoadScene(sceneName);
    }
}
