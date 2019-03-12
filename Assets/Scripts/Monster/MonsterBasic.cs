using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(FieldOfView))]
[RequireComponent(typeof(Animator))]
public class MonsterBasic : MonoBehaviour {

    public GameObject player;
    public Vector3[] patrolArray;
    protected NavMeshAgent navMesh;
    protected int patrolPoint = 0;
    protected FieldOfView FieldOfView;

    protected List<GameObject> targets;
    protected Animator animator;

    enum Attackstates
    {
        Patrol,
        Alert,
        Attacking
    }

    [SerializeField]
    Attackstates attackstates;
	// Use this for initialization
	protected virtual void Start () {
        attackstates = Attackstates.Patrol;
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        navMesh = GetComponent<NavMeshAgent>();
        FieldOfView = GetComponent<FieldOfView>();
    }
	
	// Update is called once per frame
	protected virtual void Update () {

        switch (attackstates)
        {
            case Attackstates.Patrol:
                e_Patrol();
                break;
            case Attackstates.Alert:
                e_Alert();
                break;
            case Attackstates.Attacking:
                e_Attacking();
                break;
        }

        targets = FieldOfView.ViewTargets;
        UpdateAttackState();
	}

    void UpdateAttackState()
    {
        if (targets.Count > 0)
        {
            attackstates = Attackstates.Attacking;
        }
    }

    protected virtual void e_Patrol()
    {
    }

    protected virtual void e_Alert()
    {

    }

    protected virtual void e_Attacking()
    {
    }
}
