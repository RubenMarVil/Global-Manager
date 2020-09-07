using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickEvent : MonoBehaviour
{
    private static GameObject actualObject;
    private static Animator animPlayer;

    void Start()
    {
        animPlayer = GameObject.FindGameObjectWithTag("Players").GetComponent<Animator>();
    }

    void Update()
    {
        RaycastHit hit;
        Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);

        if (!animPlayer.GetBool("TelephoneEvent") && !animPlayer.GetBool("PositEvent") && !animPlayer.GetBool("LoupeEvent"))
        {
            if (Physics.Raycast(ray, out hit))
            {
                BoxCollider bc = hit.collider as BoxCollider;
                if (bc != null)
                {
                    if (actualObject != null)
                    {
                        actualObject.GetComponent<Animator>().SetBool("PointerEnter", false);
                        Debug.Log(actualObject.name + " PointerExit!!");
                    }

                    actualObject = bc.gameObject;

                    if (Input.GetMouseButtonDown(0))
                    {
                        switch (actualObject.tag)
                        {
                            case "GreenCommunication":
                            case "RedCommunication":
                                animPlayer.SetBool("TelephoneEvent", true);
                                break;
                            case "GreenCoordination":
                            case "RedCoordination":
                                animPlayer.SetBool("PositEvent", true);
                                break;
                            case "GreenControl":
                            case "RedControl":
                                animPlayer.SetBool("LoupeEvent", true);
                                break;
                        }
                        Debug.Log(actualObject.name + " Clicked!!");
                    }
                    else
                    {
                        actualObject.GetComponent<Animator>().SetBool("PointerEnter", true);
                        Debug.Log(actualObject.name + " PointerEnter!!");
                    }

                }
                else
                {
                    if (actualObject != null)
                    {
                        actualObject.GetComponent<Animator>().SetBool("PointerEnter", false);
                        Debug.Log(actualObject.name + " PointerExit!!");
                    }
                }
            }
        }
    }

    public static void ShowEvent()
    {
        if(actualObject != null) {
            switch(actualObject.tag)
            {
                case "RedCommunication":
                    actualObject.GetComponent<RedTelephoneHandle>().ShowEvent();
                    break;
                case "RedCoordination":
                    actualObject.GetComponent<RedPositHandle>().ShowEvent();
                    break;
                case "RedControl":
                    actualObject.GetComponent<RedLoupeHandle>().ShowEvent();
                    break;
                case "GreenCommunication":
                    actualObject.GetComponent<GreenTelephoneHandle>().ShowEvent();
                    break;
                case "GreenCoordination":
                    actualObject.GetComponent<GreenPositHandle>().ShowEvent();
                    break;
                case "GreenControl":
                    actualObject.GetComponent<GreenLoupeHandle>().ShowEvent();
                    break;
            }
        }
        
    }

    public static void DeleteEvent()
    {
        if(actualObject != null)
        {
            switch (actualObject.tag)
            {
                case "RedCommunication":
                    actualObject.GetComponent<RedTelephoneHandle>().DeleteEvent();
                    Destroy(actualObject);
                    actualObject = null;
                    animPlayer.SetBool("TelephoneEvent", false);
                    break;
                case "RedCoordination":
                    actualObject.GetComponent<RedPositHandle>().DeleteEvent();
                    Destroy(actualObject);
                    actualObject = null;
                    animPlayer.SetBool("PositEvent", false);
                    break;
                case "RedControl":
                    actualObject.GetComponent<RedLoupeHandle>().DeleteEvent();
                    Destroy(actualObject);
                    actualObject = null;
                    animPlayer.SetBool("LoupeEvent", false);
                    break;
                case "GreenCommunication":
                    actualObject.GetComponent<GreenTelephoneHandle>().DeleteEvent();
                    Destroy(actualObject);
                    actualObject = null;
                    animPlayer.SetBool("TelephoneEvent", false);
                    break;
                case "GreenCoordination":
                    actualObject.GetComponent<GreenPositHandle>().DeleteEvent();
                    Destroy(actualObject);
                    actualObject = null;
                    animPlayer.SetBool("PositEvent", false);
                    break;
                case "GreenControl":
                    actualObject.GetComponent<GreenLoupeHandle>().DeleteEvent();
                    Destroy(actualObject);
                    actualObject = null;
                    animPlayer.SetBool("LoupeEvent", false);
                    break;
            }
        }
        
    }
}
