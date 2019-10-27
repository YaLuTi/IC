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
            transforms = animator.gameObject.GetComponent<ProjectileTransform>();
            Transform t = transforms.GetTransform(FirePosition);
            Instantiate(projectile, t.position, t.rotation);
            Shoot = true;
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        Shoot = false;
    }
}
