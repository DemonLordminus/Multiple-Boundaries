using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerFollowCamera : NetworkBehaviour
{
    public override void OnGainedOwnership()
    {
        base.OnGainedOwnership();
        if (followClients.Contains(OwnerClientId))
        {
            isFollow = true;
            lastPos = followCam.position; 
        }
        else
        {
            this.enabled = false;
        }
    }
    public override void OnLostOwnership()
    {
        base.OnLostOwnership();
        isFollow = false;
    }
    [SerializeField] private List<ulong> followClients;
    [SerializeField] Transform followCam;
    private Vector3 lastPos;
    [SerializeField] private bool isFollow;
    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        followCam = SystemManager.Instance.cameraRoot;
    }
    private void Update()
    {
        if(isFollow)
        {
            if(lastPos != followCam.position) 
            {
                var posDelta = followCam.position - lastPos;
                transform.position += posDelta;
                lastPos = followCam.position;
            }
        }
    }


}
