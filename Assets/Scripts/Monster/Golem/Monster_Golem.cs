using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Golem : TestMonster {

    // Use this for initialization
    [Header("AttackValue")]
    [SerializeField]
    float CloseDistance = 5f;
    [SerializeField]
    float MidDistance = 10f;
    [SerializeField]
    float LongDistance = 15f;

    [SerializeField]
    float ComboLowRange = 5f;
    [SerializeField]
    float ComboLowCD = 450f;
    [SerializeField]
    float ShortAttackRange = 5f;
    [SerializeField]
    float ShortAttackCD = 300f;
    [SerializeField]
    float ComboRotateRange = 9f;
    [SerializeField]
    float ComboRotateCD = 10f;
    [SerializeField]
    float JumpSlashRange = 15f;
    [SerializeField]
    float JumpSlashCD = 15f;
    [SerializeField]
    float CounterSlashCD = 200f;

    public float testRange = 5;

    float speed = 1;
    public float anger = 0;

    bool SecondForm = false;

    [Header("Particle")]
    public GameObject JumpSlashParticle;
    public GameObject JumpSlashParticle_t;
    bool IsJumpSlashParticle = false;

    [Header("Sound Assets")]
    public AudioAssets StepSound;
    public AudioAssets AttackLowSound;
    public AudioAssets AttackLowTwoSound;
    public AudioAssets JumpSound;
    public AudioAssets AttackJumpSlashOneSound;
    public AudioAssets AttackJumpSlashTwoSound;
    public AudioAssets AttackDownAttackOneSound;
    public AudioAssets AttackDownAttackTwoSound;
    

    // Set State
	protected override void Start ()
    {
        base.Start();
        PlayerDistance = Mathf.Abs(Vector3.Distance(player.transform.position, transform.position));
        destination = Nav.GetCorners();
    }
	
	// Set nav corner and speed
	protected override void Update ()
    {
        base.Update();
        if (player.activeSelf == true)
        {
            PlayerDistance = Vector3.Distance(player.transform.position, transform.position);
        }
        CD();
        if (IsJumpSlashParticle)
        {
            IsJumpSlashParticle = false;
            Debug.Log(JumpSlashParticle_t.transform.position);
            GameObject g = Instantiate(JumpSlashParticle, JumpSlashParticle_t.transform.position, Quaternion.identity);
            Destroy(g, 6);
        }
        animator.SetFloat("Speed", speed);
    }
    void OnDrawGizmosSelected()
    {
        {
            Gizmos.color = new Color(0,1,1,0.3f);
            Gizmos.DrawSphere(transform.position, testRange);
        }
    }

    protected override void e_Alert()
    {
        base.e_Alert();
    }

    protected override void e_Attacking()
    {
        base.e_Attacking();

        speed = 1;
        /*if (d < JumpSlashRange && d > WalkAround)
        {
            animator.SetTrigger("JumpSlash");
        }else if (d < WalkAround && d > AttackLowRange)
        {
            speed = 1;
            if(d < 6)
            {
                animator.SetTrigger("DownAttack");
            }
        }
        else if (d < AttackLowRange)
        {
            animator.SetTrigger("AttackLow");
        }*/
        if (IsAttacking) return;
       if(PlayerDistance < CloseDistance)
        {
            // SetXY(0, 1);
            if (PlayerDistance < 4 && ShortAttackCD >= 300)
            {
                IsAttacking = true;
                ShortAttackCD = 0;
                animator.SetTrigger("ShortAttack");
                return;
            }
            else if (PlayerDistance < 7 && ComboLowCD >= 450)
            {
                IsAttacking = true;
                ComboLowCD = 0;
                animator.SetTrigger("AttackLow");
                return;
            }
            else if(PlayerDistance > 4 && ShortAttackCD >= 300)
            {
                SetXY(0, 1);
                anger++;
            }
            else if (PlayerDistance < 4 && anger >= 100)
            {
                if (CounterSlashCD >= 150)
                {
                    IsAttacking = true;
                    anger = 0;
                    CounterSlashCD = 0;
                    animator.SetTrigger("SlashOne");
                }else if(ShortAttackCD >= 100)
                {
                    IsAttacking = true;
                    ShortAttackCD = 0;
                    animator.SetTrigger("ShortAttack");
                    return;
                }
                return;
            }
            else if(PlayerDistance < 7 && ComboLowCD < 450 && ShortAttackCD < 300)
            {
                SetXY(0, -1);
                anger++;
            }
            else
            {
                SetXY(1, 0);
                // Need a value to avoid it keep rotate
            }
            
        }
        else if(PlayerDistance < MidDistance)
        {
            if(anger > 0)
            {
                anger--;
            }
            if (PlayerDistance > 9 && JumpSlashCD >= 600)
            {
                if(PlayerDistance < 12)
                {
                    SetXY(0, -1);
                }
                else
                {
                    IsAttacking = true;
                    JumpSlashCD = 0;
                    animator.SetTrigger("JumpSlash");
                    return;
                }
            }
            else if(PlayerDistance > 9)
            {
                if(JumpSlashCD < 300)
                {
                    SetXY(0, 1);
                }else if (JumpSlashCD > 300)
                {
                    SetXY(1, 0);
                }
                else
                {
                    SetXY(0, 1);
                }
            }
            else if (PlayerDistance < 9 && ComboRotateCD >= 500)
            {
                IsAttacking = true;
                ComboRotateCD = 0;
                animator.SetTrigger("ComboAttack");
                return;
            }
            else
            {
                if(ComboLowCD >= 450 || ShortAttackCD >= 300)
                {
                    SetXY(0, 1);
                }
                else
                {
                    SetXY(1, 0);
                }
            }
        }
        else
        {
            if (anger > 0)
            {
                anger--;
            }
            if (PlayerDistance < 15 && JumpSlashCD >= 600)
            {
                IsAttacking = true;
                JumpSlashCD = 0;
                animator.SetTrigger("JumpSlash");
                return;
            }
            else if (PlayerDistance > 20 && JumpSlashCD >= 600)
            {
                SetXY(0, 2);
            }else if (PlayerDistance < 20 && animator.GetFloat("Y") < 2 && JumpSlashCD >= 600)
            {
                SetXY(0, 1);
            }
            else
            {
                SetXY(0, 1);
            }
        }
    }

    void SetXY(float x, float y)
    {
        animator.SetFloat("X", x, 0.5f, Time.deltaTime);
        animator.SetFloat("Y", y, 0.5f, Time.deltaTime);
    }

    public override void Damaged(float damage, Vector3 p, Vector3 Attacker)
    {
        base.Damaged(damage, p, Attacker);
        if (PlayerDistance < CloseDistance)
        {
            anger += 50;
        }
    }

    void CD()
    {
        if (IsAttacking) return;
        if (ComboLowCD < 450)
        {
            ComboLowCD++;
        }
        if (ShortAttackCD < 300)
        {
            ShortAttackCD++;
        }
        ComboRotateCD++;
        JumpSlashCD++;
        CounterSlashCD++;
    }

    void e_Attacking_OutRange()
    {
        /*if (d < AttackLowRange)
        {
            animator.SetTrigger("AttackLow");
        }*/
    }

    protected override void e_Death()
    {
        base.e_Death();
    }

    protected override void e_Patrol()
    {
        base.e_Patrol();
    }

    public override void e_FoundPlayer()
    {
        base.e_FoundPlayer();
        attackstates = Attackstates.Attacking;
    }

    public override void UpdateAttackState()
    {
        base.UpdateAttackState();
    }
    

    public void SpawnParticle(int i)
    {
        switch (i)
        {
            case 0:
                IsJumpSlashParticle = true;
                break;
            default:
                break;
        }
    }

    // Should be like animation curver

    void PlayAttackLow()
    {
        AttackLowSound.Play(audioSource);
    }

    void PlayAttackLowTwo()
    {
        AttackLowTwoSound.Play(audioSource);
    }

    void PlayJump()
    {
        JumpSound.Play(audioSource);
    }

    void PlayJumpSlashOne()
    {
        AttackJumpSlashOneSound.Play(audioSource);
    }

    void PlayJumpSlashTwo()
    {
        AttackJumpSlashTwoSound.Play(audioSource);
    }
    void PlayDownAttackOne()
    {
        AttackDownAttackOneSound.Play(audioSource);
    }
    void PlayDownAttackTwo()
    {
        AttackDownAttackTwoSound.Play(audioSource);
    }

    void PlayStep()
    {
        StepSound.Play(audioSource);
    }

    void ResetTrigger()
    {
        animator.ResetTrigger("AttackLow");
        animator.ResetTrigger("JumpSlash");
    }
}
