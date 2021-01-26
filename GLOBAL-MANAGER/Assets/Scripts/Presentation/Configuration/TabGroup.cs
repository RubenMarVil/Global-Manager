using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabGroup : MonoBehaviour
{
    public List<TabButton> tabButtons;
    public TabButton selectedTab;
    public List<GameObject> objectsToSwap;
    
    public void Subscribe(TabButton button)
    {
        if(tabButtons == null)
        {
            tabButtons = new List<TabButton>();
        }

        tabButtons.Add(button);
    }

    public void OnTabEnter(TabButton button)
    {
        ResetTabs();
        if (selectedTab == null || button != selectedTab)
        {
            button.background.sprite = button.tabHover;
        }
    }

    public void OnTabExit(TabButton button)
    {
        ResetTabs();
    }

    public void OnTabSelected(TabButton button)
    {
        selectedTab = button;
        ResetTabs();
        button.background.sprite = button.tabActive;
        int index = button.transform.GetSiblingIndex();
        for(int i = 0; i < objectsToSwap.Count; i++)
        {
            if(i == index)
            {
                objectsToSwap[i].SetActive(true);
            }
            else
            {
                objectsToSwap[i].SetActive(false);
            }
        }
    }

    public void SetDissable(TabButton button)
    {
        button.background.sprite = button.tabDissable;

        if(button.site)
        {
            button.siteName.text = "";
        }
    }

    public void SetAble(TabButton button)
    {
        button.background.sprite = button.tabIdle;

        if (button.site)
        {
            button.siteName.text = button.name.Insert(4, " ");
        }
    }

    public void ResetTabs()
    {
        foreach(TabButton button in tabButtons)
        {
            if (selectedTab != null && selectedTab == button) { continue; }
            if(button.dissable)
            {
                button.background.sprite = button.tabDissable;

                if (button.site)
                {
                    button.siteName.text = "";
                }
            }
            else
            {
                button.background.sprite = button.tabIdle;

                if (button.site)
                {
                    button.siteName.text = button.name.Insert(4, " ");
                }
            }
            
        }
    }
}
