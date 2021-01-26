using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;
using Assets.Scripts.Control;
using UnityEngine.SceneManagement;
using Lean.Gui;
using System.Collections;

public class SurveyHandle : MonoBehaviour
{
    private InputField userName;
    private Slider age;

    private ToggleGroup colocalized_globalKnowledgeGroup;
    private ToggleGroup culturalKnowledgeGroup;
    private ToggleGroup languageKnowledgeGroup;
    private ToggleGroup timeKnowledgeGroup;
    private ToggleGroup siteAnswerGroup;
    private ToggleGroup followTheSunAnswerGroup;
    private ToggleGroup offshoringAnswerGroup;
    private ToggleGroup outsourcingAnswerGroup;
    private ToggleGroup sexGroup;

    private Toggle colocalized_globalKnowledge;
    private Toggle culturalKnowledge;
    private Toggle languageKnowledge;
    private Toggle timeKnowledge;
    private Toggle siteAnswer;
    private Toggle followTheSunAnswer;
    private Toggle offshoringAnswer;
    private Toggle outsourcingAnswer;
    private Toggle sex;

    private bool colocalized_globalKnowledgeAny;
    private bool culturalKnowledgeAny;
    private bool languageKnowledgeAny;
    private bool timeKnowledgeAny;
    private bool siteAnswerAny;
    private bool followTheSunAnswerAny;
    private bool offshoringAnswerAny;
    private bool outsourcingAnswerAny;
    private bool sexAnswerAny;

    public GameObject panelResult;
    public GameObject usernameText;
    public GameObject ageText;
    public GameObject scoreText;
    public GameObject userLevelText;

    private GameObject ErrorModal;
    private String ErrorMessage;

    private GameObject PlayerModal;

    public Animator transitionAnim;

    void Start()
    {
        ErrorModal = GameObject.Find("/Canvas/ModalError/");
        PlayerModal = GameObject.Find("/Canvas/ModalPlayerCreated/");

        userName = GameObject.Find("ScrollView/Viewport/Content/Q1/Answer").GetComponent<InputField>();
        age = GameObject.Find("ScrollView/Viewport/Content/Q2/SliderAge").GetComponent<Slider>();

        colocalized_globalKnowledgeGroup = GameObject.Find("ScrollView/Viewport/Content/Q4/RadioButtonGroup").GetComponent<ToggleGroup>();
        culturalKnowledgeGroup = GameObject.Find("ScrollView/Viewport/Content/Q5/RadioButtonGroup").GetComponent<ToggleGroup>();
        languageKnowledgeGroup = GameObject.Find("ScrollView/Viewport/Content/Q6/RadioButtonGroup").GetComponent<ToggleGroup>();
        timeKnowledgeGroup = GameObject.Find("ScrollView/Viewport/Content/Q7/RadioButtonGroup").GetComponent<ToggleGroup>();
        siteAnswerGroup = GameObject.Find("ScrollView/Viewport/Content/Q8/RadioButtonGroup").GetComponent<ToggleGroup>();
        followTheSunAnswerGroup = GameObject.Find("ScrollView/Viewport/Content/Q9/RadioButtonGroup").GetComponent<ToggleGroup>();
        offshoringAnswerGroup = GameObject.Find("ScrollView/Viewport/Content/Q10/RadioButtonGroup").GetComponent<ToggleGroup>();
        outsourcingAnswerGroup = GameObject.Find("ScrollView/Viewport/Content/Q11/RadioButtonGroup").GetComponent<ToggleGroup>();
        sexGroup = GameObject.Find("ScrollView/Viewport/Content/Q1/Sex").GetComponent<ToggleGroup>();

        colocalized_globalKnowledgeAny = false;
        culturalKnowledgeAny = false;
        languageKnowledgeAny = false;
        timeKnowledgeAny = false;
        siteAnswerAny = false;
        followTheSunAnswerAny = false;
        offshoringAnswerAny = false;
        outsourcingAnswerAny = false;
        sexAnswerAny = false;
}

