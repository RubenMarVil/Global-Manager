using UnityEngine;
using System;
using Assets.Scripts.Control;

using Panda;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class GameHandle : MonoBehaviour
{
    public GameObject[] typesEventsNegative;
    public GameObject[] typesEventsPositive;

    private int TimeForNextEvent;
    public static int TimePerEvent;
    private double TimeForNextDay;
    public double TimePerDay;

    public static float dropVelocity = 1.5f;

    private float salaryPerDay;
    private int workers;

    public static int negativeCommunicationEvents;
    public static int negativeCoordinationEvents;
    public static int negativeControlEvents;

    public static int correctNegativeCommunicationEvents;
    public static int correctNegativeCoordinationEvents;
    public static int correctNegativeControlEvents;

    public static int negativeFailureStreak;
    public static int positiveEventStreak;

    public static float NegativeWeight;
    public static float[] NegativeEventsWeights;
    public static float[] PositiveEventsWeights;

    private float RandomNegativeNumber;
    private float RandomPositiveNumber;

    public static int NumEventsActive;

    [Task]
    bool IsPlayerBasic;

    [Task]
    bool IsPlayerIntermediate;

    [Task]
    bool IsPlayerAdvanced;

    void Start()
    {
        NegativeWeight = 1.0f;
        NegativeEventsWeights = new[] { 0.33f, 0.33f, 0.33f };
        PositiveEventsWeights = new[] { 0.33f, 0.33f, 0.33f };

        negativeCommunicationEvents = 0;
        negativeCoordinationEvents = 0;
        negativeControlEvents = 0;

        correctNegativeCommunicationEvents = 0;
        correctNegativeCoordinationEvents = 0;
        correctNegativeControlEvents = 0;

        negativeFailureStreak = 0;
        positiveEventStreak = 0;

        NumEventsActive = 0;

        IsPlayerBasic = false;
        IsPlayerIntermediate = false;
        IsPlayerAdvanced = false;

        switch(UserControl.actualUser.UserLevel)
        {
            case UserLevels.BASIC: 
                TimePerEvent = 20;
                IsPlayerBasic = true; 
                break;
            case UserLevels.INTERMEDIATE: 
                TimePerEvent = 15;
                IsPlayerIntermediate = true; 
                break;
            case UserLevels.ADVANCED: 
                TimePerEvent = 10;
                IsPlayerAdvanced = true; 
                break;
        }
        TimeForNextEvent = 5 + Convert.ToInt32(Time.time);
        TimeForNextDay = TimePerDay + Convert.ToInt32(Time.time);

        salaryPerDay = CalculateSalaryPerDay() * 8;
        workers = CalculateNumWorkers();

        GameObject.Find("/InfoUI/UI/Username").GetComponent<Text>().text = UserControl.actualUser.Name;
        GameObject.Find("/InfoUI/UI/InitialCharacteristics/ProjectDifficulty/Value").GetComponent<Text>().text = GameConfigurationControl.actualGameConfiguration.ProjectDifficulty.ToString();
        GameObject.Find("/InfoUI/UI/InitialCharacteristics/ProjectBudget/Value").GetComponent<Text>().text = Math.Truncate(GameConfigurationControl.actualGameConfiguration.InitialBudget).ToString() + "$";
        GameObject.Find("/InfoUI/UI/InitialCharacteristics/ProjectDuration/Value").GetComponent<Text>().text = (Math.Truncate(100 * GameConfigurationControl.actualGameConfiguration.InitialDuration) / 100).ToString() + " Years";
    }

    void Update()
    {
        if(Time.time >= TimeForNextDay)
        {
            DurationBar.AddValueConst(-1);
            BudgetBar.AddValueConst(-salaryPerDay);
            ProgressBar.AddValueConst(workers * 0.0075f);

            TimeForNextDay += TimePerDay;
        }
    }

    [Task]
    void CreatePositiveEvent()
    {
        GeneratePositiveEvent(-1);
        Task.current.Succeed();
    }

    [Task]
    void CreateNegativeEvent()
    {
        GenerateNegativeEvent(-1);
        Task.current.Succeed();
    }

    [Task]
    void NegativeEvent()
    {
        if(UnityEngine.Random.Range(0.0f, 1.0f) <= NegativeWeight)
        {
            Task.current.Succeed();
        }
        else
        {
            Task.current.Fail();
        }
    }

    [Task]
    void CommunicationNegativeEvent()
    {
        RandomNegativeNumber = UnityEngine.Random.Range(0.0f, 1.0f);

        if (RandomNegativeNumber <= NegativeEventsWeights[0])
        {
            Task.current.Succeed();
        }
        else
        {
            Task.current.Fail();
        }
    }

    [Task]
    void CoordinationNegativeEvent()
    {
        if (RandomNegativeNumber <= NegativeEventsWeights[1])
        {
            Task.current.Succeed();
        }
        else
        {
            Task.current.Fail();
        }
    }

    [Task]
    void ControlNegativeEvent()
    {
        if (RandomNegativeNumber <= NegativeEventsWeights[2])
        {
            Task.current.Succeed();
        }
        else
        {
            Task.current.Fail();
        }
    }

    [Task]
    void CommunicationPositiveEvent()
    {
        RandomPositiveNumber = UnityEngine.Random.Range(0.0f, 1.0f);

        if (RandomPositiveNumber <= PositiveEventsWeights[0])
        {
            Task.current.Succeed();
        }
        else
        {
            Task.current.Fail();
        }
    }

    [Task]
    void CoordinationPositiveEvent()
    {
        if (RandomPositiveNumber <= PositiveEventsWeights[1])
        {
            Task.current.Succeed();
        }
        else
        {
            Task.current.Fail();
        }
    }

    [Task]
    void ControlPositiveEvent()
    {
        if (RandomPositiveNumber <= PositiveEventsWeights[2])
        {
            Task.current.Succeed();
        }
        else
        {
            Task.current.Fail();
        }
    }

    [Task]
    void CreateNegativeCommunicationEvent()
    {
        GenerateNegativeEvent(0);
        Task.current.Succeed();

        float changeNumber = UnityEngine.Random.Range(0.0f, negativeCommunicationEvents - correctNegativeCommunicationEvents) / 10;
        if(changeNumber + NegativeEventsWeights[0] > 1.0f)
        {
            changeNumber = 1.0f - NegativeEventsWeights[0];
        }

        NegativeEventsWeights[0] += changeNumber;
        NegativeEventsWeights[1] -= changeNumber / 2;
        NegativeEventsWeights[2] -= changeNumber / 2;

        Debug.Log("CommunicationWeight: " + NegativeEventsWeights[0] + "\nCoordinationWeight: " + NegativeEventsWeights[1] + "\nControlWeight: " + NegativeEventsWeights[2]);
    }

    [Task]
    void CreateNegativeCoordinationEvent()
    {
        GenerateNegativeEvent(1);
        Task.current.Succeed();

        float changeNumber = UnityEngine.Random.Range(0.0f, negativeCoordinationEvents - correctNegativeCoordinationEvents) / 10;
        if (changeNumber + NegativeEventsWeights[1] > 1.0f)
        {
            changeNumber = 1.0f - NegativeEventsWeights[1];
        }

        NegativeEventsWeights[1] += changeNumber;
        NegativeEventsWeights[0] -= changeNumber / 2;
        NegativeEventsWeights[2] -= changeNumber / 2;

        Debug.Log("CommunicationWeight: " + NegativeEventsWeights[0] + "\nCoordinationWeight: " + NegativeEventsWeights[1] + "\nControlWeight: " + NegativeEventsWeights[2]);
    }

    [Task]
    void CreateNegativeControlEvent()
    {
        GenerateNegativeEvent(2);
        Task.current.Succeed();

        float changeNumber = UnityEngine.Random.Range(0.0f, negativeControlEvents - correctNegativeControlEvents) / 10;
        if (changeNumber + NegativeEventsWeights[2] > 1.0f)
        {
            changeNumber = 1.0f - NegativeEventsWeights[2];
        }

        NegativeEventsWeights[2] += changeNumber;
        NegativeEventsWeights[0] -= changeNumber / 2;
        NegativeEventsWeights[1] -= changeNumber / 2;

        Debug.Log("CommunicationWeight: " + NegativeEventsWeights[0] + "\nCoordinationWeight: " + NegativeEventsWeights[1] + "\nControlWeight: " + NegativeEventsWeights[2]);
    }

    [Task]
    void CreatePositiveCommunicationEvent()
    {
        GeneratePositiveEvent(0);
        Task.current.Succeed();
    }

    [Task]
    void CreatePositiveCoordinationEvent()
    {
        GeneratePositiveEvent(1);
        Task.current.Succeed();
    }

    [Task]
    void CreatePositiveControlEvent()
    {
        GeneratePositiveEvent(2);
        Task.current.Succeed();
    }

    [Task]
    void IsCommunicationLowest()
    {
        if(Math.Min(Math.Min(negativeCommunicationEvents, negativeCoordinationEvents), negativeControlEvents) == negativeCommunicationEvents)
        {
            Task.current.Succeed();
        }
        else
        {
            Task.current.Fail();
        }
    }

    [Task]
    void IsCoordinationLowest()
    {
        if (Math.Min(Math.Min(negativeCommunicationEvents, negativeCoordinationEvents), negativeControlEvents) == negativeCoordinationEvents)
        {
            Task.current.Succeed();
        }
        else
        {
            Task.current.Fail();
        }
    }

    [Task]
    void IsControlLowest()
    {
        if (Math.Min(Math.Min(negativeCommunicationEvents, negativeCoordinationEvents), negativeControlEvents) == negativeControlEvents)
        {
            Task.current.Succeed();
        }
        else
        {
            Task.current.Fail();
        }
    }

    [Task]
    void CheckStress()
    {
        if(StressBar.GetPercentageValue() >= 80.0f)
        {
            Task.current.Succeed();
        }
        else
        {
            Task.current.Fail();
        }
    }

    [Task]
    void CheckBudget()
    {
        if(BudgetBar.GetPercentageValue() <= 15.0f)
        {
            Task.current.Succeed();
        }
        else
        {
            Task.current.Fail();
        }
    }

    [Task]
    void CheckDuration()
    {
        if (DurationBar.GetPercentageValue() <= 20.0f)
        {
            Task.current.Succeed();
        }
        else
        {
            Task.current.Fail();
        }
    }

    [Task]
    void CanDropEvent()
    {
        if (Time.time >= TimeForNextEvent)
        {
            Debug.Log("Evento!!");
            Task.current.Succeed();

            TimeForNextEvent += TimePerEvent;
        }
        else
        {
            Task.current.Fail();
        }
    }

    private void GenerateNegativeEvent(int type)
    {
        NumEventsActive++;

        positiveEventStreak = 0;
        if (type == -1)
        {
            System.Random rng = new System.Random();
            type = rng.Next(0, typesEventsNegative.Length - 1);
        }

        Instantiate(typesEventsNegative[type]);

        switch(type)
        {
            case 0: negativeCommunicationEvents++; break;
            case 1: negativeCoordinationEvents++; break;
            case 2: negativeControlEvents++; break;
        }

        StressBar.AddValue(NumEventsActive * 0.75f);
    }

    private void GeneratePositiveEvent(int type)
    {
        positiveEventStreak++;

        if (type == -1)
        {
            System.Random rng = new System.Random();
            type = rng.Next(0, typesEventsPositive.Length - 1);
        }

        Instantiate(typesEventsPositive[type]);
    }

    private float CalculateSalaryPerDay()
    {
        float salary = 0;
        DBCountry dbCountry = new DBCountry();

        foreach(SiteConfiguration site in GameConfigurationControl.actualGameConfiguration.SitesList)
        {
            Country country = GameConfigurationControl.GetAllDataOfCountry(site.Country);

            salary += site.TeamSize * country.Salary;
        }

        return salary;
    }

    private int CalculateNumWorkers()
    {
        int numWorkers = 0;

        foreach(SiteConfiguration site in GameConfigurationControl.actualGameConfiguration.SitesList)
        {
            numWorkers += site.TeamSize;
        }

        return numWorkers;
    }

    public static void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ReturnMainMenu()
    {
        int totalNegativeEvents = negativeCommunicationEvents + negativeCoordinationEvents + negativeControlEvents;
        int correctNegativeEvents = correctNegativeCommunicationEvents + correctNegativeCoordinationEvents + correctNegativeControlEvents;

        float durationValue = DurationBar.GetValue() / 365.0f;

        bool inserted = GameConfigurationControl.InsertResultGame(StressBar.GetValue(), ProgressBar.GetValue(), BudgetBar.GetValue(), durationValue, totalNegativeEvents, correctNegativeEvents);

        UpdateLevelPlayer();

        if (inserted)
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(0, LoadSceneMode.Single);
        }
    }

    private void UpdateLevelPlayer()
    {
        List<GameConfiguration> listGames = GameConfigurationControl.GetAllGamesOfPlayer(UserControl.actualUser.Name);

        int performance = CalculatePerformance(listGames);
        listGames.RemoveAt(listGames.Count - 1);
        int resilience = CalculateResilience(listGames);

        Debug.Log("Performance = " + performance + "\nResilience = " + resilience);
        Debug.Log("Score = " + UserControl.actualUser.Score + "\nUserLevel = " + UserControl.actualUser.UserLevel);

        int changeScore = UserControl.actualUser.UpdateScoreLevel(performance, resilience);

        if((changeScore > 0 && ProgressBar.GetValue() >= 90.0f) || (changeScore < 0 && ProgressBar.GetValue() < 90.0f))
        {
            UserControl.UpdateScoreUserLevel();
        }
        

        Debug.Log("Score = " + UserControl.actualUser.Score + "\nUserLevel = " + UserControl.actualUser.UserLevel);
    }

    private int CalculateResilience(List<GameConfiguration> listGames)
    {
        int resilience = 1;
        float[] averageDeviationCorrectNegativeEvents = { 0, 0 };

        if(listGames.Any())
        {
            averageDeviationCorrectNegativeEvents = CalculateAverageDeviationPastEvents(listGames);
        }

        int totalNegativeEvents = negativeCommunicationEvents + negativeCoordinationEvents + negativeControlEvents;
        int correctNegativeEvents = correctNegativeCommunicationEvents + correctNegativeCoordinationEvents + correctNegativeControlEvents;

        float averageActualNegativeEvents = Convert.ToSingle(correctNegativeEvents) / Convert.ToSingle(totalNegativeEvents);

        if(averageActualNegativeEvents > averageDeviationCorrectNegativeEvents[0] + averageDeviationCorrectNegativeEvents[1])
        {
            resilience = 2;
        }
        else if(averageActualNegativeEvents < averageDeviationCorrectNegativeEvents[0] - averageDeviationCorrectNegativeEvents[1])
        {
            resilience = 0;
        }
        else
        {
            resilience = 1;
        }

        return resilience;
    }

    private float[] CalculateAverageDeviationPastEvents(List<GameConfiguration> listGames)
    {
        float[] averageDeviation = new float[2];
        float[] percentageCorrectEvents = new float[listGames.Count];

        for(int i = 0; i < listGames.Count; i++)
        {
            percentageCorrectEvents[i] = Convert.ToSingle(listGames[i].CorrectNegativeEvents) / Convert.ToSingle(listGames[i].TotalNegativeEvents);
        }

        averageDeviation[0] = percentageCorrectEvents.Average();

        averageDeviation[1] = 0;

        if (percentageCorrectEvents.Any())
        {       
            double sum = percentageCorrectEvents.Sum(d => Math.Pow(d - averageDeviation[0], 2));

            averageDeviation[1] = Convert.ToSingle(Math.Sqrt((sum) / (percentageCorrectEvents.Count() - 1)));
        }

        if(averageDeviation[1] > averageDeviation[0])
        {
            int maxValue = Array.FindIndex(percentageCorrectEvents, val => val.Equals(percentageCorrectEvents.Max()));
            int minValue = Array.FindIndex(percentageCorrectEvents, val => val.Equals(percentageCorrectEvents.Min()));

            listGames.RemoveAt(maxValue);
            listGames.RemoveAt(minValue);

            averageDeviation = CalculateAverageDeviationPastEvents(listGames);
        }

        return averageDeviation;
    }

    private int CalculatePerformance(List<GameConfiguration> listGames)
    {
        int performance = 1;
        int[] range = { listGames.Count-10, 10 };

        if(listGames.Count < 10)
        {
            range[0] = 0;
            range[1] = listGames.Count;
        }
        float[] listSuccessProjects = CalculateSuccessProjects(listGames.GetRange(range[0], range[1]));

        switch(UserControl.actualUser.UserLevel)
        {
            case UserLevels.BASIC:
                if(listSuccessProjects[0] < 1.0f || listSuccessProjects[1] < 0.5f)
                {
                    performance = 0;
                }
                else if((listSuccessProjects[0] == 1.0f && listSuccessProjects[1] >= 0.5f && listSuccessProjects[1] < 1.0f) || listSuccessProjects[2] < 0.5f)
                {
                    performance = 1;
                }
                else if((listSuccessProjects[1] == 1.0f && listSuccessProjects[2] >= 0.5f && listSuccessProjects[2] < 1.0f) || listSuccessProjects[3] < 0.5f)
                {
                    performance = 2;
                }
                else
                {
                    performance = 2;
                }
                break;
            case UserLevels.INTERMEDIATE:
                if ((listSuccessProjects[0] < 1.0f || listSuccessProjects[1] < 1.0f) || listSuccessProjects[2] < 0.5f)
                {
                    performance = 0;
                }
                else if ((listSuccessProjects[1] == 1.0f && listSuccessProjects[2] >= 0.5f && listSuccessProjects[2] < 1.0f) || listSuccessProjects[3] < 0.5f)
                {
                    performance = 1;
                }
                else if ((listSuccessProjects[2] == 1.0f && listSuccessProjects[3] >= 0.5f && listSuccessProjects[3] < 1.0f) || listSuccessProjects[4] < 0.5f)
                {
                    performance = 2;
                }
                else
                {
                    performance = 2;
                }
                break;
            case UserLevels.ADVANCED:
                if ((listSuccessProjects[0] < 1.0f || listSuccessProjects[1] < 1.0f || listSuccessProjects[2] < 1.0f) || listSuccessProjects[3] < 0.5f)
                {
                    performance = 0;
                }
                else if ((listSuccessProjects[2] == 1.0f && listSuccessProjects[3] >= 0.5f && listSuccessProjects[3] < 1.0f) || listSuccessProjects[4] < 0.5f)
                {
                    performance = 1;
                }
                else if (listSuccessProjects[3] == 1.0f && listSuccessProjects[4] >= 0.5f && listSuccessProjects[4] < 1.0f)
                {
                    performance = 2;
                }
                else
                {
                    performance = 2;
                }
                break;
        }

        return performance;
    }

    private float[] CalculateSuccessProjects(List<GameConfiguration> listGames)
    {
        float[] listPercentageSuccessProjects = new float[5];
        int[] listTotalProjects = new int[5];
        int[] listSuccessProjects = new int[5];

        foreach(GameConfiguration game in listGames)
        {
            int level = 0;
            switch(game.ProjectDifficulty)
            {
                case ProjectDifficultyLevels.VERY_LOW: level = 0; break;
                case ProjectDifficultyLevels.LOW: level = 1; break;
                case ProjectDifficultyLevels.MEDIUM: level = 2; break;
                case ProjectDifficultyLevels.HIGH: level = 3; break;
                case ProjectDifficultyLevels.VERY_HIGH: level = 4; break;
            }

            listTotalProjects[level]++;
            if(game.ProgressValue == 100.0f)
            {
                listSuccessProjects[level]++;
            }
        }

        for(int i = 0; i < listTotalProjects.Length; i++)
        {
            listPercentageSuccessProjects[i] = Convert.ToSingle(listSuccessProjects[i]) / Convert.ToSingle(listTotalProjects[i]);
            if(listPercentageSuccessProjects[i] != listPercentageSuccessProjects[i])
            {
                listPercentageSuccessProjects[i] = 1.0f;
            }
        }

        return listPercentageSuccessProjects;
    }
}
