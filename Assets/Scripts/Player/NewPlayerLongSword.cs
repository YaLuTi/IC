using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerLongSword : MonoBehaviour
{
    [Header("Animation Event")]
    public AnimationEvent Dodge;
    public AnimationEvent LightAttack_One;

    // Basic
    PlayerCombatEventer combatEventer;
    PlayerHP playerHP;
    WeaponColliderBasic weapon;

    // Value
    float DodgeCooldown = 20f;
    float _DodgeCooldownCount = 20f;
    float DodgeReverseSpeed = 1;

    void Start()
    {
        combatEventer = GetComponent<PlayerCombatEventer>();
        playerHP = GetComponent<PlayerHP>();
        weapon = GetComponentInChildren<WeaponColliderBasic>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonUp("JoyStickX") && _DodgeCooldownCount >= DodgeCooldown)
        {
            _DodgeCooldownCount = 0;
            combatEventer.SetAnimation(Dodge);
        }

        if (Input.GetButtonDown("R1"))
        {
            combatEventer.SetAnimation(LightAttack_One);
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
