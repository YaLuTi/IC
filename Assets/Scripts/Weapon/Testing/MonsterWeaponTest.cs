using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterWeaponTest : MonoBehaviour {

    WeaponColliderBasic weapon;
    MeleeWeaponTrail weaponTrail;
	// Use this for initialization
	void Start () {
        weapon = GetComponentInChildren<WeaponColliderBasic>();
        weaponTrail = GetComponentInChildren<MeleeWeaponTrail>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void WeaponOn()
    {
        // Debug.Log("On");
        weapon.StartAttack();
    }
    void WeaponOff()
    {
        // Debug.Log("Off");
        weapon.StopAttack();
    }

    void TrailOn()
    {
        weaponTrail.TrailOn();
    }

    void TrailOff()
    {
        weaponTrail.TrailOff();
    }
}
