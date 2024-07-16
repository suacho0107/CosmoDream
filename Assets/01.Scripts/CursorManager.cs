using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    Texture2D hand;
    Texture2D original;

    void Start()
    {
        original = Resources.Load<Texture2D>("cursor1");
        hand = Resources.Load<Texture2D>("cursor2");
    }

    public void OnMouseOver()
    {
        Cursor.SetCursor(hand, Vector2.zero, CursorMode.Auto);
    }

    public void OnMouseExit()
    {
        Cursor.SetCursor(original, new Vector2(0, 0), CursorMode.Auto);
    }
}