using UnityEngine;

public class Brush : MonoBehaviour
{
    private LineManagerT lineManager;
    private bool isDrawing = false;
    private SpriteRenderer spriteRenderer;
    void Start()
    {
        lineManager = FindObjectOfType<LineManagerT>();
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
        return (obj1.name == "BR1" || obj1.name == "BR2") &&
               (obj2.name == "BR1" || obj2.name == "BR2") ||
               (obj1.name == "BR1" || obj1.name == "BR3") &&
               (obj2.name == "BR1" || obj2.name == "BR3") ||
               (obj1.name == "BR2" || obj1.name == "BR3") &&
               (obj2.name == "BR2" || obj2.name == "BR3") ||
               (obj1.name == "BR2" || obj1.name == "BR4") &&
               (obj2.name == "BR2" || obj2.name == "BR4") ||
               (obj1.name == "BR3" || obj1.name == "BR5") &&
               (obj2.name == "BR3" || obj2.name == "BR5") ||
               (obj1.name == "BR4" || obj1.name == "BR5") &&
               (obj2.name == "BR4" || obj2.name == "BR5");
    }

}
