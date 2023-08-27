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
    private readonly NetworkVariable<InputNetworkData> _netState = new(writePerm: NetworkVariableWritePermission.Owner);
    public Vector2 moveControl;
    public static NetdataManager host;
    public bool isThisHost;
    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        Debug.Log(OwnerClientId);
        Init();
        
    }
    public override void OnDestroy()
    {
        base.OnDestroy();
        
    }

    private void Init()
    {
        if (IsOwner)
        {
            instance = this;
            AddNewOrder(OwnerClientId);
        }
        if (OwnerClientId == 0)
        {
            host = this;
            isThisHost = true;
            Debug.Log("Host确定",gameObject);
        }
        Debug.Log("生成Manager");
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
    [ServerRpc(RequireOwnership = false)]
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

    [ServerRpc(RequireOwnership = false)]
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

    [ServerRpc(RequireOwnership = false)]
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
    private void Update()
    {
        if(isThisHost)
        {
            if (IsOwner)
            {
                _netState.Value = new InputNetworkData() 
                {
                    moveDir = moveControl
                };
            }
            else
            {
                moveControl = _netState.Value.moveDir;
            }
        }
    }
}
struct InputNetworkData : INetworkSerializable
{
    private float _x, _y;
    internal Vector2 moveDir
    {
        get => new Vector2(_x, _y);
        set
        {
            _x = value.x;
            _y = value.y;
        }
    }
    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref _x);
        serializer.SerializeValue(ref _y);
    }
}