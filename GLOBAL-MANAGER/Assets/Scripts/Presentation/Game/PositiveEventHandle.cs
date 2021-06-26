using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositiveEventHandle : MonoBehaviour
{
    public float plusProgress;
    public float plusBudget;
    public float plusDuration;

    public FeedbackHandle feedback;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void OkBtn()
    {
        ProgressBar.AddValue(plusProgress);
        if (plusProgress > 0)
            feedback.StartPositiveProgressQuestion();
        else if (plusProgress < 0)
            feedback.StartNegativeProgressQuestion();

        BudgetBar.AddValue(plusBudget);
        if (plusBudget > 0)
            feedback.StartPositiveBudgetQuestion();
        else if (plusBudget < 0)
            feedback.StartNegativeBudgetQuestion();

        DurationBar.AddValue(plusDuration);
        if (plusDuration > 0)
            feedback.StartPositiveDurationQuestion();
        else if (plusDuration < 0)
            feedback.StartNegativeDurationQuestion();

        ClickEvent.DeleteEvent();

        GameHandle.NegativeWeight += UnityEngine.Random.Range(0.0f, GameHandle.positiveEventStreak * 10) / 100;

        GameHandle.NegativeWeight = (GameHandle.NegativeWeight > 1.0f) ? 1.0f : GameHandle.NegativeWeight;
    }
}
