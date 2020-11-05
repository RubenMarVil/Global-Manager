using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public enum ToolType
{
    SYNCHRONOUS,
    ASYNCHRONOUS
}

public class ToggleButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    public CommunicationProject communicationHandle;

    public Image background;

    public bool active;
    public bool dissable;

    public ToolType type;

    public void OnPointerClick(PointerEventData eventData)
    {
        communicationHandle.OnButtonClick(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        communicationHandle.OnButtonEnter(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        communicationHandle.OnButtonExit(this);
    }

    void Start()
    {
        background = GetComponent<Image>();

        communicationHandle.Subscribe(this);

        active = false;
        dissable = false;
    }
}
