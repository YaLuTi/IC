using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animator_SpawnParticle : StateMachineBehaviour
{
    public float SpawnTime;
    public int SpawnNum;
    bool IsSpawn = false;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        IsSpawn = false;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        if (animatorStateInfo.normalizedTime > SpawnTime && !IsSpawn)
        {
            Debug.Log(0);
            animator.GetComponent<Monster_Golem>().SpawnParticle(SpawnNum);
            IsSpawn = true;
        }
    }
}
