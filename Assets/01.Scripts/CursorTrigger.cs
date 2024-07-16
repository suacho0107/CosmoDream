using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorTrigger : MonoBehaviour
{
    public CursorManager cursorManager;

    void OnMouseOver()
    {
        cursorManager.OnMouseOver();
    }

    void OnMouseExit()
    {
        cursorManager.OnMouseExit();
    }
}
