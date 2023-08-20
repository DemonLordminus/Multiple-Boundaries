using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemManager : Singleton<SystemManager>
{
    private new void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }
    public CameraGroup cameraGroup;
    public ControllerWhitWindowPosition controller;

    //private void OnDestroy()
    //{
    //    Destroy(cameraGroup.gameObject);
    //    Destroy(controller.gameObject);
    //}
}
