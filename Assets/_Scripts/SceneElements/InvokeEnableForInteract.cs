using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvokeEnableForInteract : MonoBehaviour
{
    [SerializeField] private int id;
    [SerializeField] private bool isNormal;
    
    public void InvokeEnableEvent()
    {
        EnableForInteract.OnInteract?.Invoke(id, isNormal);
        Destroy(gameObject);
    }
}
