using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationCurver : MonoBehaviour {

    public Animator animator;
    AnimatorStateInfo animatorState;
    public AnimationCurveObject[] animationCurves;
    public int CurrentCurve;
    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        animatorState = animator.GetCurrentAnimatorStateInfo(0);
        if (animatorState.normalizedTime >= 1) return;
        animator.speed = animationCurves[CurrentCurve].curve.Evaluate(animatorState.normalizedTime);
        // Debug.Log(animatorState.normalizedTime);
	}

    void SetCurve(int i)
    {
        CurrentCurve = i - 1;
    }
}
