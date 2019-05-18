using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponController : MonoBehaviour {

    public GameObject WeaponParent;
    [Header("Put Your Weapon Here")]
    public GameObject SpawnWeapon;
	// Use this for initialization
	void Awake () {
        GameObject g = SpawnWeapon;
        g = Instantiate(g);
        g.transform.parent = WeaponParent.transform;
        g.transform.localPosition = Vector3.zero;
        g.transform.localRotation = Quaternion.identity;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
