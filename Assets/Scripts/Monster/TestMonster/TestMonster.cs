using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(FieldOfView))]
public class TestMonster : MonsterBasic {

    // Use this for initialization
    [SerializeField]
    protected float BattleRange = 10f;

    bool Attacking = false;

    protected Vector3 destination;
    public float AngularSpeed = 2f;

    public AudioEvent[] cutAudio;

	protected override void Start () {
        base.Start();
    }
	
	// Update is called once per frame
	protected override void Update () {

        base.Update();
        InstantlyTurn();
        
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
        // navMesh.SetDestination(patrolArray[patrolPoint]);
        if (patrolArray.Length > 0)
        {
            destination = patrolArray[patrolPoint];
        }

        if (patrolArray.Length > 0)
        {
            float RemainingDistance = Vector3.Distance(transform.position, patrolArray[patrolPoint]);

            if (RemainingDistance <= 1f)
            {
                patrolPoint++;
                patrolPoint = patrolPoint % patrolArray.Length;
            }
        }
    }


    public override void UpdateAttackState()
    {
        base.UpdateAttackState();
        if (targets.Count > 0)
        {
            attackstates = Attackstates.Attacking_OutRange;
        }
    }
    public override void Damaged(float damage)
    {
        base.Damaged(damage);
        if (IsDeath) return;
        attackstates = Attackstates.Attacking_OutRange;
        animator.SetBool("IsLock", true);
        Health -= damage;

        for(int i = 0; i < cutAudio.Length; i++)
        {
            cutAudio[i].Play(audioSource);
        }

        animator.SetTrigger("Damaged");
        //StartCoroutine(DamagedEvent());
    }

    IEnumerator DamagedEvent()
    {
        // animator.SetBool("IsDamaged", true);
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

        /*destination = Nav.GetCorners();
        

        if (Vector3.Distance(transform.position, player.transform.position) > DashDistance)
        {
            moveSpeed = 3f;
        }
        else
        {
            animator.SetTrigger("At");
        }*/
    }

    protected override void e_Death()
    {
        base.e_Death();
        animator.SetTrigger("Death");
        IsDeath = true;
        this.enabled = false;
        moveSpeed = 0;
    }

    // Need fix to NavPath
    private void InstantlyTurn()
    {
        //When on target -> dont rotate!
        /*if ((destination - transform.position).magnitude < 0.1f) return;

        Vector3 direction = (destination - transform.position).normalized;
        Quaternion qDir = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, qDir, Time.deltaTime * 1f);*/

        
        AnimatorStateInfo animatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (animatorStateInfo.IsTag("Attack")) return;
        if ((destination - transform.position).magnitude < 0.1f) return;

        Vector3 direction = (destination - transform.position).normalized;
        Quaternion qDir = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, qDir, AngularSpeed * Time.deltaTime);
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
