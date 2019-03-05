using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerLongSword : MonoBehaviour {

    Animator animator;
    int AttackCombo = 0;
    bool SwitchState = true;
    AnimatorStateInfo animatorState;
	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        animatorState = animator.GetCurrentAnimatorStateInfo(0);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("JoyStickTriangle"))
        {
            StopAllCoroutines();
            if (AttackCombo < 2)
            {
                AttackCombo++;
            }
            animator.SetInteger("Attack", AttackCombo);
            StartCoroutine(AttackReset());
        }
        CheckAnimatorState();
	}

    void CheckAnimatorState()
    {
        if(animatorState.shortNameHash != animator.GetCurrentAnimatorStateInfo(0).shortNameHash)
        {
            animatorState = animator.GetCurrentAnimatorStateInfo(0);
            animator.SetInteger("Attack", 0);
            // StartCoroutine(AttackCooldownCounter());
            SwitchState = true;
        }
    }

    IEnumerator AttackReset()
    {
        yield return new WaitForSecondsRealtime(0.65f);
        animator.SetInteger("Attack", 0);
        AttackCombo = 0;
        yield return 0;
    }

    IEnumerator AttackCooldownCounter()
    {
        yield return new WaitForFixedUpdate();
        Debug.Log(1);
        yield return 0;
    }
}
