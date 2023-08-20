using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[DefaultExecutionOrder(-100)]
public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    //�ⲿ���ɷ���
    private static T instance;
    //�ⲿ�ɷ��ʣ��в��ɸ���
    public static T Instance
    {
        get
        {
            if (instance != null)
            {
                return instance;
            }
            else 
            {
                if(!Application.isEditor)
                Debug.LogWarningFormat("No Instance {0} (Stacktrace: {1})", typeof(T), Environment.StackTrace);
                return instance;
            }
        }
    }


    protected virtual void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarningFormat("Duplicate {0} detected. Destroying new instance.", typeof(T));
            Destroy(gameObject);
        }
        else
        {
            instance = (T)this;
        }
    }
    public static bool IsInitiailzed
    {
        get { return instance != null; }
    }
    protected virtual void OnDestory()
    {
        if(instance == this)
        {
            instance = null;
        }
    }
}
