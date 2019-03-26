using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestMonster : MonsterBasic {

    // Use this for initialization
    [SerializeField]
    float DashDistance = 1f;

    bool Attacking = false;


	protected override void Start () {
        base.Start();
        moveSpeed = 1f;
        navMesh.updatePosition = false;
    }
	
	// Update is called once per frame
	protected override void Update () {
        base.Update();
        InstantlyTurn(navMesh.destination);
        navMesh.Warp(transform.position);
        
        UpdateAnimator();
    }

    void UpdateAnimator()
    {
        // animator.SetBool("IsDamaged", false);
        animator.SetFloat("Speed", Mathf.Abs(moveSpeed));
    }

    protected override void e_Patrol()
    {
        base.e_Patrol();
        Debug.DrawLine(transform.position, patrolArray[patrolPoint], Color.red);
        navMesh.SetDestination(patrolArray[patrolPoint]);
        float RemainingDistance = Vector3.Distance(transform.position, patrolArray[patrolPoint]);
        if (RemainingDistance <= 0.5f)
        {
            patrolPoint++;
            patrolPoint = patrolPoint % patrolArray.Length;
        }
    }

    public override void Damaged()
    {
        base.Damaged();
        StartCoroutine(DamagedEvent());
    }

    IEnumerator DamagedEvent()
    {
        animator.SetBool("IsDamaged", true);
        animator.SetInteger("Attack", 0);
        yield return new WaitForSeconds(.25f);
        animator.SetBool("IsDamaged", false);
        yield return 0;
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
            animator.SetInteger("Attack", 0);
            moveSpeed = 2f;
            navMesh.angularSpeed = 300f;
        }
        else
        {
            animator.SetInteger("Attack", 1);
        }
    }

    // Need fix to NavPath
    private void InstantlyTurn(Vector3 destination)
    {
        //When on target -> dont rotate!
        if ((destination - transform.position).magnitude < 0.1f) return;

        Vector3 direction = (destination - transform.position).normalized;
        Quaternion qDir = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, qDir, Time.deltaTime * 1f);
    }
    
    public void OnAnimatorMove()
    {
        // we implement this function to override the default root motion.
        // this allows us to modify the positional speed before it's applied.
        if (Time.deltaTime > 0)
        {
            Vector3 v = (animator.deltaPosition * 0.01f * moveSpeed) / Time.deltaTime;
            transform.position += v;
        }
    }
}
