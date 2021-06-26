using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinousMusic : MonoBehaviour
{
    private static ContinousMusic instance = null;

    void Start()
    {
        
    }

    public static ContinousMusic Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(this.gameObject);
    }

    void Update()
    {
        
    }
}
