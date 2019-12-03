using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animator_SetMoveSpeed : StateMachineBehaviour
{
    PlayerMove playerMove;
    public float speed;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        animator.SetFloat("AttackCount", animator.GetFloat("AttackCount") + 1);
        playerMove = animator.gameObject.GetComponent<PlayerMove>();
        playerMove.MoveSpeedMultiplier = speed;
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
       animator.SetFloat("AttackCount", animator.GetFloat("AttackCount") - 1);
        if (animator.GetFloat("AttackCount") == 0)
        {
            playerMove = animator.gameObject.GetComponent<PlayerMove>();
            playerMove.MoveSpeedMultiplier = 1;
        }
        if (animator.tag == "Damaged")
        {
            playerMove = animator.gameObject.GetComponent<PlayerMove>();
            playerMove.MoveSpeedMultiplier = 1;
        }

    }
}
