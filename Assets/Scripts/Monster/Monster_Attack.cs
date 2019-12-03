using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Attack : StateMachineBehaviour
{
    public float StartTime;
    public float EndTime;
    bool On = false;
    bool Off = false;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        On = false;
        Off = false;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        if(animatorStateInfo.normalizedTime >= StartTime && !On)
        {
            Debug.Log("On");
            On = true;
            animator.GetComponentInChildren<WeaponColliderBasic>().StartAttack();
        }
        if (animatorStateInfo.normalizedTime >= EndTime && !Off)
        {
            Debug.Log("Off");
            Off = true;
            animator.GetComponentInChildren<WeaponColliderBasic>().StopAttack();
        }
    }
}
