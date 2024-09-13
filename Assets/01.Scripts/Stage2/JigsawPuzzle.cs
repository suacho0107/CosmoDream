using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JigsawPuzzle : MonoBehaviour
{
    public Transform puzzlePosSet;
    public Transform puzzlePieceSet;

    public bool IsClear()
    {
        for (int i = 0; i < puzzlePosSet.transform.childCount; i++)
        {
            if (puzzlePosSet.transform.GetChild(i).childCount == 0)
            {
                return false;
            }

            if (puzzlePosSet.transform.GetChild(i).GetChild(0).GetComponent<PuzzlePiece>().piece_no != i+1)
            {
                return false;
            }
        }
        return true;
    }

    public void RetryGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}