using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RedTelephoneHandle : MonoBehaviour
{
    private float[] xMargin = { 0.76f, 1.2f };
    private float yStart = 3.907f;
    private float yEnd = 0.907f;
    private float[] zMargin = { 0f, 1.6f };
    private float[] rotationMargin = { -60f, 14f };
    private bool falling;

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

    public void AlertObservers(string message)
    {
        if (message.Equals("ResetParameters"))
        {
            GetComponent<Animator>().SetBool("PointerEnter", false);
            GetComponent<Animator>().SetBool("Clicked", false);
        }
    }
}
