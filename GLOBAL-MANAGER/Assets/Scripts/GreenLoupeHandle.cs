using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenLoupeHandle : MonoBehaviour
{
    private float[] xMargin = { -4.4f, -3.52f };
    private float yStart = 3.5f;
    private float yEnd = 0.57f;
    private float[] zMargin = { 1.53f, 2f };
    private float[] rotationMargin = { 100f, 220f };
    private bool falling;

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
}
