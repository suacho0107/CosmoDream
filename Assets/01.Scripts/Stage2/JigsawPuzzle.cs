using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JigsawPuzzle : MonoBehaviour
{
    private JigsawManager jigsawManager;
    public Transform puzzlePosSet;
    public Transform puzzlePieceSet;
    public int puzzleNumber;
    public bool isPuzzleActive = false;

    public GameObject startMessage;
    public GameObject clearMessage;

    void Start()
    {
        jigsawManager = FindObjectOfType<JigsawManager>();
        if (startMessage != null) startMessage.SetActive(true);
        if (clearMessage != null) clearMessage.SetActive(false);
    }

    public void HideStartUI()
    {
        if (startMessage != null) startMessage.SetActive(false);
    }
    
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
        jigsawManager.CompletePuzzle(puzzleNumber);
        if (clearMessage != null) clearMessage.SetActive(true);
        return true;
    }

    void Update()
    {
        // 스페이스 바로 퍼즐 풀기 시작
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartPuzzle();
        }
    }

    public void StartPuzzle()
    {
        // 시작 메시지 숨기기
        if (startMessage != null) startMessage.SetActive(false);
        isPuzzleActive = true;
    }

    public bool IsPuzzleActive()
    {
        return isPuzzleActive;
    }

    public void GoToNextScene()
    {
        // 원하는 다음 씬 번호로 변경
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void RetryGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}