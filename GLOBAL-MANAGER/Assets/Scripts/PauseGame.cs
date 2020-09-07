using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseGame : MonoBehaviour
{
    private bool Paused;

    public Sprite PlayImage;
    public Sprite PauseImage;

    void Start()
    {
        Paused = false;
        GetComponent<Image>().sprite = PauseImage;
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
            GetComponent<Image>().sprite = PlayImage;
            Time.timeScale = 0;
        }
        else
        {
            GetComponent<Image>().sprite = PauseImage;
            Time.timeScale = 1;
        }
    }
}
