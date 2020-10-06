using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Avatar boyAvatar;
    public Avatar girlAvatar;

    public Mesh boyMesh;
    public Mesh girlMesh;

    public Texture boyTexture;
    public Texture girlTexture;

    private Animator animCharacter;
    private Animator animPlayer;

    void Start()
    {
        animPlayer = GetComponent<Animator>();
        animCharacter = transform.GetChild(0).gameObject.GetComponent<Animator>();

        if(UserControl.actualUser.IsMan)
        {
            transform.GetChild(0).GetComponent<Animator>().avatar = boyAvatar;
            transform.GetChild(0).GetChild(0).GetComponent<SkinnedMeshRenderer>().sharedMesh = boyMesh;
            transform.GetChild(0).GetChild(0).GetComponent<SkinnedMeshRenderer>().sharedMaterials[0].mainTexture = boyTexture;
        }
        else
        {
            transform.GetChild(0).GetComponent<Animator>().avatar = girlAvatar;
            transform.GetChild(0).GetChild(0).GetComponent<SkinnedMeshRenderer>().sharedMesh = girlMesh;
            transform.GetChild(0).GetChild(0).GetComponent<SkinnedMeshRenderer>().sharedMaterials[0].mainTexture = girlTexture;
        }
    }

    void Update()
    {
        
    }

    void FixedUpdate()
    {
        
    }

    public void AlertObservers(string message)
    {
        switch(message)
        {
            case "Walking":
                animCharacter.SetFloat("Walking", 1.0f);
                break;
            case "Stop":
                animCharacter.SetFloat("Walking", 0.0f);
                break;
            case "ShowEvent":
                ClickEvent.ShowEvent();
                break;
        }
    }
}
