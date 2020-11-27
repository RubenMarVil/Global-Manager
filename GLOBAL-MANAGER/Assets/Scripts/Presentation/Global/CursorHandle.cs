using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        if (GetComponent<Button>().interactable)
        {
            Cursor.SetCursor(cursorHand, hotSpot, cursorMode);
        }
    }
}
