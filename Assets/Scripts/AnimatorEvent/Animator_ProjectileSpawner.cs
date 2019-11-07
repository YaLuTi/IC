using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animator_ProjectileSpawner : StateMachineBehaviour
{
    public int FirePosition;
    public float ActiveTime;
    public GameObject projectile;
    ProjectileTransform transforms;
    bool Shoot = false;

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        if(animatorStateInfo.normalizedTime >= ActiveTime && !Shoot)
        {

            animator.GetComponent<ProjectileTransform>().FireProjectile(0);
            Shoot = true;
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        Shoot = false;
    }
}
