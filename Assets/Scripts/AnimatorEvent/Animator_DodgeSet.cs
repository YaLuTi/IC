using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animator_DodgeSet : StateMachineBehaviour
{
    PlayerMove playerMove;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        playerMove = animator.GetComponent<PlayerMove>();
        playerMove.IsDodging = true;
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        playerMove = animator.GetComponent<PlayerMove>();
        playerMove.IsDodging = false;
    }
}
