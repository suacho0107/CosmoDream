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

        puzzle = FindObjectOfType<JigsawPuzzle>();
        image = GetComponent<Image>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (CheckSnapPuzzle())
        {
            puzzle.UpdatePuzzlePieceStatus(piece_no); // JigsawPuzzle에 조각 번호 전달
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
            if (Vector2.Distance(puzzle.puzzlePosSet.transform.GetChild(i).position, transform.position) < snapOffset)
            {
                transform.SetParent(puzzle.puzzlePosSet.transform.GetChild(i).transform);
                transform.localPosition = Vector3.zero;
                return true;
            }
        }
        return false;
    }
}