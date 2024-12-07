using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TV : MonoBehaviour
{
    private LineManager lineManager;
    private bool isDrawing = false;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        lineManager = FindObjectOfType<LineManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = true; // ó������ ��� ���̵��� ����
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
                Debug.Log(hit.collider.gameObject.name + "�� ����Ǿ����ϴ�!");
            }
            else
            {
                lineManager.EndCurrentLine(transform.position, null);
                Debug.Log("������ �� �����ϴ�.");
            }
        }
        else
        {
            lineManager.EndCurrentLine(transform.position, null);
            Debug.Log("���� ����");
        }
    }

    private bool IsPosConnection(GameObject obj1, GameObject obj2)
    {
        // obj1�� obj2�� ������ ���� ���տ� ���ԵǾ� �ִ��� Ȯ��
        return (obj1.name == "B1" || obj1.name == "B2") &&
               (obj2.name == "B1" || obj2.name == "B2") ||
               (obj1.name == "B1" || obj1.name == "B3") &&
               (obj2.name == "B1" || obj2.name == "B3") ||
               (obj1.name == "B2" || obj1.name == "B3") &&
               (obj2.name == "B2" || obj2.name == "B3") ||
               (obj1.name == "B3" || obj1.name == "B4") &&
               (obj2.name == "B3" || obj2.name == "B4") ||
               (obj1.name == "B4" || obj1.name == "B1") &&
               (obj2.name == "B4" || obj2.name == "B1") ||
               (obj1.name == "B4" || obj1.name == "B5") &&
               (obj2.name == "B4" || obj2.name == "B5") ||
               (obj1.name == "B5" || obj1.name == "B6") &&
               (obj2.name == "B5" || obj2.name == "B6") ||
               (obj1.name == "B6" || obj1.name == "B7") &&
               (obj2.name == "B7" || obj2.name == "B8") ||
               (obj1.name == "B7" || obj1.name == "B8") &&
               (obj2.name == "B8" || obj2.name == "B9") ||
               (obj1.name == "B9" || obj1.name == "B10") &&
               (obj2.name == "B9" || obj2.name == "B10") ||
               (obj1.name == "B10" || obj1.name == "B11") &&
               (obj2.name == "B10" || obj2.name == "B11") ||
               (obj1.name == "B11" || obj1.name == "B12") &&
               (obj2.name == "B11" || obj2.name == "B12") ||
               (obj1.name == "B12" || obj1.name == "B13") &&
               (obj2.name == "B12" || obj2.name == "B13") ||
               (obj1.name == "B13" || obj1.name == "B14") &&
               (obj2.name == "B13" || obj2.name == "B14") ||
               (obj1.name == "B14" || obj1.name == "B15") &&
               (obj2.name == "B14" || obj2.name == "B15") ||
               (obj1.name == "B15" || obj1.name == "B16") &&
               (obj2.name == "B15" || obj2.name == "B16") ||
               (obj1.name == "B16" || obj1.name == "B5") &&
               (obj2.name == "B16" || obj2.name == "B5")
               ;
    }

}
