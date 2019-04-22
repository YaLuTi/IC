using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(FieldOfView))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(AINav))]
public class MonsterBasic : MonoBehaviour {

    public GameObject player;
    public Vector3[] patrolArray;
    protected NavMeshAgent navMesh;
    protected int patrolPoint = 0;
    protected FieldOfView FieldOfView;
    public float moveSpeed = 1f;
    public float Health = 1f;
    public float Stamina = 10f;
    protected AudioSource audioSource;
    protected AINav Nav; 

    protected List<GameObject> targets;
    protected Animator animator;


    [SerializeField]
    protected Attackstates attackstates;
	// Use this for initialization
	protected virtual void Start () {
        attackstates = Attackstates.Patrol;
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        navMesh = GetComponent<NavMeshAgent>();
        FieldOfView = GetComponent<FieldOfView>();
        audioSource = GetComponent<AudioSource>();
        Nav = GetComponent<AINav>();
    }
	
	// Update is called once per frame
	protected virtual void Update () {

        if(Health <= 0)
        {
            attackstates = Attackstates.Death;
        }
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
            case Attackstates.Death:
                e_Death();
                break;
            default:
                break;
        }

        targets = FieldOfView.ViewTargets;
        UpdateAttackState();
	}

    public virtual void UpdateAttackState()
    {
    }

    public virtual void Damaged(float damage)
    {
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
    protected virtual void e_Death()
    {

    }
}
