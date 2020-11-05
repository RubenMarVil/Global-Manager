using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ToggleGroupHandle : MonoBehaviour
{
    public ToggleGroup Group;

    public Toggle ToggleActive;

    void Start()
    {
        Group = GetComponent<ToggleGroup>();
    }

    void Update()
    {
        
    }

    public void ToggleChanged()
    {
        ToggleActive =  Group.ActiveToggles().ElementAt<Toggle>(0);
    }
}
