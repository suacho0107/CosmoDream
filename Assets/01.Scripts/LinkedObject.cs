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
        // obj1과 obj2가 가능한 연결 조합에 포함되어 있는지 확인
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
