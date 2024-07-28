using UnityEngine;

public class LinkedObject : MonoBehaviour
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
        return (obj1.name == "Circle1" || obj1.name == "Circle2") &&
               (obj2.name == "Circle1" || obj2.name == "Circle2") ||
               (obj1.name == "Circle1" || obj1.name == "Circle3") &&
               (obj2.name == "Circle1" || obj2.name == "Circle3") ||
               (obj1.name == "Circle2" || obj1.name == "Circle3") &&
               (obj2.name == "Circle2" || obj2.name == "Circle3") ||
               (obj1.name == "Circle3" || obj1.name == "Circle4") &&
               (obj2.name == "Circle3" || obj2.name == "Circle4") ||
               (obj1.name == "Circle4" || obj1.name == "Circle5") &&
               (obj2.name == "Circle4" || obj2.name == "Circle5") ||
               (obj1.name == "Circle4" || obj1.name == "Circle6") &&
               (obj2.name == "Circle4" || obj2.name == "Circle6") ||
               (obj1.name == "Circle5" || obj1.name == "Circle6") &&
               (obj2.name == "Circle5" || obj2.name == "Circle6");
    }

    //public void SetSelected(bool selected)
    //{
    //    //spriteRenderer.enabled = selected;
    //}
}
