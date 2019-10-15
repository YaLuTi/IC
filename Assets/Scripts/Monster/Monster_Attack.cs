using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Attack : StateMachineBehaviour
{
    public float StartTime;
    public float EndTime;

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        if(animatorStateInfo.normalizedTime >= StartTime && animatorStateInfo.normalizedTime < EndTime)
        {
            Debug.Log(1);
            animator.GetComponentInChildren<WeaponColliderBasic>().StartAttack();
        }
        if (animatorStateInfo.normalizedTime >= EndTime)
        {
            Debug.Log(2);
            animator.GetComponentInChildren<WeaponColliderBasic>().StopAttack();
        }
    }
}
