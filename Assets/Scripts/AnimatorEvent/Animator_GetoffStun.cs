using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animator_GetoffStun : StateMachineBehaviour
{
    MonsterBasic monster;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        monster = animator.GetComponent<MonsterBasic>();
        monster.IsStun = false;
    }
}
