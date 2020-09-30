using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorHandle : MonoBehaviour
{
    public Texture2D cursorPointer;
    public Texture2D cursorHand;
    private CursorMode cursorMode;
    private Vector2 hotSpot;

    void Start()
    {
        cursorMode = CursorMode.Auto;
        hotSpot = Vector2.zero;
    }

    public void SetPointerCursor()
    {
        Cursor.SetCursor(cursorPointer, hotSpot, cursorMode);
    }

    public void SetHandCursor()
    {
        Cursor.SetCursor(cursorHand, hotSpot, cursorMode);
    }
}
