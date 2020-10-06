using Assets.Scripts.Control;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectPlayerHandle : MonoBehaviour
{
    private List<User> userList;

    public GameObject UserBtnPrefab;

    private Animator animScrollView;

    void Start()
    {
        animScrollView = GameObject.FindGameObjectWithTag("Menu").GetComponent<Animator>();

        userList = UserControl.GetAllPlayers();

        if(userList.Count != 0)
        {
            transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);

            for (int i = 0; i < userList.Count; i++)
            {
                GameObject newUserBtn = Instantiate(UserBtnPrefab, transform.GetChild(0).transform.GetChild(0));

                newUserBtn.GetComponent<Button>().onClick.AddListener(BackMainMenu);
                newUserBtn.GetComponent<PlayerBtnHandle>().user = userList[i];

                newUserBtn.name = userList[i].Name;
                newUserBtn.transform.GetChild(0).GetComponent<Text>().text = userList[i].Name;
            }
        } 
        else
        {
            transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    void Update()
    {
        
    }

    public void BackMainMenu()
    {
        animScrollView.SetBool("SelectPlayer", false);
    }
}
