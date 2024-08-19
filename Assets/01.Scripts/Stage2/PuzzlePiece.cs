using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

	public class PuzzlePiece : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public int snapOffset = 30;
    public JigsawPuzzle puzzle;
    public GameObject PiecePos;
    public int piece_no;

    void Start()
{
    string name = gameObject.name;
    string numberString = "";

    // 이름의 끝에서부터 숫자를 추출
    for (int i = name.Length - 1; i >= 0; i--)
    {
        if (char.IsDigit(name[i]))
        {
            numberString = name[i] + numberString; // 숫자를 문자열로 추가
        }
        else
        {
            break;
        }
    }

    // 추출한 문자열이 비어있지 않다면
    if (numberString.Length > 0)
    {
        piece_no = int.Parse(numberString); // 문자열을 정수로 변환
    }
}


    bool CheckSnapPuzzle()
    {
        for (int i = 0; i < puzzle.puzzlePosSet.transform.childCount; i++)
        {
            //위치에 자식오브젝트가 있으면 이미 퍼즐조각이 놓여진 것
            if(puzzle.puzzlePosSet.transform.GetChild(i).childCount != 0)
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

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }
 
    public void OnEndDrag(PointerEventData eventData)
    { 
        if (!CheckSnapPuzzle())
        {
            transform.SetParent(puzzle.puzzlePieceSet.transform);
        }
 
        if (puzzle.IsClear())
        {
            Debug.Log("Clear");
        }
    }
}