    void Update()
    {
        bool intro = false;
        ErrorMessage = "";

        if(String.IsNullOrWhiteSpace(userName.text))
        {
            if (intro)
            {
                ErrorMessage += "-Question 1\n";
            }
            else
            {
                ErrorMessage += "-Question 1\t\t\t";
            }
            intro = !intro;
        }

        try
        {
            sex = sexGroup.ActiveToggles().ElementAt<Toggle>(0);
            sexAnswerAny = true;
        }
        catch (ArgumentOutOfRangeException)
        {
            sexAnswerAny = false;
        }

        try
        {
            colocalized_globalKnowledge = colocalized_globalKnowledgeGroup.ActiveToggles().ElementAt<Toggle>(0);
            colocalized_globalKnowledgeAny = true;
        }
        catch (ArgumentOutOfRangeException)
        {
            if (intro)
            {
                ErrorMessage += "-Question 4\n";
            }
            else
            {
                ErrorMessage += "-Question 4\t\t\t";
            }
            intro = !intro;
            colocalized_globalKnowledgeAny = false;
        }

        try
        {
            culturalKnowledge = culturalKnowledgeGroup.ActiveToggles().ElementAt<Toggle>(0);
            culturalKnowledgeAny = true;
        } 
        catch (ArgumentOutOfRangeException)
        {
            if (intro)
            {
                ErrorMessage += "-Question 5\n";
            }
            else
            {
                ErrorMessage += "-Question 5\t\t\t";
            }
            intro = !intro;
            culturalKnowledgeAny = false;
        }

        try
        {
            languageKnowledge = languageKnowledgeGroup.ActiveToggles().ElementAt<Toggle>(0);
            languageKnowledgeAny = true;
        }
        catch (ArgumentOutOfRangeException)
        {
            if (intro)
            {
                ErrorMessage += "-Question 6\n";
            }
            else
            {
                ErrorMessage += "-Question 6\t\t\t";
            }
            intro = !intro;
            languageKnowledgeAny = false;
        }

        try
        {
            timeKnowledge = timeKnowledgeGroup.ActiveToggles().ElementAt<Toggle>(0);
            timeKnowledgeAny = true;
        }
        catch (ArgumentOutOfRangeException)
        {
            if (intro)
            {
                ErrorMessage += "-Question 7\n";
            }
            else
            {
                ErrorMessage += "-Question 7\t\t\t";
            }
            intro = !intro;
            timeKnowledgeAny = false;
        }

        try
        {
            siteAnswer = siteAnswerGroup.ActiveToggles().ElementAt<Toggle>(0);
            siteAnswerAny = true;
        }
        catch (ArgumentOutOfRangeException)
        {
            if (intro)
            {
                ErrorMessage += "-Question 8\n";
            }
            else
            {
                ErrorMessage += "-Question 8\t\t\t";
            }
            intro = !intro;
            siteAnswerAny = false;
        }

        try
        {
            followTheSunAnswer = followTheSunAnswerGroup.ActiveToggles().ElementAt<Toggle>(0);
            followTheSunAnswerAny = true;
        }
        catch (ArgumentOutOfRangeException)
        {
            if (intro)
            {
                ErrorMessage += "-Question 9\n";
            }
            else
            {
                ErrorMessage += "-Question 9\t\t\t";
            }
            intro = !intro;
            followTheSunAnswerAny = false;
        }

        try
        {
            offshoringAnswer = offshoringAnswerGroup.ActiveToggles().ElementAt<Toggle>(0);
            offshoringAnswerAny = true;
        }
        catch (ArgumentOutOfRangeException)
        {
            if (intro)
            {
                ErrorMessage += "-Question 10\n";
            }
            else
            {
                ErrorMessage += "-Question 10\t\t\t";
            }
            intro = !intro;
            offshoringAnswerAny = false;
        }

        try
        {
            outsourcingAnswer = outsourcingAnswerGroup.ActiveToggles().ElementAt<Toggle>(0);
            outsourcingAnswerAny = true;
        }
        catch (ArgumentOutOfRangeException)
        {
            if (intro)
            {
                ErrorMessage += "-Question 11\n";
            }
            else
            {
                ErrorMessage += "-Question 11\t\t\t";
            }
            intro = !intro;
            outsourcingAnswerAny = false;
        }
    }

