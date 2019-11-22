using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "WeaponData")]
public class WeaponList : ScriptableObject
{
    public WeaponAsset[] weaponAssets;
}

[System.Serializable]
public class WeaponAsset
{
    public string name;
    public string ID;
    public GameObject weapon;
}
