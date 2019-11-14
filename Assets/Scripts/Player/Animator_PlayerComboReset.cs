using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animator_PlayerComboReset : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        animator.SetBool("IsOnCombo", false);
    }
}
