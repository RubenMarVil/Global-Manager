using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackHandle : MonoBehaviour
{
    public Animator anim;

    public void StartPositiveStressQuestion()
    {
        anim.SetTrigger("PositiveStressQuestion");
    }

    public void StartPositiveProgressQuestion()
    {
        anim.SetTrigger("PositiveProgressQuestion");
    }

    public void StartPositiveBudgetQuestion()
    {
        anim.SetTrigger("PositiveBudgetQuestion");
    }

    public void StartPositiveDurationQuestion()
    {
        anim.SetTrigger("PositiveDurationQuestion");
    }

    public void StartNegativeStressQuestion()
    {
        anim.SetTrigger("NegativeStressQuestion");
    }

    public void StartNegativeProgressQuestion()
    {
        anim.SetTrigger("NegativeProgressQuestion");
    }

    public void StartNegativeBudgetQuestion()
    {
        anim.SetTrigger("NegativeBudgetQuestion");
    }

    public void StartNegativeDurationQuestion()
    {
        anim.SetTrigger("NegativeDurationQuestion");
    }

    public void StartNegativeStressEventTelephone()
    {
        anim.SetTrigger("NegativeStressEventTelephone");
    }

    public void StartNegativeStressEventPosit()
    {
        anim.SetTrigger("NegativeStressEventPosit");
    }

    public void StartNegativeStressEventLoupe()
    {
        anim.SetTrigger("NegativeStressEventLoupe");
    }
}
