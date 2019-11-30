using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animator_CostSP : StateMachineBehaviour
{
    public float CostSP;
    PlayerHP playerHP;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        if(playerHP == null)
        {
            playerHP = animator.gameObject.GetComponent<PlayerHP>();
        }
        playerHP.ExpendSP(CostSP);
    }
}
