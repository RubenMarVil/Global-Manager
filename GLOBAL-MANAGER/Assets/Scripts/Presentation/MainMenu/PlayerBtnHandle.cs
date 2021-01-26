using Assets.Scripts.Control;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBtnHandle : MonoBehaviour
{
    public User user;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(SetInfoPlayer);
    }

    void Update()
    {
        
    }

    public void SetInfoPlayer()
    {
        Text infoPlayer = GameObject.FindGameObjectWithTag("Info").GetComponent<Text>();
        string userLevel;
        string sex;

        if(user.Score >= 4 && user.Score <= 6) { userLevel = "INTERMEDIATE-"; }
        else if(user.Score >= 13 && user.Score <= 15) { userLevel = "INTERMEDIATE+"; }
        else { userLevel = user.UserLevel.ToString(); }

        if(user.IsMan)
        {
            sex = "MAN";
        }
        else
        {
            sex = "WOMAN";
        }

        infoPlayer.text = user.Name + 
            "\nAge: " + user.Age +
            "\n" + sex +
            "\n" + userLevel +
            "\nScore: " + user.Score + 
            "\nProjects: " + user.NumProjects;

        UserControl.actualUser = user;

        GameObject.FindGameObjectWithTag("StartGameBtn").GetComponent<Button>().interactable = true;

        Debug.Log($"[PlayerBtn - INFO] User selected --> {user.Name}");
    }
}
