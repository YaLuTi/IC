using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerMove))]
public class PlayerLongSword : MonoBehaviour {

    Animator animator;
    PlayerMove playerMove;
    [SerializeField]
    int AttackCombo = 0;
    [SerializeField]
    bool SwitchState = true;
    AnimatorStateInfo animatorState;

    public AudioEvent SlashSound;
    public AudioEvent SlashSoundTwo;
    AudioSource audioSource;

    WeaponColliderBasic weapon;

    [SerializeField]
    float[] AttackTrueNormalized;
    [SerializeField]
    float[] AttackFalseNormalized;

    [SerializeField]
    float[] HeavyAttackTrueNormalized;
    [SerializeField]
    float[] HeavyAttackFalseNormalized;

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
        playerMove = GetComponent<PlayerMove>();
    }
	
	// Update is called once per frame
	void Update () {

        animatorState = animator.GetCurrentAnimatorStateInfo(0);
        DodgeInterrupt();

        if (Input.GetButtonDown("R1"))
        {
            StopCoroutine(AttackReseter);
            animator.SetTrigger("At");
            animator.ResetTrigger("HeavyAt");
            AttackReseter = StartCoroutine(AttackReset());
            if (SwitchState)
            {
                SwitchState = false;
                if (AttackCombo < AttackTrueNormalized.Length)
                {
                    AttackCombo++;
                    animator.SetInteger("Attack", AttackCombo);
                }
            }
        }
        if (Input.GetButtonDown("R2"))
        {
            StopCoroutine(AttackReseter);
            animator.SetTrigger("HeavyAt");
            animator.ResetTrigger("At");
            AttackReseter = StartCoroutine(AttackReset());
            if (SwitchState)
            {
                SwitchState = false;
                if (AttackCombo < AttackTrueNormalized.Length)
                {
                    AttackCombo++;
                    animator.SetInteger("Attack", AttackCombo);
                }
            }
        }
        CheckAnimatorState();
	}

    void DodgeInterrupt()
    {
        // Final attack need another Judgment
        if (animatorState.IsTag("Attack") && (animator.GetBool("Dodge") || animator.GetBool("IsSteping")))
        {
            if (animatorState.normalizedTime > 0.4f)
            {
                StopCoroutine(AttackReseter);
                animator.SetInteger("Attack", 0);
                AttackCombo = 0;
                SwitchState = true;
            }
        }
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
    void PlaySlashTwo()
    {
        SlashSoundTwo.Play(audioSource);
    }

    void WeaponOn()
    {
        SwitchState = true;
        weapon.StartAttack();
    }
    void WeaponOff()
    {
        weapon.StopAttack();
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
        animator.ResetTrigger("At");
        animator.ResetTrigger("HeavyAt");
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
