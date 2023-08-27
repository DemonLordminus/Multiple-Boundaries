using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ChangeOwnerShip : MonoBehaviour
{
    [SerializeField] private LayerMask playerLayer;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((playerLayer.value & 1 << collision.gameObject.layer) > 0)
        {
            Debug.Log($"changeTo {NetdataManager.instance.OwnerClientId}");
            //collision.GetComponent<NetworkObject>().ChangeOwnership(NetdataManager.instance.OwnerClientId);
            //NetdataManager.instance.ChangeOwnerShip(collision.GetComponent<NetworkObject>(), NetdataManager.instance.OwnerClientId);
            collision.GetComponent<ChangeOwnerShipInPlayer>().ChangeOwnership(NetdataManager.instance.OwnerClientId);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((playerLayer.value & 1 << collision.gameObject.layer) > 0)
        {
            collision.GetComponent<ChangeOwnerShipInPlayer>().LeaveOwnership(NetdataManager.instance.OwnerClientId); 
        }
    }
}
