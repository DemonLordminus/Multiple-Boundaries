using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InputSystem;
using Unity.Netcode;

public class InputManager : NetworkBehaviour
{
    //[SerializeField] private float _cheapInterpolationTime = 0.1f;
    [SerializeField] private bool isUsingThis = false;
    [SerializeField] bool isUpdate = false;
    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        if(IsHost)
        {
            if(Instance != null)
            {
                Debug.LogWarning("input Manager÷ÿ∏¥");
                Destroy(gameObject);
            }
            Instance = this;
        }
    }
    //private readonly NetworkVariable<InputNetworkData> _netState = new(writePerm: NetworkVariableWritePermission.Owner);

    public static InputManager Instance;    
    public MainInput mainInput;
    public Vector2 moveContorl;
    private void Awake()
    {
        mainInput = new MainInput();
        NetButton.OnNetDone += () => { isUpdate = true; };
    }
    private void OnEnable()
    {
        mainInput.Enable();
    }
    private void OnDisable()
    {
        mainInput.Disable();
    }
    private void Update()
    {
        if (isUsingThis && isUpdate)
        {
            //Debug.Log("test");
            var nowMoveDir = mainInput.MainControl.Move.ReadValue<Vector2>();
            if (IsHost)
            {
                moveContorl = nowMoveDir;
            }
            else
            {
                MoveDirUpdateServerRPC(nowMoveDir);
            }
        }
    }

    private void OnApplicationFocus(bool focus)
    {
        isUsingThis = focus;
    }
    [ServerRpc(RequireOwnership = false)]
    private void MoveDirUpdateServerRPC(Vector2 newMoveDir)
    {
        moveContorl = newMoveDir;
    }
}

//struct InputNetworkData : INetworkSerializable
//{
//    private float _x, _y;
//    internal Vector2 moveDir
//    {
//        get => new Vector2(_x, _y);
//        set
//        {
//            _x = value.x;
//            _y = value.y;
//        }
//    }
//    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
//    {
//        serializer.SerializeValue(ref _x);
//        serializer.SerializeValue(ref _y);
//    }
//}