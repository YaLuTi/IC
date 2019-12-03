using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animator_DodgeSet : StateMachineBehaviour
{
    PlayerMove playerMove;
    PlayerHP playerHP;
    bool On = false;
    bool Off = false;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        playerMove = animator.GetComponent<PlayerMove>();
        playerHP = animator.GetComponent<PlayerHP>();
        playerMove.IsDodging = true;
        On = false;
        Off = false;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        if(animatorStateInfo.normalizedTime > 0.15f && !On)
        {
            On = true;
            playerHP.SetInvulnerability();
        }
        if(animatorStateInfo.normalizedTime > 0.65f && !Off)
        {
            Off = true;
            playerHP.ReSetInvulnerability();
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        playerMove = animator.GetComponent<PlayerMove>();
        playerMove.IsDodging = false;
        playerHP.ReSetInvulnerability();
    }
}
