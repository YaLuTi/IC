using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_AnimatorRotateSet : StateMachineBehaviour
{
    MonsterBasic monster;
    public float StartTime;
    public float EndTime;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        monster = animator.GetComponent<MonsterBasic>();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        if(animatorStateInfo.normalizedTime > StartTime && animatorStateInfo.normalizedTime < EndTime)
        {
            monster.RotateAble = false;
        }

        if (animatorStateInfo.normalizedTime > EndTime)
        {
            monster.RotateAble = true;
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        monster.RotateAble = true;
    }
}
