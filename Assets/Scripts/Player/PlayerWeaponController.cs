using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class PlayerWeaponController : MonoBehaviour {

    public GameObject WeaponParent;
    [Header("Put Your Weapon Here")]
    public GameObject SpawnWeapon;
    GameObject UsingWeapon;
    public WeaponList weaponList;
    [Header("Other Shit")]
    int WeaponNum;

    public GameObject WeaponUI;
    Image WeaponIcon;
    ButtonHighLight WeaponFrame;

    PlayerCombatEventer combatEventer;
    NewPlayerLongSword longSword;
    public AnimationEvent ChangeWeaponEvent;
	// Use this for initialization
	void Awake () {
        UsingWeapon = SpawnWeapon;
        UsingWeapon = Instantiate(UsingWeapon);
        UsingWeapon.transform.parent = WeaponParent.transform;
        UsingWeapon.transform.localPosition = Vector3.zero;
        UsingWeapon.transform.localRotation = Quaternion.identity;
        combatEventer = GetComponent<PlayerCombatEventer>();
        WeaponIcon = WeaponUI.GetComponent<Image>();
        WeaponFrame = WeaponUI.GetComponentInChildren<ButtonHighLight>();
        longSword = GetComponent<NewPlayerLongSword>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (CutSceneDirector.IsOnMovie) return;
        if (Input.GetButtonDown("JoyStickT"))
        {
            if (combatEventer.SetAnimation(ChangeWeaponEvent))
            {
                WeaponFrame._HighLight();
            }
        }
	}
    
    public void ChangeWeapon(GameObject weapon)
    {
        Destroy(UsingWeapon);
        UsingWeapon = weapon;
        UsingWeapon = Instantiate(weapon);
        UsingWeapon.transform.parent = WeaponParent.transform;
        UsingWeapon.transform.localPosition = Vector3.zero;
        UsingWeapon.transform.localRotation = Quaternion.identity;
        longSword.weapon = UsingWeapon.GetComponent<WeaponColliderBasic>();
    }
}
