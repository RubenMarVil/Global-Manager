using Lean.Gui;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenLoupeHandle : MonoBehaviour
{
    private float[] xMargin = { -4.3f, -3.48f };
    private float yStart = 3.5f;
    private float yEnd = 0.52f;
    private float[] zMargin = { 1.4f, 1.64f };
    private float[] rotationMargin = { 100f, 220f };
    private bool falling;

    private GameObject windowEvent;
    private int maxNumEvent = 3;

    void Start()
    {
        Debug.Log("Green Loupe Event Created!");

        System.Random rnd = new System.Random();

        float x = GetRandomFloat(rnd, xMargin[0], xMargin[1]);
        float z = GetRandomFloat(rnd, zMargin[0], zMargin[1]);
        transform.position = new Vector3(x, yStart, z);

        float rotation = GetRandomFloat(rnd, rotationMargin[0], rotationMargin[1]);
        transform.rotation = Quaternion.Euler(new Vector3(0, rotation, 0));

        falling = true;

        string eventName = "/Questions/ControlGreen" + rnd.Next(1, maxNumEvent + 1);
        windowEvent = GameObject.Find(eventName);
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
