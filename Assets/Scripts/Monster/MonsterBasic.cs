using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterBasic : MonoBehaviour {

    public GameObject player;
    public Vector3[] patrolArray;
    NavMeshAgent navMesh;
    int patrolPoint = 0;

    enum Attackstates
    {
        Patrol,
        Alert,
        Attacking
    }

    Attackstates attackstates;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        navMesh = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {

        switch (attackstates)
        {
            case Attackstates.Patrol:
                e_Patrol();
                break;
            case Attackstates.Alert:
                break;
            case Attackstates.Attacking:
                break;
        }
        e_Patrol();
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

    }
}
