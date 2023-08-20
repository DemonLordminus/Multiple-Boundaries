using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using System;

public class NetdataManager : NetworkBehaviour
{
    public List<ulong> clientOrder;
    public static NetdataManager instance;
    public static event Action OnOrderUpdate;


    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        if (IsOwner)
        {
            instance = this;
            AddNewOrder(OwnerClientId);
        }
        Debug.Log("Éú³ÉManager");

    }
    public override void OnNetworkDespawn()
    {
        base.OnNetworkDespawn();
        if (IsOwner)
        {
            RemoveOrder(); 
        }
    }
    private void OnApplicationFocus(bool focus)
    {
        if(focus && IsOwner)
        {
            ChangeOrder();
        }
    }
    private void AddNewOrder(ulong clientID)
    {

        AddNewOrderServerRPC(clientID);

    }
    [ServerRpc]
    private void AddNewOrderServerRPC(ulong clientID)
    {
        //Debug.Log(clientID);
        AddNewOrderClientRPC(clientID);
        //clientOrder.Insert(0, clientID);
    }
    [ClientRpc]
    private void AddNewOrderClientRPC(ulong clientID)
    {
        instance.clientOrder.Insert(0, clientID);
        OnOrderUpdate?.Invoke();
    }

    private void ChangeOrder()
    {
        ChangeOrderServerRPC(OwnerClientId);

    }

    [ServerRpc]
    private void ChangeOrderServerRPC(ulong clientID)
    {
        //clientOrder.Remove(clientID);
        //clientOrder.Insert(0, clientID);
        //OnOrderUpdate?.Invoke();
        ChangeOderClientRPC(clientID);
    }
    [ClientRpc]
    private void ChangeOderClientRPC(ulong clientID)
    {
        instance.clientOrder.Remove(clientID);
        instance.clientOrder.Insert(0, clientID);
        OnOrderUpdate?.Invoke();
    }
    public int GetNowOrder()
    {
        return clientOrder.FindIndex(u => u == OwnerClientId);
    }


    private void RemoveOrder()
    {
        RemoveOrderServerRPC(OwnerClientId);

    }

    [ServerRpc]
    private void RemoveOrderServerRPC(ulong clientID)
    {
        //clientOrder.Remove(clientID);
        //clientOrder.Insert(0, clientID);
        //OnOrderUpdate?.Invoke();
        RemoveOrderClientRPC(clientID);
    }
    [ClientRpc]
    private void RemoveOrderClientRPC(ulong clientID)
    {
        instance.clientOrder.Remove(clientID);
        OnOrderUpdate?.Invoke();
    }
}
