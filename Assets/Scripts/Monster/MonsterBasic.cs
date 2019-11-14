using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

[RequireComponent(typeof(FieldOfView))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(AINav))]
public class MonsterBasic : MonoBehaviour {

    [Header("Effect")]
    public GameObject DamagedParticle;
    [Header("Audio")]
    public AudioClip[] DamagedAudios;
    public RangedFloat volume;
    [MinMaxRange(0f, 2f)]
    public RangedFloat pitch;

    [Header("AI")]
    public GameObject player;
    public Vector3[] patrolArray;
    protected NavMeshAgent navMesh;
    protected int patrolPoint = 0;
    protected FieldOfView FieldOfView;
    public bool IsAttacking = false;
    public bool RotateAble = true;
    public bool IsStun = false;

    public enum BattleSolution
    {
        MeleeAttack,
        LongRange,
        Runaway,
        Close
    }
    [SerializeField]
    public BattleSolution solution;

    [Header("MonsterSetting")]
    public Slider HPSlider;
    public float moveSpeed = 1f;
    public float MaxHealth = 1;
    protected float Health = 1f;
    public float Stamina = 10f;
    public bool IsEventMonster = false;
    protected AudioSource audioSource;
    protected AINav Nav;
    protected bool IsDeath = false;

    protected List<GameObject> targets;
    protected Animator animator;


    [SerializeField]
    protected Attackstates attackstates;
	// Use this for initialization
	protected virtual void Start () {
        // attackstates = Attackstates.Patrol;
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        navMesh = GetComponent<NavMeshAgent>();
        FieldOfView = GetComponent<FieldOfView>();
        audioSource = GetComponent<AudioSource>();
        Nav = GetComponent<AINav>();
        Health = MaxHealth;
    }
	
	// Update is called once per frame
	protected virtual void Update () {
        if (IsDeath) return;

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
            case Attackstates.NuN:
                e_NuN();
                break;
            default:
                break;
        }
        if(HPSlider != null)
        {
            HPSlider.value = Health / MaxHealth;
        }
        targets = FieldOfView.ViewTargets;
        UpdateAttackState();
	}

    public virtual void UpdateAttackState()
    {
    }

    public virtual void Damaged(float damage, Vector3 p)
    {
        GameObject g = Instantiate(DamagedParticle, p, Quaternion.identity);
        Destroy(g, 3);
        if (DamagedAudios.Length == 0) return;

        audioSource.clip = DamagedAudios[Random.Range(0, DamagedAudios.Length)];
        audioSource.volume = Random.Range(volume.minValue, volume.maxValue);
        audioSource.pitch = Random.Range(pitch.minValue, pitch.maxValue);
        audioSource.PlayOneShot(audioSource.clip);
    }

    public void SetAttack()
    {
        attackstates = Attackstates.Attacking;
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
    protected virtual void e_NuN()
    {

    }
    public virtual void e_FoundPlayer()
    {

    }
}
