using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestMonster : MonsterBasic {

    // Use this for initialization
    [SerializeField]
    float DashDistance = 7;
	protected override void Start () {
        base.Start();
	}
	
	// Update is called once per frame
	protected override void Update () {
        base.Update();
        UpdateAnimator();
	}

    void UpdateAnimator()
    {
        animator.SetFloat("Speed", Mathf.Abs(navMesh.velocity.z));
    }

    protected override void e_Patrol()
    {
        base.e_Patrol();
        navMesh.SetDestination(patrolArray[patrolPoint]);
    }

    protected override void e_Alert()
    {
        base.e_Alert();
    }

    protected override void e_Attacking()
    {
        base.e_Attacking();

        navMesh.SetDestination(player.transform.position);

        if (Vector3.Distance(transform.position, player.transform.position) > DashDistance)
        {

        }
        else
        {
           // navMesh.speed = 0;
        }

    }
}
