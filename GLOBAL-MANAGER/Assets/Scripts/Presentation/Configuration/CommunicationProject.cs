using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommunicationProject : MonoBehaviour
{
    public List<ToggleButton> toolsList;
    public List<ToggleButton> toolsActives;

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
            tool.background.sprite = tool.buttonActive;

            GameObject newTool = new GameObject("Tool" + toolsActives.Count);
            newTool.AddComponent<Image>();
            newTool.GetComponent<Image>().sprite = tool.icon;
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
            tool.background.sprite = tool.buttonHover;

            for(int i = 0; i < panelActiveTools.transform.childCount; i++)
            {
                if(panelActiveTools.transform.GetChild(i).gameObject.GetComponent<Image>().sprite == tool.icon)
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
            tool.background.sprite = tool.buttonHover;
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
                tool.background.sprite = tool.buttonActive;
            }
            else if(tool.dissable)
            {
                tool.background.sprite = tool.buttonDissable;
            }
            else
            {
                tool.background.sprite = tool.buttonIdle;
            }
        }
    }

    public void SetDissableButtons()
    {
        foreach(ToggleButton tool in toolsList)
        {
            if(tool.active)
            {
                tool.background.sprite = tool.buttonActive;
            }
            else
            {
                tool.background.sprite = tool.buttonDissable;
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
                tool.background.sprite = tool.buttonIdle;
                tool.dissable = false;
            }
        }
    }

    void Start()
    {
        toolsActives = new List<ToggleButton>();
    }
}
