using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XInputDotNetPure;


public class PlayerHP : MonoBehaviour {

    Animator animator;
    public bool Invulnerability = false; // Remember change this to private
    public GameObject HealthPotion;

    public GameObject DamagedParticle;
    public AK.Wwise.Event DamagedSoundEvent;
    PlayerCombatEventer playerCombatEventer;
    public AnimationEvent DamagedEvent;
    public AnimationEvent DeathEvent;

    [Header("Value")]
    public float MaxHP = 10;
    public float HP = 10;
    public Slider HPslider;

    public float MaxSP = 10;
    public float SP = 10;
    public Slider SPslider;
    public float SPRegenSpeed;
    public float SPRegenCoolDown;
    public static bool IsDeath = false;
    float SPRegenCount;

    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
        playerCombatEventer = GetComponent<PlayerCombatEventer>();

    }
	
	// Update is called once per frame
	void Update ()
    {
        if (!animator.GetBool("IsHealing"))
        {
            HealthPotion.SetActive(false);
        }
        if(SPRegenCount > SPRegenCoolDown && SP < MaxSP)
        {
            SP += SPRegenSpeed * Time.deltaTime;
            if(SP >= MaxSP)
            {
                SP = MaxSP;
            }
        }else if (SPRegenCount < SPRegenCoolDown)
        {
            SPRegenCount += 1 * Time.deltaTime;
        }
        if (HPslider == null || SPslider == null) return;
        HPslider.value = HP / MaxHP;
        SPslider.value = SP / MaxSP;
    }

    public void TurnOnHealthPotion()
    {
        HealthPotion.SetActive(true);
    }

    public void Damaged(float damage)
    {
        if (IsDeath) return;
        if (animator.GetBool("IsBlock")) return;
        if (Invulnerability)
        {
            Debug.Log("Dodge");
            return;
        }
        Instantiate(DamagedParticle, transform.position, Quaternion.identity);
        DamagedSoundEvent.Post(gameObject);
        HP -= damage;
        if(HP < 0)
        {
            IsDeath = true;
            PlayerBackpackData.FirstDeath = true;
            playerCombatEventer.SetAnimation(DeathEvent);
            GameObject.FindGameObjectWithTag("Death").GetComponent<DeathUI>()._DeathEvent();
        }
        else
        {
            playerCombatEventer.SetAnimation(DamagedEvent);
        }
    }

    public void Healed(float heal)
    {
        if(HP < MaxHP)
        {
            HP += heal * Time.deltaTime;
        }
        if(HP >= MaxHP)
        {
            HP = MaxHP;
        }
    }

    public void ExpendSP(float sp)
    {
        if(SP - sp >= 0)
        {
            SP -= sp;
            SPRegenCount = 0;
        }
        else
        {
            SP = 0;
            SPRegenCount = 0;
        }
    }

    public bool CheckSP(float sp)
    {
        if (SP - sp >= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool OverpullSP()
    {
        if (SP > 0)
        {
            SPRegenCount = 0;
            SP = 0;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SetInvulnerability()
    {
        Invulnerability = true;
    }
    public void ReSetInvulnerability()
    {
        Invulnerability = false;
    }
}
