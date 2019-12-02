using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow_Magician_AI : TestMonster
{
    public float LiteMagicDistance;

    protected override void Update()
    {
        PlayerDistance = Mathf.Abs(Vector3.Distance(player.transform.position, transform.position));
        base.Update();
        // destination = Nav.GetCorners();
        CD();
    }

    void CD()
    {

    }

    protected override void e_Alert()
    {
        base.e_Alert();
    }
    protected override void e_Attacking()
    {
        base.e_Attacking();
        solution = BattleSolution.LongRange;

        if (IsStun) return;
        switch (solution)
        {
            case BattleSolution.LongRange:

                if (IsAttacking) return;

                if(PlayerDistance < LiteMagicDistance)
                {
                    animator.SetTrigger("CastLiteMagic");
                    IsAttacking = true;
                }

                break;
            default:
                break;
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
    protected override void Start()
    {
        base.Start();
        destination = Nav.GetCorners();
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
