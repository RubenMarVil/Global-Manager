using Lean.Gui;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedLoupeHandle : MonoBehaviour
{
    private float[] xMargin = { -4.3f, -3.48f };
    private float yStart = 3.5f;
    private float yEnd = 0.6f;
    private float[] zMargin = { 1.4f, 1.64f };
    private float[] rotationMargin = { 100f, 220f };
    private bool falling;

    private GameObject windowEvent;
    private int maxNumEvent = 12;

    //public FeedbackHandle feedback;

    void Start()
    {
        Debug.Log("Red Loupe Event Created!");

        //feedback = GameObject.Find("/Canvas").GetComponent<FeedbackHandle>();

        System.Random rnd = new System.Random();

        float x = GetRandomFloat(rnd, xMargin[0], xMargin[1]);
        float z = GetRandomFloat(rnd, zMargin[0], zMargin[1]);
        transform.position = new Vector3(x, yStart, z);

        float rotation = GetRandomFloat(rnd, rotationMargin[0], rotationMargin[1]);
        transform.rotation = Quaternion.Euler(new Vector3(0, rotation, 0));

        falling = true;

        int eventNum = rnd.Next(1, maxNumEvent + 1);
        if(GameHandle.controlEvents.Count >= maxNumEvent)
        {
            GameHandle.controlEvents = GameHandle.controlEvents.GetRange(maxNumEvent - 5, 5);
        }
        while (GameHandle.controlEvents.Contains(eventNum))
        {
            eventNum = rnd.Next(1, maxNumEvent + 1);
        }

        Debug.Log($"[RED LOUPE HANDLE - INFO] Creating control event number {eventNum}");
        switch (eventNum)
        {
            case 1: GameHandle.coordinationEvents.Add(3); break;
            case 2: GameHandle.coordinationEvents.Add(4); break;
            case 4: GameHandle.communicationEvents.Add(2); break;
            case 5: GameHandle.coordinationEvents.Add(5); break;
            case 7: GameHandle.coordinationEvents.Add(7); break;
            case 8: GameHandle.coordinationEvents.Add(11); break;
            case 12: GameHandle.communicationEvents.Add(12); break;
        }
        GameHandle.controlEvents.Add(eventNum);

        string eventName = "/Questions/ControlRed" + eventNum;
        windowEvent = GameObject.Find(eventName);

        //feedback.StartNegativeStressEventLoupe();
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
