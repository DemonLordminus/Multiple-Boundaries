using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;

public class OrderShowText : NetworkBehaviour
{
    [SerializeField] TextMeshProUGUI nowOrderText;
    [SerializeField] TextMeshProUGUI OrderListText;
    [SerializeField] TextMeshProUGUI IDText;
    private void Awake()
    {
        NetdataManager.OnOrderUpdate += UpdateOrderList;
        NetdataManager.OnOrderUpdate += UpdateNowOrder;
    }
    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        if(!IsOwner) 
        {
            nowOrderText.gameObject.SetActive(false);
            IDText.gameObject.SetActive(false);
        }
        if(!IsHost)
        {
            OrderListText.gameObject.SetActive(false);
            NetdataManager.OnOrderUpdate -= UpdateOrderList;
        }
        IDText.text = OwnerClientId.ToString();
    }
    public override void OnNetworkDespawn()
    {
        base.OnNetworkDespawn();
        OrderListText.gameObject.SetActive(true);
        NetdataManager.OnOrderUpdate += UpdateOrderList;
    }

    private void UpdateNowOrder()
    {
        nowOrderText.text = $"{NetdataManager.instance.GetNowOrder()}";
    }
    private void UpdateOrderList()
    {
        var orderList = NetdataManager.instance.clientOrder;
        string listString = string.Empty;
        for (int i = 0; i < orderList.Count; i++)
        {
            listString += orderList[i].ToString();
            listString += "\n";
        }
        OrderListText.text = listString;
    }
}
