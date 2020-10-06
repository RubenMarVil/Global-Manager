using Lean.Transition.Method;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuHandle : MonoBehaviour
{
    private Animator animScrollView;

    public GameObject StartGameBtn;

    public GameObject PointerEnterTransition;
    public GameObject PointerExitTransition;

    void Start()
    {
        animScrollView = GameObject.FindGameObjectWithTag("Menu").GetComponent<Animator>();

        Time.timeScale = 1;
    }

    void Update()
    {

    }

    public void StartProjectEnter()
    {
        if(StartGameBtn.GetComponent<Button>().interactable)
        {
            PointerEnterTransition.GetComponent<LeanTransformLocalScaleXY>().BeginThisTransition();
        }
    }

    public void StartProjectExit()
    {
        if(StartGameBtn.GetComponent<Button>().interactable)
        {
            PointerExitTransition.GetComponent<LeanTransformLocalScaleXY>().BeginThisTransition();
        }
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
