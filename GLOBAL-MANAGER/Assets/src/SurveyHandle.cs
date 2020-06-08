using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;
using Assets.src.domain;
using NRules.Fluent;
using System.Reflection;
using NRules;

public class SurveyHandle : MonoBehaviour
{
    private GameObject ErrorMessage;

    private InputField userName;
    private Slider age;

    private ToggleGroup culturalKnowledgeGroup;
    private ToggleGroup languageKnowledgeGroup;
    private ToggleGroup timeKnowledgeGroup;

    private Toggle culturalKnowledge;
    private Toggle languageKnowledge;
    private Toggle timeKnowledge;

    private bool culturalKnowledgeAny;
    private bool languageKnowledgeAny;
    private bool timeKnowledgeAny;

    void Start()
    {
        ErrorMessage = GameObject.Find("ScrollView/Viewport/Content/ErrorMessage");

        userName = GameObject.Find("ScrollView/Viewport/Content/Q1/Answer").GetComponent<InputField>();
        age = GameObject.Find("ScrollView/Viewport/Content/Q2/SliderAge").GetComponent<Slider>();

        culturalKnowledgeGroup = GameObject.Find("ScrollView/Viewport/Content/Q5/RadioButtonGroup").GetComponent<ToggleGroup>();
        languageKnowledgeGroup = GameObject.Find("ScrollView/Viewport/Content/Q6/RadioButtonGroup").GetComponent<ToggleGroup>();
        timeKnowledgeGroup = GameObject.Find("ScrollView/Viewport/Content/Q7/RadioButtonGroup").GetComponent<ToggleGroup>();

        culturalKnowledgeAny = false;
        languageKnowledgeAny = false;
        timeKnowledgeAny = false;
}

    void Update()
    {
        try
        {
            culturalKnowledge = culturalKnowledgeGroup.ActiveToggles().ElementAt<Toggle>(0);
            culturalKnowledgeAny = true;
        } 
        catch (ArgumentOutOfRangeException)
        {
            culturalKnowledgeAny = false;
        }

        try
        {
            languageKnowledge = languageKnowledgeGroup.ActiveToggles().ElementAt<Toggle>(0);
            languageKnowledgeAny = true;
        }
        catch (ArgumentOutOfRangeException)
        {
            languageKnowledgeAny = false;
        }

        try
        {
            timeKnowledge = timeKnowledgeGroup.ActiveToggles().ElementAt<Toggle>(0);
            timeKnowledgeAny = true;
        }
        catch (ArgumentOutOfRangeException)
        {
            timeKnowledgeAny = false;
        }
    }

    public void GetUserLevel()
    {
        if(!culturalKnowledgeAny || !languageKnowledgeAny || !timeKnowledgeAny || String.IsNullOrWhiteSpace(userName.text))
        {
            ErrorMessage.SetActive(true);
        }
        else
        {
            var user = new User(userName.text, (int)age.value);

            var repository = new RuleRepository();
            repository.Load(x => x.From(Assembly.GetExecutingAssembly()));

            var factory = repository.Compile();

            var session = factory.CreateSession();

            if (culturalKnowledge.name.Equals("Yes"))
            {
                user.CulturalKnowledge = KnowledgeLevels.HIGH;
                Debug.Log("Cultural Difference Knowledge: HIGH");
            }
            else
            {
                user.CulturalKnowledge = KnowledgeLevels.LOW;
                Debug.Log("Cultural Difference Knowledge: LOW");
            }

            if (languageKnowledge.name.Equals("Yes"))
            {
                user.LanguageKnowledge = KnowledgeLevels.HIGH;
                Debug.Log("Language Difference Knowledge: HIGH");
            }
            else
            {
                user.LanguageKnowledge = KnowledgeLevels.LOW;
                Debug.Log("Language Difference Knowledge: LOW");
            }

            if (timeKnowledge.name.Equals("Yes"))
            {
                user.timeKnowledge = KnowledgeLevels.HIGH;
                Debug.Log("Time Difference Knowledge: HIGH");
            }
            else
            {
                user.timeKnowledge = KnowledgeLevels.LOW;
                Debug.Log("Time Difference Knowledge: LOW");
            }

            session.Insert(user);

            session.Update(user);
            session.Fire();

            Debug.Log($"User level: {user.UserLevel}");
        }
    }
}
