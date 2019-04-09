using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerLongSword : MonoBehaviour {

    Animator animator;
    [SerializeField]
    int AttackCombo = 0;
    [SerializeField]
    bool SwitchState = true;
    AnimatorStateInfo animatorState;

    public AudioEvent SlashSound;
    AudioSource audioSource;

    WeaponColliderBasic weapon;

    [SerializeField]
    float[] AttackTrueNormalized;
    [SerializeField]
    float[] AttackFalseNormalized;

    Coroutine AttackReseter;
    Coroutine AttackOne;
    Coroutine AttackTwo;
    Coroutine AttackThree;

    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
        animatorState = animator.GetCurrentAnimatorStateInfo(0);
        weapon = GetComponentInChildren<WeaponColliderBasic>();
        AttackReseter = StartCoroutine(AttackReset());
        audioSource = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("JoyStickTriangle"))
        {
            StopCoroutine(AttackReseter);
            AttackReseter = StartCoroutine(AttackReset());
            if (SwitchState)
            {
                SwitchState = false;
                if (AttackCombo < 3)
                {
                    AttackCombo++;
                    StartCoroutine(Attack(AttackCombo));
                    animator.SetInteger("Attack", AttackCombo);
                }
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
            // SwitchState = true;
        }
    }

    void PlaySlash()
    {
        SlashSound.Play(audioSource);
    }

    IEnumerator Attack(int Combo)
    {
        yield return new WaitForFixedUpdate();
        yield return new WaitForFixedUpdate();
        SwitchState = true;

        while (!animator.GetCurrentAnimatorStateInfo(0).IsName("Attack" + Combo))
        {
            yield return 0;
        }

        while (true)
        {
            AnimatorStateInfo animatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);
            if(animatorStateInfo.normalizedTime > AttackTrueNormalized[Combo - 1])
            {
                weapon.StartAttack();
                yield return 0;
            }
            if (animatorStateInfo.normalizedTime > AttackFalseNormalized[Combo - 1])
            {
                weapon.StopAttack();
                break;
            }
            yield return 0;
        }
        
        if(Combo >= AttackTrueNormalized.Length)
        {
            animator.SetInteger("Attack", 0);
            AttackCombo = 0;
            SwitchState = true;
        }
        yield return 0;
    }

    IEnumerator AttackReset()
    {
        yield return new WaitForSecondsRealtime(1f);
        animator.SetInteger("Attack", 0);
        AttackCombo = 0;
        SwitchState = true;
        yield return 0;
    }

    IEnumerator AttackCooldownCounter()
    {
        yield return new WaitForFixedUpdate();

        yield return 0;
    }
}
