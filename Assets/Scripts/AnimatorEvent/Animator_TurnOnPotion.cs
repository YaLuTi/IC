using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animator_TurnOnPotion : StateMachineBehaviour
{
    public float time;
    bool IsOn;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        IsOn = false;
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        if(animatorStateInfo.normalizedTime > time && !IsOn)
        {
            animator.GetComponent<PlayerHP>().TurnOnHealthPotion();
            IsOn = true;
        }
    }
}
