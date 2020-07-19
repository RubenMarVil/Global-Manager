using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickEvent : MonoBehaviour
{
    void Update()
    {
        RaycastHit hit;
        Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit))
            {
                BoxCollider bc = hit.collider as BoxCollider;
                if (bc != null)
                {
                    bc.gameObject.GetComponent<Animator>().SetBool("Clicked", true);
                    Debug.Log("Clicked!!");
                }
            }
        }

        if (Physics.Raycast(ray, out hit))
        {
            BoxCollider bc = hit.collider as BoxCollider;
            if (bc != null)
            {
                bc.gameObject.GetComponent<Animator>().SetBool("PointerEnter", true);
                Debug.Log("PointerEnter!!");
            }
        }
    }
}
