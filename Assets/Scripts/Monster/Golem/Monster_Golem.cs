using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Golem : TestMonster {

    // Use this for initialization
    [Header("AttackValue")]
    [SerializeField]
    float AttackLowRange = 5f;
    [SerializeField]
    float WalkAround = 10f;
    [SerializeField]
    float JumpSlashRange = 10f;

    public float testRange = 5;

    float speed = 0;

    [Header("Sound Assets")]
    public AudioAssets StepSound;
    public AudioAssets AttackLowSound;
    public AudioAssets AttackLowTwoSound;
    public AudioAssets JumpSound;
    public AudioAssets AttackJumpSlashOneSound;
    public AudioAssets AttackJumpSlashTwoSound;
    public AudioAssets AttackDownAttackOneSound;
    public AudioAssets AttackDownAttackTwoSound;

    float d;

    // Set State
	protected override void Start () {
        base.Start();
        attackstates = Attackstates.Alert;
        d = Vector3.Distance(player.transform.position, transform.position);
    }
	
	// Set nav corner and speed
	protected override void Update () {
        base.Update();
        destination = Nav.GetCorners();
        if (player.activeSelf == true)
        {
            d = Vector3.Distance(player.transform.position, transform.position);
        }
        animator.SetFloat("Speed", speed);
    }
    void OnDrawGizmosSelected()
    {
        {
            Gizmos.color = new Color(0,1,1,0.3f);
            Gizmos.DrawSphere(transform.position, testRange);
        }
    }

    protected override void e_Alert()
    {
        base.e_Alert();
    }

    protected override void e_Attacking()
    {
        base.e_Attacking();

        speed = 1;
        if (d < JumpSlashRange && d > WalkAround)
        {
            animator.SetTrigger("JumpSlash");
        }else if (d < WalkAround && d > AttackLowRange)
        {
            speed = 1;
            if(d < 6)
            {
                animator.SetTrigger("DownAttack");
            }
        }
        else if (d < AttackLowRange)
        {
            animator.SetTrigger("AttackLow");
        }

    }

    void e_Attacking_OutRange()
    {
        if (d < AttackLowRange)
        {
            animator.SetTrigger("AttackLow");
        }
    }

    protected override void e_Death()
    {
        base.e_Death();
    }

    protected override void e_Patrol()
    {
        base.e_Patrol();
    }

    public override void UpdateAttackState()
    {
        base.UpdateAttackState();
    }
    

    // Should be like animation curver

    void PlayAttackLow()
    {
        AttackLowSound.Play(audioSource);
    }

    void PlayAttackLowTwo()
    {
        AttackLowTwoSound.Play(audioSource);
    }

    void PlayJump()
    {
        JumpSound.Play(audioSource);
    }

    void PlayJumpSlashOne()
    {
        AttackJumpSlashOneSound.Play(audioSource);
    }

    void PlayJumpSlashTwo()
    {
        AttackJumpSlashTwoSound.Play(audioSource);
    }
    void PlayDownAttackOne()
    {
        AttackDownAttackOneSound.Play(audioSource);
    }
    void PlayDownAttackTwo()
    {
        AttackDownAttackTwoSound.Play(audioSource);
    }

    void PlayStep()
    {
        StepSound.Play(audioSource);
    }

    void ResetTrigger()
    {
        animator.ResetTrigger("AttackLow");
        animator.ResetTrigger("JumpSlash");
    }
}
