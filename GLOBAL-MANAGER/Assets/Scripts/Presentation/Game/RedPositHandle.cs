using JetBrains.Annotations;
using Lean.Gui;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedPositHandle : MonoBehaviour
{
    private float[] xMargin = { -5.42f, -3.53f };
    private float yStart = 3.5f;
    private float[] yMargin = { 1.33f, 2.34f };
    private float yEnd;
    private float z = 0.16f;
    private bool falling;

    private GameObject windowEvent;
    private int maxNumEvent = 13;

    //public FeedbackHandle feedback;

    void Start()
    {
        Debug.Log("Red Posit Event Created!");

        //feedback = GameObject.Find("/Canvas").GetComponent<FeedbackHandle>();

        System.Random rnd = new System.Random();

        float x = GetRandomFloat(rnd, xMargin[0], xMargin[1]);
        yEnd = GetRandomFloat(rnd, yMargin[0], yMargin[1]);
        transform.position = new Vector3(x, yStart, z);

        falling = true;

        int eventNum = rnd.Next(1, maxNumEvent + 1);
        if(GameHandle.coordinationEvents.Count >= maxNumEvent)
        {
            GameHandle.coordinationEvents = GameHandle.coordinationEvents.GetRange(maxNumEvent - 5, 5);
        }
        while (GameHandle.coordinationEvents.Contains(eventNum))
        {
            eventNum = rnd.Next(1, maxNumEvent + 1);
        }

        Debug.Log($"[RED POSIT HANDLE - INFO] Creating coordination event number {eventNum}");
        switch (eventNum)
        {
            case 2: GameHandle.communicationEvents.Add(1); break;
            case 3: GameHandle.controlEvents.Add(1); break;
            case 4: GameHandle.controlEvents.Add(2); break;
            case 5: GameHandle.controlEvents.Add(5); break;
            case 6: GameHandle.communicationEvents.Add(5); break;
            case 7: GameHandle.controlEvents.Add(7); break;
            case 8: GameHandle.communicationEvents.Add(7); break;
            case 11: GameHandle.controlEvents.Add(8); break;
        }
        GameHandle.coordinationEvents.Add(eventNum);

        string eventName = "/Questions/CoordinationRed" + eventNum;
        windowEvent = GameObject.Find(eventName);

        //feedback.StartNegativeStressEventPosit();
    }

    void Update()
    {
        if (falling)
        {
            if (transform.position.y > yEnd)
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
