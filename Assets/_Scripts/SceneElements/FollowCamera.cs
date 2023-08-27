using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] private Transform followRoot;

    private void Start()
    {
        followRoot = SystemManager.Instance.cameraRoot;
    }
    private void Update()
    {
        transform.position = followRoot.position;
    }
}
