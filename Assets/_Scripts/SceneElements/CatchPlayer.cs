using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class CatchPlayer : NetworkBehaviour
{
    [SerializeField] bool isCatch;
    [SerializeField] GameObject catchObject;
    [SerializeField] ulong ownerID;
    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        if(NetworkManager.Singleton.LocalClientId == ownerID)
        {
            SetOwner();
        }
    }
    private void SetOwner()
    {
        SetOwnerServerRpc(ownerID);
    }
    [ServerRpc(RequireOwnership = false)]
    private void SetOwnerServerRpc(ulong newOwner)
    {
        GetComponent<NetworkObject>().ChangeOwnership(newOwner);
    }

    [Button("CatchPlayerToThis")]
    public void CatchPlayerToThis(GameObject _catchObject)
    {
        if (!isCatch)
        {
            isCatch = true;
            catchObject = _catchObject;
        }
        else
        {
            isCatch = false;
            catchObject = null;
        }
    }
    [ClientRpc]
    private void ForceMoveClientRpc()
    {

    }
    private void Update()
    {
        if(isCatch)
        {
            catchObject.transform.position = transform.position;
        }   
    }

}
