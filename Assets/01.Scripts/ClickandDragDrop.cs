using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickandDragDrop : MonoBehaviour
{
    private Camera cam;
    private Vector3 dragOffset;
    public float speed = 40f;

    void Awake()
    {
        cam = Camera.main;
    }

    void OnMouseDown()
    {
        dragOffset = transform.position - GetMousePos();
        // 클릭시 발생 이벤트는 이쪽에 추가하면 됩니다!
    }

    void OnMouseDrag()
    {
        transform.position = Vector3.MoveTowards
            (transform.position, GetMousePos() + dragOffset, speed * Time.deltaTime);
    }

    Vector3 GetMousePos()
    {
        var mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        return mousePos;
    }
}