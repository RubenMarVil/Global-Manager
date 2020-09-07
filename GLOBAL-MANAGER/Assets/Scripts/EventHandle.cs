using Assets.Scripts.Control;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class EventHandle : MonoBehaviour
{
    public float[] StressImpacts;
    public float[] ProgressImpacts;
    public float[] BudgetImpacts;
    public float[] DurationImpacts;
    public bool[] CorrectAnswer;

    public string type;

    private Toggle Answer;

    private ToggleGroup AnswerGroup;

    private bool AnswerAny;

    void Start()
    {
        AnswerGroup = transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).transform.GetChild(5).GetComponent<ToggleGroup>();

        AnswerAny = false;
    }

    void Update()
    {
        try
        {
            Answer = AnswerGroup.ActiveToggles().ElementAt<Toggle>(0);
            AnswerAny = true;
        }
        catch (ArgumentOutOfRangeException)
        {
            AnswerAny = false;
        }
    }

    public void Solve()
    {
        if (AnswerAny)
        {
            int answerNumber = Convert.ToInt32(Answer.name.Substring(Answer.name.Length - 1)) - 1;

            StressBar.AddValue(StressImpacts[answerNumber]);
            ProgressBar.AddValue(ProgressImpacts[answerNumber]);
            BudgetBar.AddValue(BudgetImpacts[answerNumber]);
            DurationBar.AddValue(DurationImpacts[answerNumber]);

            transform.GetChild(0).GetChild(1).gameObject.GetComponent<Scrollbar>().value = 1;

            ClickEvent.DeleteEvent();

            Answer.isOn = false;

            if (CorrectAnswer[answerNumber])
            {
                switch(type)
                {
                    case "Communication": GameHandle.correctNegativeCommunicationEvents++; break;
                    case "Coordination": GameHandle.correctNegativeCoordinationEvents++; break;
                    case "Control": GameHandle.correctNegativeControlEvents++; break;
                }

                GameHandle.negativeFailureStreak = 0;

                GameHandle.TimePerEvent -= (GameHandle.TimePerEvent > 6) ? 2 : 0;
                GameHandle.NegativeWeight += UnityEngine.Random.Range(0.0f, (int)UserControl.actualUser.UserLevel) / 100;

                GameHandle.NegativeWeight = (GameHandle.NegativeWeight > 1.0f) ? 1.0f : GameHandle.NegativeWeight;
            }
            else
            {
                GameHandle.negativeFailureStreak++;

                GameHandle.TimePerEvent += (GameHandle.TimePerEvent < 24) ? 2 : 0;
                GameHandle.NegativeWeight -= UnityEngine.Random.Range(0.0f, (int)UserLevels.ADVANCED + 5 - (int)UserControl.actualUser.UserLevel + GameHandle.negativeFailureStreak) / 100;

                GameHandle.NegativeWeight = (GameHandle.NegativeWeight < 0.0f) ? 0.0f : GameHandle.NegativeWeight;
            }

            Debug.Log("NegativeWeight: " + GameHandle.NegativeWeight);

            GameHandle.NumEventsActive--;
        }
    }
}
