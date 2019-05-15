using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationCurver : MonoBehaviour {

    public Animator animator;
    AnimatorStateInfo animatorState;
    public AnimationCurveObject[] animationCurves;
    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        animatorState = animator.GetCurrentAnimatorStateInfo(0);
        animator.speed = animationCurves[0].curve.Evaluate(animatorState.normalizedTime);
	}
}
