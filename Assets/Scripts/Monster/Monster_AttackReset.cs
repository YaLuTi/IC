using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_AttackReset : StateMachineBehaviour
{
    MonsterBasic monster;

    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        monster = animator.GetComponent<MonsterBasic>();
        monster.IsAttacking = false;
    }
}
