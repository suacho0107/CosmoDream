using UnityEngine;

public class Flower : MonoBehaviour
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
        return (obj1.name == "F1" || obj1.name == "F2") &&
               (obj2.name == "F1" || obj2.name == "F2") ||
               (obj1.name == "F1" || obj1.name == "F3") &&
               (obj2.name == "F1" || obj2.name == "F3") ||
               (obj1.name == "F2" || obj1.name == "F3") &&
               (obj2.name == "F2" || obj2.name == "F3") ||
               (obj1.name == "F2" || obj1.name == "F4") &&
               (obj2.name == "F2" || obj2.name == "F4") ||
               (obj1.name == "F2" || obj1.name == "F6") &&
               (obj2.name == "F2" || obj2.name == "F6") ||
               (obj1.name == "F3" || obj1.name == "F5") &&
               (obj2.name == "F3" || obj2.name == "F5") ||
               (obj1.name == "F3" || obj1.name == "F7") &&
               (obj2.name == "F3" || obj2.name == "F7") ||
               (obj1.name == "F4" || obj1.name == "F6") &&
               (obj2.name == "F4" || obj2.name == "F6") ||
               (obj1.name == "F5" || obj1.name == "F7") &&
               (obj2.name == "F5" || obj2.name == "F7") ||
               (obj1.name == "F6" || obj1.name == "F7") &&
               (obj2.name == "F6" || obj2.name == "F7") ||
               (obj1.name == "F6" || obj1.name == "F8") &&
               (obj2.name == "F6" || obj2.name == "F8") ||
               (obj1.name == "F7" || obj1.name == "F8") &&
               (obj2.name == "F7" || obj2.name == "F8");
    }

}
