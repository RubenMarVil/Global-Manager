using Lean.Gui;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RedTelephoneHandle : MonoBehaviour
{
    private float[] xMargin = { 0.68f, 0.95f };
    private float yStart = 3.907f;
    private float yEnd = 1.0f;
    private float[] zMargin = { 0f, 1.52f };
    private float[] rotationMargin = { -60f, 14f };
    private bool falling;

    private GameObject windowEvent;
    private int maxNumEvent = 11;

    void Start()
    {
        Debug.Log("Red Telephone Event Created!");

        System.Random rnd = new System.Random();

        float x = GetRandomFloat(rnd, xMargin[0], xMargin[1]);
        float z = GetRandomFloat(rnd, zMargin[0], zMargin[1]);
        transform.position = new Vector3(x, yStart, z);

        float rotation = GetRandomFloat(rnd, rotationMargin[0], rotationMargin[1]);
        transform.rotation = Quaternion.Euler(new Vector3(0, rotation, 0));

        falling = true;

        int eventNum = rnd.Next(1, maxNumEvent + 1);
        /*while(GameHandle.communicationEvents.Contains(eventNum))
        {
            eventNum = rnd.Next(1, maxNumEvent + 1);
        }*/

        Debug.Log($"[RED TELEPHONE HANDLE - INFO] Creating communication event number {eventNum}");
        GameHandle.communicationEvents.Add(eventNum);

        string eventName = "/Questions/CommunicationRed" + eventNum;
        windowEvent = GameObject.Find(eventName);
    }

    void Update()
    {
        if(falling)
        {
            if(transform.position.y > yEnd)
            {
                transform.position -= new Vector3(0, GameHandle.dropVelocity * Time.deltaTime, 0);
            }
            else
            {
                falling = false;
            }
        }
    }

    private float GetRandomFloat(System.Random rnd, float minValue, float maxValue)
    {
        var result = (rnd.NextDouble() * (maxValue - (double)minValue)) + minValue;

        return (float)result;
    }

    public void ShowEvent()
    {
        windowEvent.GetComponent<LeanWindow>().TurnOn();
        //windowEvent.transform.GetChild(0).gameObject.SetActive(true);
    }

    public void DeleteEvent()
    {
        windowEvent.GetComponent<LeanWindow>().TurnOff();
        //windowEvent.transform.GetChild(0).gameObject.SetActive(false);
    }
}
