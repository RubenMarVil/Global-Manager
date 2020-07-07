using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommunicationHandle : MonoBehaviour
{
    public int site1Num;
    public int site2Num;
    public List<string> communicationList;
    private GameObject telephone;
    private GameObject whatsapp;
    private GameObject skype;
    private GameObject forum;
    private GameObject teams;
    private GameObject email;

    void Start()
    {
        string[] sites = name.Split('-');

        site1Num = Int32.Parse(sites[0]);
        site2Num = Int32.Parse(sites[1]);

        communicationList = new List<string>();

        telephone = transform.GetChild(1).gameObject;
        whatsapp = transform.GetChild(2).gameObject;
        skype = transform.GetChild(3).gameObject;
        forum = transform.GetChild(4).gameObject;
        teams = transform.GetChild(5).gameObject;
        email = transform.GetChild(6).gameObject;
    }

    void Update()
    {
        communicationList.Clear();

        if (telephone.GetComponent<CommunicationButtonHandle>().selected)
            communicationList.Add(telephone.name);
        if (whatsapp.GetComponent<CommunicationButtonHandle>().selected)
            communicationList.Add(whatsapp.name);
        if (skype.GetComponent<CommunicationButtonHandle>().selected)
            communicationList.Add(skype.name);
        if (forum.GetComponent<CommunicationButtonHandle>().selected)
            communicationList.Add(forum.name);
        if (teams.GetComponent<CommunicationButtonHandle>().selected)
            communicationList.Add(teams.name);
        if (email.GetComponent<CommunicationButtonHandle>().selected)
            communicationList.Add(email.name);
    }
}
