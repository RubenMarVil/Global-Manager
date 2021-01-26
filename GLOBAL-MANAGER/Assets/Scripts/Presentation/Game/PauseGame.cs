using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseGame : MonoBehaviour
{
    private bool Paused;

    void Start()
    {
        Paused = false;
    }

    void Update()
    {
        
    }

    public void PauseBtn()
    {
        Paused = !Paused;
        transform.GetChild(0).gameObject.SetActive(Paused);

        if(Paused)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
}
