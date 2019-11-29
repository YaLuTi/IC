using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XInputDotNetPure;


public class PlayerHP : MonoBehaviour {

    Animator animator;
    public bool Invulnerability = false; // Remember change this to private

    public GameObject DamagedParticle;
    PlayerCombatEventer playerCombatEventer;
    public AnimationEvent DamagedEvent;

    [Header("Value")]
    public float MaxHP = 10;
    public float HP = 10;
    public Slider HPslider;

    public float MaxSP = 10;
    public float SP = 10;
    public Slider SPslider;
    public float SPRegenSpeed;
    public float SPRegenCoolDown;
    float SPRegenCount;

    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
        playerCombatEventer = GetComponent<PlayerCombatEventer>();

    }
	
	// Update is called once per frame
	void Update ()
    {

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

    public void Damaged(float damage)
    {
        if (animator.GetBool("IsBlock")) return;
        if (Invulnerability)
        {
            Debug.Log("Dodge");
            return;
        }
        foreach (AnimatorControllerParameter parameter in animator.parameters)
        {
            animator.ResetTrigger(parameter.name);
        }
        Instantiate(DamagedParticle, transform.position, Quaternion.identity);
        HP -= damage;
        playerCombatEventer.SetAnimation(DamagedEvent);
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

    void SetInvulnerability()
    {
        Invulnerability = true;
    }
    void ReSetInvulnerability()
    {
        Invulnerability = false;
    }
}
