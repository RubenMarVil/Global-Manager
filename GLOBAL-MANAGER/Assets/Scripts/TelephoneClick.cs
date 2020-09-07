using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelephoneClick : MonoBehaviour
{
    private Animator animPlayer;

    void Awake()
    {
        animPlayer = GameObject.FindGameObjectWithTag("Players").GetComponent<Animator>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void TelephoneClicked()
    {
        animPlayer.SetTrigger("TelephoneEvent");
    }
}
