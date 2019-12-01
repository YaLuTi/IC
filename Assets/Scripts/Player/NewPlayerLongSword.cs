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
    public AnimationEvent FocusAttack_One;
    public AnimationEvent FocusAttack_Two;
    public AnimationEvent FocusAttack_Three;
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
    public int combo = 0;
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
        Value();
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
                return;
            }
        }
        else
        {
            if (Input.GetButtonUp("JoyStickX") && _DodgeCooldownCount >= DodgeCooldown && (playerHP.CheckSP(3) || playerHP.OverpullSP()))
            {
                _DodgeCooldownCount = 0;
                combatEventer.SetAnimation(Dodge);
                return;
            }
        }
        if(Input.GetAxis("DPadY") == 1 && !Drinking)
        {
            Drinking = true;
            combatEventer.SetAnimation(Drink);
            animator.SetBool("IsHealing", true);
            return;
        }
        else if(Input.GetAxis("DPadY") == 0)
        {
            Drinking = false;
        }

        // Add a new animator Trigger named FocusAttack   要想辦法處理TriggerName問題


        if (Input.GetButtonDown("R1") && playerHP.CheckSP(2))
        {
            if (animator.GetBool("Focus"))
            {
                if(combo == 0)
                {
                    if (combatEventer.SetAnimation(FocusAttack_One))
                    {
                        combo = 1;
                        return;
                    }
                }
                else if(combo == 1)
                {
                    if (combatEventer.SetAnimation(FocusAttack_Two))
                    {
                        combo = 2;
                        return;
                    }
                }
                else if (combo == 2)
                {
                    if (combatEventer.SetAnimation(FocusAttack_Three))
                    {
                        combo = 0;
                        return;
                    }
                }
            }
            else
            {
                if (combo == 0)
                {
                    if (combatEventer.SetAnimation(LightAttack_One))
                    {
                        combo = 2;
                        return;
                    }
                }
                else if (combo == 2)
                {
                    if (combatEventer.SetAnimation(LightAttack_Two))
                    {
                        combo = 0;
                        return;
                    }
                }
            }
        }
        if (Input.GetButtonDown("R2") && playerHP.CheckSP(2))
        {
            combatEventer.SetAnimation(HeavyAttack);
            return;
        }
        if (Input.GetButtonDown("L1") && playerHP.CheckSP(2))
        {
            combatEventer.SetAnimation(Block);
            animator.SetBool("IsBlock", true);
            return;
        }
        if (Input.GetButtonUp("L1"))
        {
            animator.SetBool("IsBlock", false);
        }
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
