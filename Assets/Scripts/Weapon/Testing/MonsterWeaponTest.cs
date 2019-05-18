using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterWeaponTest : MonoBehaviour {

    WeaponColliderBasic weapon;
	// Use this for initialization
	void Start () {
        weapon = GetComponentInChildren<WeaponColliderBasic>();
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
}
