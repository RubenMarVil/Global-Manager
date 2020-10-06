using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositiveEventHandle : MonoBehaviour
{
    public float plusProgress;
    public float plusBudget;
    public float plusDuration;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void OkBtn()
    {
        BudgetBar.AddValue(plusBudget);
        DurationBar.AddValue(plusDuration);

        ClickEvent.DeleteEvent();


        GameHandle.NegativeWeight += UnityEngine.Random.Range(0.0f, GameHandle.positiveEventStreak * 10) / 100;

        GameHandle.NegativeWeight = (GameHandle.NegativeWeight > 1.0f) ? 1.0f : GameHandle.NegativeWeight;
    }
}
