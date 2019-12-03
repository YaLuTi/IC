using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animator_WeaponTrail : StateMachineBehaviour
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
        if(animatorStateInfo.normalizedTime > StartTime && !On)
        {
            Debug.Log("TON");
            On = true;
            animator.gameObject.GetComponent<MonsterWeaponTest>().TrailOn();
        }
        if (animatorStateInfo.normalizedTime > EndTime && !Off)
        {
            Debug.Log("TFF");
            Off = true;
            animator.gameObject.GetComponent<MonsterWeaponTest>().TrailOff();
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        
    }
}
