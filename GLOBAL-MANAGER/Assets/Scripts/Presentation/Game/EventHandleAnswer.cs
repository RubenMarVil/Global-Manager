using Panda;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class EventHandleAnswer : MonoBehaviour
{
    private Transform contentQuestion;

    public float[] StressImpacts;
    public float[] ProgressImpacts;
    public float[] BudgetImpacts;
    public float[] DurationImpacts;
    public bool[] CorrectAnswer;

    public Color errorColor;
    public Color correctColor;
    public Color idleColor;

    public Sprite[] sprite_idle;

    public string type;

    public AudioSource sound;

    public AudioClip correctAudio;
    public AudioClip errorAudio;

    public FeedbackHandle feedback;

    public bool answered;

    void Start()
    {
        contentQuestion = transform.GetChild(0).GetChild(0);
        sprite_idle = new Sprite[] { contentQuestion.GetChild(1).GetComponent<Image>().sprite, contentQuestion.GetChild(2).GetComponent<Image>().sprite,
                                     contentQuestion.GetChild(3).GetComponent<Image>().sprite };

        answered = false;
    }

    void Update()
    {
        
    }  

    public void Answer(int i)
    {
        StartCoroutine(SetAnswer(i));
    }

    public IEnumerator SetAnswer(int i)
    {
        if (!answered)
        {
            float secondsActive = 0f;

            answered = true;

            GameObject selectedAnswer = contentQuestion.GetChild(i + 1).gameObject;
            selectedAnswer.GetComponent<Image>().sprite = selectedAnswer.GetComponent<Button>().spriteState.highlightedSprite;

            StressBar.AddValue(StressImpacts[i]);
            if (StressImpacts[i] > 0)
            {
                feedback.StartNegativeStressQuestion();
                secondsActive += 1.15f;
            }
            else if (StressImpacts[i] < 0)
            {
                feedback.StartPositiveStressQuestion();
                secondsActive += 1.15f;
            }

            float progressRepercussion = 0f;
            if (ProgressImpacts[i] < 0)
            {
                progressRepercussion = (1 + (Convert.ToSingle((int)GameConfigurationControl.actualGameConfiguration.ProjectDifficulty) / 10f)) * ProgressImpacts[i];
            }
            else
            {
                progressRepercussion = (1 - (Convert.ToSingle((int)GameConfigurationControl.actualGameConfiguration.ProjectDifficulty) / 10f)) * ProgressImpacts[i];
            }
            ProgressBar.AddValue(progressRepercussion);
            if (progressRepercussion > 0)
            {
                feedback.StartPositiveProgressQuestion();
                secondsActive += 1.15f;
            }
            else if (progressRepercussion < 0)
            {
                feedback.StartNegativeProgressQuestion();
                secondsActive += 1.15f;
            }

            float budgetRepercussion = BudgetImpacts[i];
            if (BudgetImpacts[i] < 0)
            {
                budgetRepercussion = (1 + (Convert.ToSingle((int)GameConfigurationControl.actualGameConfiguration.ProjectDifficulty) / 10f)) * BudgetImpacts[i];
            }
            BudgetBar.AddValue(budgetRepercussion);
            if (budgetRepercussion > 0)
            {
                feedback.StartPositiveBudgetQuestion();
                secondsActive += 1.15f;
            }
            else if (budgetRepercussion < 0)
            {
                feedback.StartNegativeBudgetQuestion();
                secondsActive += 1.15f;
            }

            float durationRepercussion = DurationImpacts[i];
            if (DurationImpacts[i] < 0)
            {
                durationRepercussion = (1 + (Convert.ToSingle((int)GameConfigurationControl.actualGameConfiguration.ProjectDifficulty) / 10f)) * DurationImpacts[i];
            }
            DurationBar.AddValue(durationRepercussion);
            if (durationRepercussion > 0)
            {
                feedback.StartPositiveDurationQuestion();
                secondsActive += 1.15f;
            }
            else if (durationRepercussion < 0)
            {
                feedback.StartNegativeDurationQuestion();
                secondsActive += 1.15f;
            }

            Debug.Log($"Difficulty = {GameConfigurationControl.actualGameConfiguration.ProjectDifficulty.ToString()}\n" +
                $"Progress --> Before({ProgressImpacts[i]}) --- After({progressRepercussion})\n" +
                $"Budget --> Before({BudgetImpacts[i]}) --- After({budgetRepercussion})\n" +
                $"Duration --> Before({DurationImpacts[i]}) --- After({durationRepercussion})\n");

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

                selectedAnswer.GetComponent<Image>().color = correctColor;

                sound.clip = correctAudio;
                sound.Play();
            }
            else
            {
                GameHandle.negativeFailureStreak++;

                GameHandle.UpdateWorkOfWorkerPerDay(RecommendConfiguration.GetProdWorkerDayFailure());

                selectedAnswer.GetComponent<Image>().color = errorColor;

                sound.clip = errorAudio;
                sound.Play();
            }

            for (int j = 0; j < CorrectAnswer.Length; j++)
            {
                if (CorrectAnswer[j])
                {
                    contentQuestion.GetChild(j + 1).GetComponent<Image>().sprite = contentQuestion.GetChild(j + 1).GetComponent<Button>().spriteState.highlightedSprite;
                    contentQuestion.GetChild(j + 1).GetComponent<Image>().color = correctColor;
                }
            }

            yield return new WaitForSeconds(secondsActive);

            GameHandle.NumEventsActive--;

            transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<Image>().sprite = sprite_idle[0];
            transform.GetChild(0).GetChild(0).GetChild(2).GetComponent<Image>().sprite = sprite_idle[1];
            transform.GetChild(0).GetChild(0).GetChild(3).GetComponent<Image>().sprite = sprite_idle[2];

            transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<Image>().color = idleColor;
            transform.GetChild(0).GetChild(0).GetChild(2).GetComponent<Image>().color = idleColor;
            transform.GetChild(0).GetChild(0).GetChild(3).GetComponent<Image>().color = idleColor;

            ClickEvent.DeleteEvent();
        }
        else
            yield return new WaitForSeconds(0.001f);
    }
}
