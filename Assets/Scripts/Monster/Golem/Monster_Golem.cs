using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Golem : TestMonster {

    // Use this for initialization
    [SerializeField]
    float AttackLowRange = 5f;

    public AudioAssets AttackLowSound;
    public AudioAssets AttackLowTwoSound;

    float d;

	protected override void Start () {
        base.Start();
        attackstates = Attackstates.Attacking;
	}
	
	// Update is called once per frame
	protected override void Update () {
        base.Update();

        destination = Nav.GetCorners();
        d = Vector3.Distance(player.transform.position, transform.position);
    }

    protected override void e_Alert()
    {
        base.e_Alert();
    }

    protected override void e_Attacking()
    {
        base.e_Attacking();

        if(d < AttackLowRange)
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

    void PlayAttackLow()
    {
        AttackLowSound.Play(audioSource);
    }

    void PlayAttackLowTwo()
    {
        AttackLowTwoSound.Play(audioSource);
    }
}
