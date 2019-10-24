using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorSetCurver : StateMachineBehaviour
{
    public AnimationCurve curve;
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        animator.SetFloat("AttackSpeed", curve.Evaluate(animatorStateInfo.normalizedTime));
    }
}
