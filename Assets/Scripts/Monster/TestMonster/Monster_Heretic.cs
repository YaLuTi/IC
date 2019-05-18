using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Heretic : TestMonster {

    bool StateLock = false;

    //Need to put it in a better position

    [Header("Battle Value")]
    [SerializeField]
    float MeleeDistance = 2;
    [SerializeField]
    float DashDistance = 6;

    // Struct
    public float AngleToPlayer = 0;
    float JumpSlash_CD = 5;
    float d;

    public AudioAssets StepAssets;
    public AudioAssets[] SlashAssets;

    enum BattleSolution
    {
        Stay,
        Runaway,
        Close
    }

    [SerializeField]
    BattleSolution solution;

    // Use this for initialization
    void Start () {
        base.Start();
        solution = BattleSolution.Stay;
    }
	


	// Update is called once per frame
	void Update () {
        base.Update();
        AngleToPlayer = Vector3.Angle(transform.forward, (player.transform.position - transform.position).normalized);
        JumpSlash_CD += Time.deltaTime;

        d = Vector3.Distance(player.transform.position, transform.position);

        if (attackstates == Attackstates.Attacking_OutRange)
        {
            e_Attacking_OutRange();
        }

        switch (solution)
        {
            case BattleSolution.Runaway:
                Runaway();
                break;
            case BattleSolution.Close:
                Close();
                break;
            case BattleSolution.Stay:
                Stay();
                break;
            default:
                break;
        }
	}

    // Solutions
    void Runaway()
    {
        moveSpeed += (3 - moveSpeed) * 0.15f;
        animator.SetFloat("Y", 1, 0.1f, Time.deltaTime);

        if (d < MeleeDistance)
        {
            solution = BattleSolution.Close;
        }
        else if (d < DashDistance)
        {
            animator.SetTrigger("JumpSlash");
        }

        destination = Nav.GetCorners();
    }

    void Close()
    {
        destination = Nav.GetCorners();

        if (d < MeleeDistance)
        {
            if (Mathf.Abs(AngleToPlayer) < 20 && d < 1.75f)
            {
                animator.ResetTrigger("JumpSlash");
                moveSpeed = 0f;
                animator.SetFloat("X", 0f, 0.1f, Time.deltaTime);
                animator.SetFloat("Y", 0f, 0.1f, Time.deltaTime);
                animator.SetTrigger("At");
                return;
            }
            else
            {
                animator.ResetTrigger("At");
                moveSpeed += (2.5f - moveSpeed) * 0.15f;
                animator.SetFloat("X", 0.55f, 0.1f, Time.deltaTime);
                animator.SetFloat("Y", 0.15f, 0.1f, Time.deltaTime);
                return;
            }
        }

        if (d < DashDistance && JumpSlash_CD > 7 && Mathf.Abs(AngleToPlayer) < 7.5f)
        {
            moveSpeed += (1.5f - moveSpeed) * 0.15f;
            JumpSlash_CD = 0;
            animator.SetTrigger("JumpSlash");
        }
        else if (d < 3.5f && JumpSlash_CD > 0.5f && JumpSlash_CD < 1.5f)
        {
            moveSpeed += (3f - moveSpeed) * 0.15f;
            Vector3 direction = (destination - transform.position).normalized;
            Quaternion qDir = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, qDir, 10 * Time.deltaTime);
            animator.SetTrigger("RotateSlash");
        }
        else if(d < DashDistance && JumpSlash_CD < 7)
        {
            moveSpeed += (3f - moveSpeed) * 0.15f;
            animator.SetFloat("X", 0.65f, 0.1f, Time.deltaTime);
            animator.SetFloat("Y", 0.4f, 0.1f, Time.deltaTime);
        }

        if (d > DashDistance)
        {
            moveSpeed += (3f - moveSpeed) * 0.15f;
            animator.SetFloat("X", 0.15f, 0.1f, Time.deltaTime);
            animator.SetFloat("Y", 0.85f, 0.1f, Time.deltaTime);
        }

        if(d > 20)
        {
            solution = BattleSolution.Runaway;
        }

    }

    void Stay()
    {
        destination = Nav.GetCorners();
        moveSpeed += (1.5f - moveSpeed) * 0.15f;
        animator.SetFloat("X", 0.6f, 0.1f, Time.deltaTime);
        animator.SetFloat("Y", 0.1f, 0.1f, Time.deltaTime);

        if(d < 10 && attackstates == Attackstates.Attacking_OutRange)
        {
            solution = BattleSolution.Close;
            StateLock = true;
        }
    }
    //Solutions

    void PlayStep()
    {
        StepAssets.Play(audioSource);
    }

    void PlaySlash()
    {
        for (int i = 0; i < SlashAssets.Length; i++)
        {
            SlashAssets[1].Play(audioSource);
        }
    }

    void e_Attacking_OutRange()
    {
        Nav.CaculatePlayerMomentum();
        if (!StateLock)
        {
            animator.SetFloat("Speed", 0);
            if (Nav.PlayerMomentum > 1)
            {
                solution = BattleSolution.Close;
                StateLock = true;
            }
            else if(Nav.PlayerMomentum < -1)
            {
                solution = BattleSolution.Runaway;
                StateLock = true;
            }
            else
            {
                solution = BattleSolution.Stay;
            }
        }
    }

    public override void UpdateAttackState()
    {
        base.UpdateAttackState();
        if (attackstates == Attackstates.Attacking)
        {
            destination = Nav.GetCorners();
            if (d > BattleRange)
            {
                attackstates = Attackstates.Attacking_OutRange;
                animator.SetBool("IsLock", true);
            }
        }
    }

    public override void Damaged(float damage)
    {
        base.Damaged(damage);
        attackstates = Attackstates.Attacking_OutRange;
    }
}
