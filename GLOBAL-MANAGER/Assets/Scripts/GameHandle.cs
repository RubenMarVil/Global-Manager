using UnityEngine;
using System;

public class GameHandle : MonoBehaviour
{
    public GameObject[] typesEvents;

    public int TimeVelocity;
    private int CountFrames;

    public static float dropVelocity = 1.5f;

    void Start()
    {
        CountFrames = 0;
    }

    void Update()
    {
        CountFrames++;
        Debug.Log(CountFrames);

        if(CountFrames >= TimeVelocity)
        {
            GenerateEvent();

            CountFrames = 0;
        }
    }

    private void GenerateEvent()
    {
        System.Random rng = new System.Random();

        Instantiate(typesEvents[0]);
        //Instantiate(typesEvents[rng.Next(0, typesEvents.Length - 1)]);
    }
}
