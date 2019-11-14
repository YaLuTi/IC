using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animator_PlayerHeal : StateMachineBehaviour
{
    PlayerHP playerHP;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        playerHP = animator.GetComponent<PlayerHP>();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        playerHP.Healed(1);
    }
}
