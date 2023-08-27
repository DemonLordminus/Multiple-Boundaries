using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Rendering;

public class ChangeOwnerShipInPlayer : NetworkBehaviour
{
    [SerializeField] private SortingGroup hideGroup;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private NetworkObject networkObject;
    [SerializeField] private bool isHide,isHalfHide;
    //[SerializeField] private Color normal, halfHide;
    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        if (!IsHost && isHide)
        {
            if (!isHalfHide)
            {
                hideGroup.enabled = true; 
            }
            else
            {
                //normal = spriteRenderer.color;
                var color = spriteRenderer.color;
                spriteRenderer.color = new Color(color.r,color.g,color.b, 0.5f);
            }
        }
    }
    public void ChangeOwnership(ulong targetID)
    {
        ChangeOwnerShipServerRPC(targetID);
    }
    [ServerRpc(RequireOwnership = false)]
    public void ChangeOwnerShipServerRPC(ulong targetID,int nowCheck = -1)
    {
        var targetIndex = NetdataManager.host.clientOrder.FindIndex(x => x == targetID);
        var nowIndex = NetdataManager.host.clientOrder.FindIndex(x => x == OwnerClientId);
        if(nowIndex > targetIndex)
        {
            networkObject.ChangeOwnership(targetID);
            SpriteHideClientRPC();
        }
        else
        {

        }
        //Debug.Log(nowCheck);
        //int p;
        //if(nowCheck == -1)
        //{
        //}
        //else
        //{
        //    p = nowCheck;
        //}
        ////ulong[] tests = new ulong[p];
        ////for (int i = 0; i < p; i++)
        ////{
        ////    Debug.Log(i);
        ////    tests[i] = NetdataManager.host.clientOrder[i];
        ////}
        //if (p == 0)
        //{
        //    if (!spriteRenderer.isVisible)
        //    {
        //        networkObject.ChangeOwnership(targetID);
        //        SpriteHideClientRPC();
        //    }
        //    return;
        //}

        //ClientRpcParams clientRpcParams = new ClientRpcParams
        //{
        //    Send = new ClientRpcSendParams
        //    {
        //        TargetClientIds = new ulong[]
        //        {
        //            NetdataManager.host.clientOrder[p - 1]
        //        }
        //    }
        //};
        //CheckIsCanChangeOwnerShipClientRpc(targetID,p - 1,clientRpcParams);

    }
    public void LeaveOwnership(ulong targetID)
    {
        if (IsOwner)
        {
            LeaveOwnershipServerRPC(targetID); 
        }
    }
    [ServerRpc(RequireOwnership = false)]
    public void LeaveOwnershipServerRPC(ulong targetID, int nowCheck = -1)
    {
        int p = 0;
        if (nowCheck == -1)
        {
            p = NetdataManager.host.clientOrder.FindIndex(x => x == targetID);
        }
        else
        {
            p = nowCheck;
        }
        //Debug.Log(p);
        //Debug.Log(NetdataManager.host.clientOrder.Count-1);
        //ulong[] tests = new ulong[p];
        //for (int i = p; i < NetdataManager.host.clientOrder.Count; i++)
        //{
        //    tests[i] = NetdataManager.host.clientOrder[i];
        //}
        if (p == NetdataManager.host.clientOrder.Count - 1)
        {
            return;
        }
        ClientRpcParams clientRpcParams = new ClientRpcParams
        {
            Send = new ClientRpcSendParams
            {
                TargetClientIds = new ulong[]
                {
                    NetdataManager.host.clientOrder[p + 1]
                }
            }
        };

        CheckIsCanChangeOwnerShipClientRpc(targetID, nowCheck + 1, clientRpcParams);

    }
    [ClientRpc]
    public void SpriteHideClientRPC()
    {
        if (isHide)
        {
            if (!IsOwner)
            {
                if(isHalfHide)
                {
                    var color = spriteRenderer.color;
                    spriteRenderer.color = new Color(color.r, color.g, color.b, 0.5f);
                }
                else
                { 
                
                    hideGroup.enabled = true;
                }
            }
            else
            {
                if (isHalfHide)
                {
                    var color = spriteRenderer.color;
                    spriteRenderer.color = new Color(color.r, color.g, color.b, 1f);
                }
                else
                {
                    hideGroup.enabled = false;
                }
              
            } 
        }
    }

    [ClientRpc]
    public void CheckIsCanChangeOwnerShipClientRpc(ulong targetID, int nowCheck, ClientRpcParams clientRpcParams = default)
    {
        //Debug.Log(nowCheck + "p");
        if (!spriteRenderer.isVisible)
        {
            LeaveOwnershipServerRPC(targetID, nowCheck);
        }
        else
        {
            JustChangeOwnerShipServerRPC(NetworkManager.Singleton.LocalClientId);
        }
    }
    [ServerRpc(RequireOwnership = false)]
    public void JustChangeOwnerShipServerRPC(ulong targetID, int nowCheck = -1)
    {
        networkObject.ChangeOwnership(targetID);
        SpriteHideClientRPC();
    }
    //[ClientRpc]
    //public void CheckIsCanChangeOwnerShipClientRpc(ulong targetID,int nowCheck,ClientRpcParams clientRpcParams = default)
    //{
    //    if (!spriteRenderer.isVisible)
    //    {
    //        ChangeOwnerShipServerRPC(targetID, nowCheck); 
    //    }
    //}
}
