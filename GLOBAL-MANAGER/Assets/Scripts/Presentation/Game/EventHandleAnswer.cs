using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventHandleAnswer : MonoBehaviour
{
    public float[] StressImpacts;
    public float[] ProgressImpacts;
    public float[] BudgetImpacts;
    public float[] DurationImpacts;
    public bool[] CorrectAnswer;

    public Sprite[] sprite_idle;

    public string type;

    void Start()
    {
        Transform contentQuestion = transform.GetChild(0).GetChild(0);
        sprite_idle = new Sprite[] { contentQuestion.GetChild(1).GetComponent<Image>().sprite, contentQuestion.GetChild(2).GetComponent<Image>().sprite,
                                     contentQuestion.GetChild(3).GetComponent<Image>().sprite };
    }

    void Update()
    {
        
    }  

    public void Answer(int i)
    {
        StressBar.AddValue(StressImpacts[i]);
        ProgressBar.AddValue(ProgressImpacts[i]);
        BudgetBar.AddValue(BudgetImpacts[i]);
        DurationBar.AddValue(DurationImpacts[i]);

        ClickEvent.DeleteEvent();

        if (CorrectAnswer[i])
        {
            switch (type)
            {
                case "Communication": GameHandle.correctNegativeCommunicationEvents++; break;
                case "Coordination": GameHandle.correctNegativeCoordinationEvents++; break;
                case "Control": GameHandle.correctNegativeControlEvents++; break;
            }

            GameHandle.negativeFailureStreak = 0;

            GameHandle.UpdateWorkOfWorkerPerDay(RecommendConfiguration.GetProdWorkerDayCorrect());
        }
        else
        {
            GameHandle.negativeFailureStreak++;

            GameHandle.UpdateWorkOfWorkerPerDay(RecommendConfiguration.GetProdWorkerDayFailure());
        }

        GameHandle.NumEventsActive--;

        transform.GetChild(0).GetChild(0).GetChild(i + 1).GetComponent<Image>().sprite = sprite_idle[i];
    }
}
