using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitySingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public bool isDontDestroyOnLoad;
    public static bool isExist;
    protected virtual void Awake()
    {
        if (isDontDestroyOnLoad)
        {
            if (isExist)
            {
                Destroy(gameObject);
                return;
            }
            isExist = true;
            DontDestroyOnLoad(gameObject);
        }
    }
    private static T instance;
    public static T Instance {
        get {
            if (instance == null)
            {
                instance = FindObjectOfType(typeof(T)) as T;
            }
            return instance;
        }
    }
}
