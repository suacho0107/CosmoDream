using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle2Brush : MonoBehaviour
{
    private LineManager lineManager;
    private bool isDrawing = false;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        lineManager = FindObjectOfType<LineManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = true; // 처음에는 모두 보이도록 설정
    }

    private void OnMouseDown()
    {
        if (!lineManager.CanSelect(gameObject))
            return;

        isDrawing = true;
        lineManager.StartNewLine(transform.position);
    }

    private void OnMouseUp()
    {
        if (!isDrawing)
            return;

        isDrawing = false;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

        if (hit.collider != null && hit.collider.gameObject != gameObject)
        {
            if (IsPosConnection(gameObject, hit.collider.gameObject))
            {
                lineManager.EndCurrentLine(hit.collider.gameObject.transform.position, hit.collider.gameObject);
                Debug.Log(hit.collider.gameObject.name + "에 연결되었습니다!");
            }
            else
            {
                lineManager.EndCurrentLine(transform.position, null);
                Debug.Log("연결할 수 없습니다.");
            }
        }
        else
        {
            lineManager.EndCurrentLine(transform.position, null);
            Debug.Log("연결 실패");
        }
    }

    private bool IsPosConnection(GameObject obj1, GameObject obj2)
    {
        switch (obj1.name)
        {
            case "GC1":
                return obj2.name == "GC2" || obj2.name == "GC3" || obj2.name == "GC5";
            case "GC2":
                return obj2.name == "GC1" || obj2.name == "GC3";
            case "GC3":
                return obj2.name == "GC1" || obj2.name == "GC2" || obj2.name == "GC4";
            case "GC4":
                return obj2.name == "GC3" || obj2.name == "GC5";
            case "GC5":
                return obj2.name == "GC1" || obj2.name == "GC4";
            default:
                return false;
        }
    }
}