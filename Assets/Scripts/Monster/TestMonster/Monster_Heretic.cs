using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Heretic : TestMonster {

    bool StateLock = false;

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
        if(attackstates == Attackstates.Attacking_OutRange)
        {
            animator.SetFloat("Speed", 0);
            e_Attacking_OutRange();
        }

        switch (solution)
        {
            case BattleSolution.Runaway:
                animator.SetFloat("Speed", 5);
                destination = Nav.GetCorners();
                break;
            case BattleSolution.Close:
                break;
            default:
                break;
        }
	}

    void e_Attacking_OutRange()
    {
        Nav.CaculatePlayerMomentum();
        if (!StateLock)
        {
            if(Nav.PlayerMomentum > 1)
            {
                solution = BattleSolution.Close;
                StateLock = true;
            }
            if(Nav.PlayerMomentum < -1)
            {
                solution = BattleSolution.Runaway;
                StateLock = true;
            }
        }
    }

    public override void UpdateAttackState()
    {
        base.UpdateAttackState();
        if(attackstates == Attackstates.Attacking)
        {
            float d = Vector3.Distance(player.transform.position, transform.position);
            if(d > BattleRange)
            {
                attackstates = Attackstates.Attacking_OutRange;
            }
        }
    }
}
