using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class PlayerWeaponController : MonoBehaviour {

    public GameObject WeaponParent;
    [Header("Put Your Weapon Here")]
    public GameObject SpawnWeapon;
    public WeaponList weaponList;
    [Header("Other Shit")]
    int WeaponNum;

    public GameObject WeaponUI;
    Image WeaponIcon;
    ButtonHighLight WeaponFrame;

    PlayerCombatEventer combatEventer;
    public AnimationEvent ChangeWeaponEvent;
	// Use this for initialization
	void Awake () {
        GameObject g = SpawnWeapon;
        g = Instantiate(g);
        g.transform.parent = WeaponParent.transform;
        g.transform.localPosition = Vector3.zero;
        g.transform.localRotation = Quaternion.identity;
        combatEventer = GetComponent<PlayerCombatEventer>();
        WeaponIcon = WeaponUI.GetComponent<Image>();
        WeaponFrame = WeaponUI.GetComponentInChildren<ButtonHighLight>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("JoyStickT"))
        {
            if (combatEventer.SetAnimation(ChangeWeaponEvent))
            {
                WeaponFrame._HighLight();
            }
        }
	}
    
    void ChangeWeapon(GameObject weapon)
    {

    }
}
