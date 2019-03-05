using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(FieldOfView))]
public class MonsterBasic : MonoBehaviour {

    public GameObject player;
    public Vector3[] patrolArray;
    NavMeshAgent navMesh;
    int patrolPoint = 0;
    FieldOfView FieldOfView;

    List<GameObject> targets;

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

    void e_Patrol()
    {
        navMesh.SetDestination(patrolArray[patrolPoint]);
    }

    void e_Alert()
    {

    }

    void e_Attacking()
    {
        navMesh.SetDestination(player.transform.position);
    }
}
