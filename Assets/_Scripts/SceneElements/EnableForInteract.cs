using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class EnableForInteract : MonoBehaviour
{
    public static Action<int,bool> OnInteract;
    [SerializeField] private int id;
    [SerializeField] private bool isNormalActive;//按下前是否启用
    [SerializeField] private bool isLimitID;
    [SerializeField] private List<ulong> onlyForClientIDs;

    private void Awake()
    {
        OnInteract += SetEnable;
        if(isNormalActive == false)
        {
            gameObject.SetActive(false);
        }
    }
    private void OnDestroy()
    {
        OnInteract -= SetEnable;
    }

    private void SetEnable(int _id,bool isEnable)
    {
        if(_id == id)
        {
            if(isLimitID)
            {
                if (!onlyForClientIDs.Contains(NetworkManager.Singleton.LocalClientId))
                { return; }    
            }
            gameObject.SetActive(isEnable == isNormalActive);
        }
    }
}
