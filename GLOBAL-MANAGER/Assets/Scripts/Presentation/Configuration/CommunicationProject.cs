using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommunicationProject : MonoBehaviour
{
    public List<ToggleButton> toolsList;
    public List<ToggleButton> toolsActives;

    public Sprite buttonIdle;
    public Sprite buttonHover;
    public Sprite buttonActive;
    public Sprite buttonDissable;

    public GameObject panelActiveTools;

    public void Subscribe(ToggleButton tool)
    {
        if(toolsList == null)
        {
            toolsList = new List<ToggleButton>();
        }

        toolsList.Add(tool);
    }

    public void OnButtonClick(ToggleButton tool)
    {
        if(!tool.active && !tool.dissable)
        {
            tool.active = true;
            toolsActives.Add(tool);
            ResetButtons();
            tool.background.sprite = buttonActive;

            GameObject newTool = new GameObject("Tool" + toolsActives.Count);
            newTool.AddComponent<Image>();
            newTool.GetComponent<Image>().sprite = tool.transform.GetChild(0).GetComponent<Image>().sprite;
            newTool.GetComponent<RectTransform>().sizeDelta = new Vector2(130, 130);
            newTool.transform.SetParent(panelActiveTools.transform);
            newTool.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);

            if (toolsActives.Count == 3)
            {
                SetDissableButtons();
            }
        }
        else if(tool.active)
        {
            tool.active = false;
            toolsActives.Remove(tool);
            ResetButtons();
            tool.background.sprite = buttonHover;

            for(int i = 0; i < panelActiveTools.transform.childCount; i++)
            {
                if(panelActiveTools.transform.GetChild(i).gameObject.GetComponent<Image>().sprite == tool.transform.GetChild(0).GetComponent<Image>().sprite)
                {
                    Destroy(panelActiveTools.transform.GetChild(i).gameObject);
                }
            }

            if(toolsActives.Count == 2)
            {
                RemoveDissables();
            }
        }
    }

    public void OnButtonEnter(ToggleButton tool)
    {
        ResetButtons();
        if(!tool.active && !tool.dissable)
        {
            tool.background.sprite = buttonHover;
        }
    }

    public void OnButtonExit(ToggleButton tool)
    {
        ResetButtons();
    }

    public void ResetButtons()
    {
        foreach(ToggleButton tool in toolsList)
        {
            if(tool.active)
            {
                tool.background.sprite = buttonActive;
            }
            else if(tool.dissable)
            {
                tool.background.sprite = buttonDissable;
            }
            else
            {
                tool.background.sprite = buttonIdle;
            }
        }
    }

    public void SetDissableButtons()
    {
        foreach(ToggleButton tool in toolsList)
        {
            if(tool.active)
            {
                tool.background.sprite = buttonActive;
            }
            else
            {
                tool.background.sprite = buttonDissable;
                tool.dissable = true;
            }
        }
    }

    public void RemoveDissables()
    {
        foreach(ToggleButton tool in toolsList)
        {
            if(tool.dissable)
            {
                tool.background.sprite = buttonIdle;
                tool.dissable = false;
            }
        }
    }

    void Start()
    {
        toolsActives = new List<ToggleButton>();
    }
}
