using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetButton : NetworkBehaviour
{
    private float nowTime = 10f;
    public static event Action OnNetDone;
    [SceneName] public string firstScene;
    private async void Start()
    {
        if(NetworkManager.Singleton.IsConnectedClient || IsHost)
        {
            return;
        }
        await Task.Yield();
        TestServer();

    }
    [EditorButton]
    private async void TestServer()
    {
        NetworkManager.Singleton.StartClient();
        if(Application.isEditor)
        {
            await Task.Delay(1000);
        }
        else
        {
            await Task.Delay(2000);
        }
        // 判断当前是否有主机
        if (NetworkManager.Singleton.IsConnectedClient)
        {
            Debug.Log("HasServer");
            OnNetDone?.Invoke();
        }
        else
        {
            TestHost();
            //NetworkManager.Singleton.DisconnectClient(NetworkManager.Singleton.LocalClientId);
            //// 自动连接到主机
            //Debug.Log("Running as Client");
            Debug.Log("NoServer");
        }
      
    }
    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        //SpawnDataManager();
    }
    //private void SpawnDataManager()
    //{
    //    SpawnPlayerServerRpc(NetworkManager.Singleton.LocalClientId);
    //}
    //[ServerRpc(RequireOwnership = false)]
    //private void SpawnPlayerServerRpc(ulong playerId)
    //{
    //    var spawn = Instantiate(netdataManager);
    //    spawn.NetworkObject.SpawnAsPlayerObject(playerId);
    //}
    private void Update()
    {
        if (NetworkManager.Singleton.IsConnectedClient || NetworkManager.Singleton.IsServer)
        {
            NetworkManager.Singleton.SceneManager.LoadScene(firstScene, LoadSceneMode.Single);
        }
    }

    private async void TestHost()
    {
        nowTime = 10f;
        NetworkManager.Singleton.Shutdown();
        while (nowTime>0)
        {
            nowTime-= Time.deltaTime;
            if(!NetworkManager.Singleton.ShutdownInProgress)
            {
                break;
            }
            await Task.Yield();
        }
        NetworkManager.Singleton.StartHost();
        OnNetDone?.Invoke();
    }
    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 10, 300, 300));
        if (!NetworkManager.Singleton.IsClient && !NetworkManager.Singleton.IsServer)
        {
            StartButtons();
        }
        else
        {
            StatusLabels();
        }

        GUILayout.EndArea();
    }

    static void StartButtons()
    {
        //if (GUILayout.Button("Host")) NetworkManager.Singleton.StartHost();
        //if (GUILayout.Button("Client")) NetworkManager.Singleton.StartClient();
        //if (GUILayout.Button("Server")) NetworkManager.Singleton.StartServer();
    }

    static void StatusLabels()
    {
        //var mode = NetworkManager.Singleton.IsHost ?
        //    "Host" : NetworkManager.Singleton.IsServer ? "Server" : "Client";

        //GUILayout.Label("Transport: " +
        //    NetworkManager.Singleton.NetworkConfig.NetworkTransport.GetType().Name);
        //GUILayout.Label("Mode: " + mode);
    }

}



