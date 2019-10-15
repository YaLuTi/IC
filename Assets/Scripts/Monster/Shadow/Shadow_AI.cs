﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow_AI : TestMonster
{
    public float LongAttackRange;
    float LongAttackCD = 7.5f;
    float LongAttackCount = 0;
    bool LongAttacking = false;
    public float RotateAttackRange;
    float RotateAttackCD = 5f;
    public float MeleeAttackRange;
    float MeleeAttackCD = 1.5f;
    float PlayerDistance = 0;
    protected override void Start()
    {
        base.Start();
        animator.SetFloat("Speed", moveSpeed);
        // attackstates = Attackstates.Attacking;
    }

    protected override void Update()
    {
        PlayerDistance = Mathf.Abs(Vector3.Distance(player.transform.position, transform.position));
        base.Update();
        destination = Nav.GetCorners();
        CD();
    }

    void CD()
    {
        if (LongAttackCD < 7.5f)
        {
            LongAttackCD += Time.deltaTime;
        }
        if(RotateAttackCD < 5f)
        {
            RotateAttackCD += Time.deltaTime;
        }
        if(MeleeAttackCD < 1.5f)
        {
            MeleeAttackCD += Time.deltaTime;
        }
    }

    protected override void e_Attacking()
    {
        base.e_Attacking();
        // If Group need to decide battle solution
        solution = BattleSolution.MeleeAttack;

        // if (IsAttacking) return;
        switch (solution)
        {
            case BattleSolution.MeleeAttack:
                animator.SetFloat("Speed", 1);
                if (LongAttacking)
                {
                    LongAttackCount += Time.deltaTime;
                    if (PlayerDistance < 3 && LongAttackCount > 1.25f)
                    {
                        animator.SetTrigger("LongAttackReady");
                        LongAttacking = false; LongAttackCount = 0;
                    }
                    if(LongAttackCount > 3f)
                    {
                        animator.SetTrigger("LongAttackReady");
                        LongAttacking = false; LongAttackCount = 0;
                    }
                }
                if (PlayerDistance < RotateAttackRange && !IsAttacking && RotateAttackCD >= 5f)
                {
                    RotateAttackCD = 0;
                    animator.SetTrigger("RotateAttack");
                    IsAttacking = true;
                }
                if (PlayerDistance < LongAttackRange && !IsAttacking && LongAttackCD >= 7.5f)
                {
                    LongAttackCD = 0;
                    animator.SetTrigger("LongAttackPrepare");
                    LongAttacking = true;
                    IsAttacking = true;
                }
                if(LongAttackCD < 7.5f && RotateAttackCD < 5f)
                {
                    if(PlayerDistance < 2 && !IsAttacking)
                    {
                        animator.SetTrigger("MeleeAttack");
                        IsAttacking = true;
                    }
                }
                break;
            default:
                break;
        }
    }
    protected override void e_Alert()
    {
        base.e_Alert();
    }

    protected override void e_Death()
    {
        base.e_Death();
        foreach(AnimatorControllerParameter parameter in animator.parameters)
        {
            if(parameter.type == AnimatorControllerParameterType.Trigger)
            {
                animator.ResetTrigger(parameter.name);
            }
        }
        animator.SetBool("IsDeath", true);
    }
    protected override void e_Patrol()
    {
        base.e_Patrol();
    }

    protected override void UpdateAnimator()
    {
        base.UpdateAnimator();
    }

    public override void e_FoundPlayer()
    {
        base.e_FoundPlayer();
        attackstates = Attackstates.Attacking;
    }
}
