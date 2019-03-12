using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerLongSword : MonoBehaviour {

    Animator animator;
    int AttackCombo = 0;
    bool SwitchState = true;
    AnimatorStateInfo animatorState;

    WeaponColliderBasic weapon;

    [SerializeField]
    float[] AttackTrueNormalized;
    [SerializeField]
    float[] AttackFalseNormalized;
    
    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
        animatorState = animator.GetCurrentAnimatorStateInfo(0);
        weapon = GetComponentInChildren<WeaponColliderBasic>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("JoyStickTriangle"))
        {
            StopAllCoroutines();
            StartCoroutine(AttackReset());
            if (SwitchState)
            {
                SwitchState = false;
                if (AttackCombo < 3)
                {
                    AttackCombo++;
                }
                animator.SetInteger("Attack", AttackCombo);
                StartCoroutine(Attack(AttackCombo));
            }
        }
        CheckAnimatorState();
	}

    void CheckAnimatorState()
    {
        if(animatorState.shortNameHash != animator.GetCurrentAnimatorStateInfo(0).shortNameHash)
        {
            animatorState = animator.GetCurrentAnimatorStateInfo(0);
            // animator.SetInteger("Attack", 0);
            // StartCoroutine(AttackCooldownCounter());
            SwitchState = true;
        }
    }

    

    IEnumerator Attack(int Combo)
    {
        yield return new WaitForFixedUpdate();
        while (true)
        {
            AnimatorStateInfo animatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);
            if(animatorStateInfo.normalizedTime > AttackTrueNormalized[Combo - 1])
            {
                // weapon.StartAttack();
                yield return 0;
            }
            if (animatorStateInfo.normalizedTime > AttackFalseNormalized[Combo - 1])
            {
                // weapon.StopAttack();
                SwitchState = true;
                break;
            }
            yield return 0;
        }
        yield return 0;
    }

    IEnumerator AttackReset()
    {
        yield return new WaitForSecondsRealtime(1f);
        animator.SetInteger("Attack", 0);
        AttackCombo = 0;
        yield return 0;
    }

    IEnumerator AttackCooldownCounter()
    {
        yield return new WaitForFixedUpdate();

        yield return 0;
    }
}
