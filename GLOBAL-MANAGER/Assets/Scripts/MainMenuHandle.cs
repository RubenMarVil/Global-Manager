using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuHandle : MonoBehaviour
{
    private Animator animScrollView;

    void Start()
    {
        animScrollView = GameObject.FindGameObjectWithTag("Menu").GetComponent<Animator>();

        Time.timeScale = 1;
    }

    void Update()
    {

    }

    public void StartProject()
    {
        SceneManager.LoadScene(2, LoadSceneMode.Single);
    }

    public void SelectPlayerBtn()
    {
        animScrollView.SetBool("SelectPlayer", true);
    }

    public void BackMainMenu()
    {
        animScrollView.SetBool("SelectPlayer", false);
    }

    public void NewPlayer()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
