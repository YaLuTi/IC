using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerLongSword : MonoBehaviour
{
    [Header("Animation Event")]
    public AnimationEvent Dodge;
    public AnimationEvent Step;
    public AnimationEvent Block;
    public AnimationEvent Drink;
    public AnimationEvent LightAttack_One;
    public AnimationEvent LightAttack_Two;
    public AnimationEvent HeavyAttack;

    // Basic
    PlayerCombatEventer combatEventer;
    PlayerHP playerHP;
    PlayerMove playerMove;
    Animator animator;
    WeaponColliderBasic weapon;

    // Value
    float DodgeCooldown = 20f;
    float _DodgeCooldownCount = 20f;
    float DodgeReverseSpeed = 1;
    int combo = 0;
    bool Drinking = false;

    void Start()
    {
        combatEventer = GetComponent<PlayerCombatEventer>();
        playerHP = GetComponent<PlayerHP>();
        playerMove = GetComponent<PlayerMove>();
        weapon = GetComponentInChildren<WeaponColliderBasic>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!animator.GetBool("IsOnCombo"))
        {
            combo = 0;
        }
        if (playerMove.IsLock)
        {
            if (Input.GetButtonUp("JoyStickX") && _DodgeCooldownCount >= DodgeCooldown && (playerHP.CheckSP(3) || playerHP.OverpullSP()))
            {
                _DodgeCooldownCount = 0;
                combatEventer.SetAnimation(Step);
            }
        }
        else
        {
            if (Input.GetButtonUp("JoyStickX") && _DodgeCooldownCount >= DodgeCooldown && (playerHP.CheckSP(3) || playerHP.OverpullSP()))
            {
                _DodgeCooldownCount = 0;
                combatEventer.SetAnimation(Dodge);
            }
        }
        if(Input.GetAxis("DPadY") == 1 && !Drinking)
        {
            Drinking = true;
            combatEventer.SetAnimation(Drink);
        }
        else if(Input.GetAxis("DPadY") == 0)
        {
            Drinking = false;
        }
        if (Input.GetButtonDown("R1") && playerHP.CheckSP(2))
        {
            if (combo == 0)
            {
                if (combatEventer.SetAnimation(LightAttack_One))
                {
                    combo = 1;
                }
            }
            else if(combo == 1)
            {
                if (combatEventer.SetAnimation(LightAttack_Two))
                {
                    combo = 0;
                }
            }
        }
        if (Input.GetButtonDown("R2") && playerHP.CheckSP(2))
        {
            combatEventer.SetAnimation(HeavyAttack);
        }
        if (Input.GetButtonDown("L1") && playerHP.CheckSP(2))
        {
            combatEventer.SetAnimation(Block);
            animator.SetBool("IsBlock", true);
        }
        if (Input.GetButtonUp("L1"))
        {
            animator.SetBool("IsBlock", false);
        }
        Value();
    }

    void Value()
    {
        if (_DodgeCooldownCount < DodgeCooldown) _DodgeCooldownCount += DodgeReverseSpeed;
    }

    void PlaySlash()
    {

    }
    void WeaponOn()
    {
        weapon.StartAttack();
    }
    void WeaponOff()
    {
        weapon.StopAttack();
    }
}