    public void CreateUser()
    {
        if(String.IsNullOrWhiteSpace(userName.text) || !colocalized_globalKnowledgeAny || !languageKnowledgeAny || 
            !timeKnowledgeAny || !culturalKnowledgeAny || !siteAnswerAny || !followTheSunAnswerAny || !offshoringAnswerAny 
            || !outsourcingAnswerAny)
        {
            ErrorModal.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<Text>().text = ErrorMessage;
            ErrorModal.GetComponent<LeanWindow>().TurnOn();
        }
        else
        {
            User user = new User(userName.text, (int)age.value);
            if (colocalized_globalKnowledge.name.Equals("Yes"))
            {
                user.Colocalized_Global = KnowledgeLevels.HIGH;
                Debug.Log("Colocalized vs Global Knowledge: HIGH");
            }
            else
            {
                user.Colocalized_Global = KnowledgeLevels.LOW;
                Debug.Log("Colocalized vs Global Knowledge: LOW");
            }

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
                user.TimeKnowledge = KnowledgeLevels.HIGH;
                Debug.Log("Time Difference Knowledge: HIGH");
            }
            else
            {
                user.TimeKnowledge = KnowledgeLevels.LOW;
                Debug.Log("Time Difference Knowledge: LOW");
            }

            switch(siteAnswer.name)
            {
                case "Right":
                    user.SiteQuestion = AnswersToQuestions.RIGHT;
                    Debug.Log("Site Question: RIGHT");
                    break;
                case "AlmostRight":
                    user.SiteQuestion = AnswersToQuestions.ALMOST_RIGHT;
                    Debug.Log("Site Question: ALMOST RIGHT");
                    break;
                case "NotRight":
                    user.SiteQuestion = AnswersToQuestions.NOT_RIGHT;
                    Debug.Log("Site Question: NOT RIGHT");
                    break;
            }

            switch (followTheSunAnswer.name)
            {
                case "Right":
                    user.FollowTheSunQuestion = AnswersToQuestions.RIGHT;
                    Debug.Log("Follow the sun Question: RIGHT");
                    break;
                case "AlmostRight":
                    user.FollowTheSunQuestion = AnswersToQuestions.ALMOST_RIGHT;
                    Debug.Log("Follow the sun Question: ALMOST RIGHT");
                    break;
                case "NotRight":
                    user.FollowTheSunQuestion = AnswersToQuestions.NOT_RIGHT;
                    Debug.Log("Follow the sun Question: NOT RIGHT");
                    break;
            }

            switch (offshoringAnswer.name)
            {
                case "Right":
                    user.OffshoringQuestion = AnswersToQuestions.RIGHT;
                    Debug.Log("Offshoring Question: RIGHT");
                    break;
                case "AlmostRight":
                    user.OffshoringQuestion = AnswersToQuestions.ALMOST_RIGHT;
                    Debug.Log("Offshoring Question: ALMOST RIGHT");
                    break;
                case "NotRight":
                    user.OffshoringQuestion = AnswersToQuestions.NOT_RIGHT;
                    Debug.Log("Offshoring Question: NOT RIGHT");
                    break;
            }

            switch (outsourcingAnswer.name)
            {
                case "Right":
                    user.OutsourcingQuestion = AnswersToQuestions.RIGHT;
                    Debug.Log("Outsourcing Question: RIGHT");
                    break;
                case "AlmostRight":
                    user.OutsourcingQuestion = AnswersToQuestions.ALMOST_RIGHT;
                    Debug.Log("Outsourcing Question: ALMOST RIGHT");
                    break;
                case "NotRight":
                    user.OutsourcingQuestion = AnswersToQuestions.NOT_RIGHT;
                    Debug.Log("Outsourcing Question: NOT RIGHT");
                    break;
            }

            user.SetSex(sex.name);

            Debug.Log("[SurveyHandle - INFO] Creating new user...");
            user.CalculateScore();
            bool inserted = UserControl.CreateNewUser(user);

            ShowResult(inserted, user);
        }
    }

    public void NextScene()
    {
        StartCoroutine(LoadScene(0));
        //SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

    public void BackMainMenu()
    {
        StartCoroutine(LoadScene(0));
        //SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

    private void ShowResult(bool inserted, User newUser)
    {
        if(inserted)
        {
            string userLevel;

            if (newUser.Score >= 4 && newUser.Score <= 6) { userLevel = "INTERMEDIATE-"; }
            else if (newUser.Score >= 13 && newUser.Score <= 15) { userLevel = "INTERMEDIATE+"; }
            else { userLevel = newUser.UserLevel.ToString(); }

            GameObject content = PlayerModal.transform.GetChild(0).GetChild(1).gameObject;
            content.transform.GetChild(2).GetComponent<Text>().text = newUser.Name;
            content.transform.GetChild(3).GetComponent<Text>().text = newUser.Age.ToString();
            content.transform.GetChild(4).GetComponent<Text>().text = newUser.Score.ToString();
            content.transform.GetChild(5).GetComponent<Text>().text = userLevel;
            PlayerModal.GetComponent<LeanWindow>().TurnOn();

            Debug.Log($"[SurveyHandle - INFO] New user created --> {newUser.Name}");
        }
        else
        {
            ErrorMessage = "Unexpected error when adding the new user to the database.";
            ErrorModal.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<Text>().text = ErrorMessage;
            ErrorModal.GetComponent<LeanWindow>().TurnOn();

            Debug.Log($"[SurveyHandle - ERROR] Error to create user --> {newUser.Name}");
        }
    }

    IEnumerator LoadScene(int scene)
    {
        transitionAnim.SetTrigger("end");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }
}
