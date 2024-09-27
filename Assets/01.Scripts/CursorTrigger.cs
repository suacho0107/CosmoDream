using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorTrigger : MonoBehaviour
{
    private CursorManager cursorManager;

    void Start()
    {
        cursorManager = CursorManager.Instance;
    }

    void OnMouseOver()
    {
        cursorManager.OnMouseOver();
    }

    void OnMouseExit()
    {
        cursorManager.OnMouseExit();
    }
}
