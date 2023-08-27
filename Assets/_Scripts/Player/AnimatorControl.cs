using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorControl : MonoBehaviour
{

    [SerializeField] private Animator animator;
    [SerializeField] private PlayerController playerController;
    public void Update()
    {
        animator.SetFloat("Speed", Mathf.Abs(playerController.nowSpeed));
        animator.SetFloat("Fall", -playerController.nowGravity);
    }
}
