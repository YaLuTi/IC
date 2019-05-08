using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Golem : TestMonster {

	// Use this for initialization
	protected override void Start () {
        base.Start();
        attackstates = Attackstates.Attacking;
	}
	
	// Update is called once per frame
	protected override void Update () {
        base.Update();
	}

    protected override void e_Alert()
    {
        base.e_Alert();
    }

    protected override void e_Attacking()
    {
        base.e_Attacking();
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
}
