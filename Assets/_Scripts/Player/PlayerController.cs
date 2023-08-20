using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerController : NetworkBehaviour
{
    [SerializeField] Rigidbody2D rb2d;
    [SerializeField] Animator animator;
    [SerializeField] float maxSpeed, maxGravity;
    [SerializeField] float speedRate;
    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        Debug.Log(OwnerClientId);
    }

    private void FixedUpdate()
    {
        if (IsOwner)
        {

            float nowSpeed = rb2d.velocity.x;
            float nowGravtiy = rb2d.velocity.y;
            nowSpeed += InputManager.Instance.moveContorl.x * speedRate;
            nowSpeed = Mathf.Clamp(nowSpeed, -maxSpeed, maxSpeed);
            nowGravtiy = Mathf.Clamp(nowGravtiy, -maxGravity, maxGravity);
            rb2d.velocity = new Vector2(nowSpeed, nowGravtiy);
            animator.SetFloat("Speed", Mathf.Abs(nowSpeed));
            animator.SetFloat("Fall", -nowGravtiy);
            if (transform.localScale.x * nowSpeed < 0)
            {
                transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
            } 
        }
    }
}
