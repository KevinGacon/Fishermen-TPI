using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    static bool isActive;
    public GameObject objs;
    void Awake()
    {
        if (!isActive)
        {
            DontDestroyOnLoad(objs);
            isActive = true;
        }
    }
}
