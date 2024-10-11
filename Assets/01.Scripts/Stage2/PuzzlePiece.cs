using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PuzzlePiece : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    public int snapOffset = 30;
    public JigsawPuzzle puzzle;
    public GameObject PiecePos;
    public int piece_no;
    public bool isPuzzleActive = false;

    private Image image;

    void Start()
    {
        string name = gameObject.name;
        string numberString = "";

        for (int i = name.Length - 1; i >= 0; i--)
        {
            if (char.IsDigit(name[i]))
            {
                numberString = name[i] + numberString;
            }
            else
            {
                break;
            }
        }

        if (numberString.Length > 0)
        {
            piece_no = int.Parse(numberString);
        }

        image = GetComponent<Image>();
    }

    void Update()
    {
        // 스페이스 바를 누르면 퍼즐 풀기 시작
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isPuzzleActive = true;
            Debug.Log("Puzzle started! Arrange the pieces to complete.");
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (puzzle.IsPuzzleActive()) // 퍼즐이 활성화된 경우에만 드래그 가능
        {
            transform.SetAsLastSibling();
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // 스냅 위치에 맞지 않으면 조각은 현재 위치에 그대로 남아 있음
        CheckSnapPuzzle();

        if (puzzle.IsClear())
        {
            Debug.Log("Clear");
        }
    }

    bool CheckSnapPuzzle()
    {
        for (int i = 0; i < puzzle.puzzlePosSet.transform.childCount; i++)
        {
            if (puzzle.puzzlePosSet.transform.GetChild(i).childCount != 0)
            {
                continue;
            }
            else if (Vector2.Distance(puzzle.puzzlePosSet.transform.GetChild(i).position, transform.position) < snapOffset)
            {
                transform.SetParent(puzzle.puzzlePosSet.transform.GetChild(i).transform);
                transform.localPosition = Vector3.zero;
                return true;
            }
        }
        return false;
    }
}