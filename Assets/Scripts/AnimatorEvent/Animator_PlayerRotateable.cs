using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animator_PlayerRotateable : StateMachineBehaviour
{
    PlayerMove player;
    public float StartTime;
    public float EndTime;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        player = animator.GetComponent<PlayerMove>();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        if (animatorStateInfo.normalizedTime > StartTime && animatorStateInfo.normalizedTime < EndTime)
        {
            player.Rotateable = false;
        }

        if (animatorStateInfo.normalizedTime > EndTime)
        {
            player.Rotateable = true;
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        player.Rotateable = true;
    }
}
